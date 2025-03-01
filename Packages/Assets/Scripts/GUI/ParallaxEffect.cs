using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _parallaxEffect;

    private Transform _camera;
    private float _startPos;
    private float _lenght;

    private void Start()
    {
        _camera = Camera.main.transform;
        _startPos = transform.position.x;
        _lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float distance = _camera.position.x * _parallaxEffect;
        float movement = _camera.position.x * (1 - _parallaxEffect);

        transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);

        if (movement > _startPos + _lenght)
            _startPos += _lenght;
        else if (movement < _startPos - _lenght)
            _startPos -= _lenght;
    }
}
