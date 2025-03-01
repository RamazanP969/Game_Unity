using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CapsuleCollider)), RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private Transform _camera;
    [SerializeField, Range(1, 10)] private float _movementSpeed;
    [SerializeField, Range(1, 10)] private float _mouseSensitivity;

    [Header("Gravity")]
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _fallMultiplier = 2.5f; //Коэф. падения
    [SerializeField] private float _ascendMultiplier = 2f; //Коэф. ускорения в прыжке
    [SerializeField] private LayerMask _groundLayer;

    [Header("Animations")]
    [SerializeField, Range(0, 1)] private float _animationChangeTime;

    private Rigidbody _rb;
    private Animator _animator;

    #region Controls
    private Controls _controls;
    private InputAction _moveInput;
    private Vector2 _moveDirection;
    private InputAction _cameraInput;
    private Vector2 _mousePosition;
    #endregion

    #region Gravity
    private bool _isGrounded = false;
    private float _groundCheckTimer = 0f;
    private float _groundCheckDelay = 0.3f;
    private float _playerHeight;
    private float _raycastDistance;
    #endregion

    private void Awake()
    {
        _controls = new Controls();
        _moveInput = _controls.Player.Movement;
        _cameraInput = _controls.Player.Camera;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;

        _animator = GetComponent<Animator>();

        _playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        _raycastDistance = 0.3f; //(_playerHeight / 2) + 0.2f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Player.Jump.performed += Jump;
    }

    private void Update()
    {
        _moveDirection = _moveInput.ReadValue<Vector2>();
        _mousePosition = _cameraInput.ReadValue<Vector2>();

        RotateCamera();
        if (!_isGrounded && _groundCheckTimer <= 0f)
        {
            //RaycastHit hit;
            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
            _isGrounded = Physics.Raycast(rayOrigin, Vector3.down, _raycastDistance, _groundLayer);
            //_isGrounded = Physics.SphereCast(rayOrigin, 0.35f, Vector3.down, out hit, _raycastDistance, _groundLayer);
            _animator.SetBool("IsGrounded", _isGrounded);
        }
        else
        {
            _groundCheckTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        ApplyJumpPhysics();
    }

    private void MovePlayer()
    {
        if (_moveDirection == Vector2.zero)
            _animator.SetFloat("Speed", 0f, _animationChangeTime, Time.deltaTime);
        else
            _animator.SetFloat("Speed", 1f, _animationChangeTime, Time.deltaTime);

        Vector3 movement = (transform.right * _moveDirection.x + transform.forward * _moveDirection.y) * _movementSpeed;

        Vector3 velocity = _rb.linearVelocity; 
        velocity.x = movement.x; 
        velocity.z = movement.z;
        _rb.linearVelocity = velocity;

        if (_isGrounded && _moveDirection.x == 0 && _moveDirection.y == 0) 
            _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
    }

    private void RotateCamera()
    {
        if (Mathf.Abs(_moveDirection.x) > 0 || Mathf.Abs(_moveDirection.y) > 0)
        {
            Vector3 currLookDirection = _camera.forward;
            currLookDirection.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(currLookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _mouseSensitivity);
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!_isGrounded) return;
        _animator.SetBool("IsJumping", true);
        _animator.SetBool("IsGrounded", false);
        _isGrounded = false;
        _groundCheckTimer = _groundCheckDelay;
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _jumpForce, _rb.linearVelocity.z);
    }

    private void ApplyJumpPhysics()
    {
        if (_rb.linearVelocity.y < 0) //Падение
        {
            _animator.SetBool("IsJumping", false);
            if (_isGrounded)
                _animator.SetBool("IsFalling", false);
            else
                _animator.SetBool("IsFalling", true);
            _rb.linearVelocity += Vector3.up * Physics.gravity.y * _fallMultiplier * Time.fixedDeltaTime;
        }
        else if (_rb.linearVelocity.y > 0) //Прыжок
        {
            _rb.linearVelocity += Vector3.up * Physics.gravity.y * _ascendMultiplier * Time.fixedDeltaTime;
        }
    }

    private void OnDisable()
    {
        _controls.Player.Jump.performed -= Jump;
        _controls.Disable();
    }
}
