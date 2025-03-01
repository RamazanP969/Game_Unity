using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] float _fireForce; 
    [SerializeField] float _timer; 
    [SerializeField] GameObject _prefab; 
    [SerializeField] Transform _offset; 

    private float _delay;
    private int _shotCount = 0; 

    private void Update()
    {
        if (_delay <= 0)
        {
            _shotCount++; 

          
            float currentFireForce = _fireForce;
            if (_shotCount % 3 == 0)
            {
                currentFireForce *= 2; 
            }

            var GO = Instantiate(_prefab, _offset.position, _offset.rotation);
            GO.GetComponent<Rigidbody>().AddForce(GO.transform.forward * currentFireForce, ForceMode.Impulse);

           
            if (_shotCount % 3 == 0)
            {
                _shotCount = 0;
            }

            _delay = _timer; 
        }

        _delay -= Time.deltaTime; 
    }
}
