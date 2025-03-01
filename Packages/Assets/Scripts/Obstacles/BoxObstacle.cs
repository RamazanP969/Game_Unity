using System;
using UnityEngine;

public class BoxObstacle : MonoBehaviour
{
    [Header("Borders")]
    [SerializeField] private Vector2 _xAxis; 
    [SerializeField] private Vector2 _yAxis;
    [SerializeField] private Vector2 _zAxis;

    [Header("Speeds")]
    [SerializeField, Range(1, 10)] private float _xSpeed;
    [SerializeField, Range(1, 10)] private float _ySpeed;
    [SerializeField, Range(1, 10)] private float _zSpeed;

    private float _x = 1f;
    private float _y = 1f;
    private float _z = 1f;

    private Vector3 _center;

    private void Start()
    {
        _center = transform.position;
    }

    private void Update()
    {
        CheckBorders();
        transform.position += new Vector3(_x * _xSpeed, _y * _ySpeed, _z * _zSpeed) * Time.deltaTime;
    }

    private void CheckBorders()
    {
        Vector3 position = transform.position;

        if (position.x <= _center.x + _xAxis.x || position.x >= _center.x + _xAxis.y) _x *= -1;
        if (position.y <= _center.y + _yAxis.x || position.y >= _center.y + _yAxis.y) _y *= -1;
        if (position.z <= _center.z + _zAxis.x || position.z >= _center.z + _zAxis.y) _z *= -1;
    }
}
