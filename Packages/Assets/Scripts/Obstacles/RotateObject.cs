using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 30f; // Скорость вращения объекта в градусах в секунду

    void Update()
    {
        // Вращаем объект вокруг оси Y (можно изменить на другие оси, если нужно)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}

