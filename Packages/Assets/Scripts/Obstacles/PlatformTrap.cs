using UnityEngine;

public class PlatformTrap : MonoBehaviour
{
    [SerializeField] GameObject _trapPlatform;
    [SerializeField] float _timeToActivate;
    [SerializeField] float _timeToDeactivate;

    private float _timeA;
    private float _timeD;
    private bool _needToRemove;
    private bool _needToSet;

    private void Start()
    {
        _timeA = _timeToActivate;
        _timeD = _timeToDeactivate;

        _needToRemove = false;
        _needToSet = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out _))
        {
            _needToRemove = true;
        }
    }

    private void Update()
    {
        if (_timeA <= 0)
        {
            _trapPlatform.SetActive(false);
            _needToSet = true;
            _needToRemove = false;
            _timeA = _timeToActivate;
        }
        
        if (_timeD <= 0)
        {
            _trapPlatform.SetActive(true);
            _needToSet = false;
            _timeD = _timeToDeactivate;
        }

        if (_needToRemove)
            _timeA -= Time.deltaTime;

        if (_needToSet)
            _timeD -= Time.deltaTime;

    }
}
