using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 30f; // �������� �������� ������� � �������� � �������

    void Update()
    {
        // ������� ������ ������ ��� Y (����� �������� �� ������ ���, ���� �����)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}

