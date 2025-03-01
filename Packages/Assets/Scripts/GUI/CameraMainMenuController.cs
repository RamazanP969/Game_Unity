using UnityEngine;

public class CameraMainMenuController : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;

    private void FixedUpdate()
    {
        transform.position += Vector3.left * _cameraSpeed * Time.fixedDeltaTime;
    }
}
