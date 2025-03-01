using UnityEngine;

public class Cannonball : MonoBehaviour
{
    [SerializeField] private float _life;
    private void Start()
    {
        Destroy(gameObject, _life);
    }
}
