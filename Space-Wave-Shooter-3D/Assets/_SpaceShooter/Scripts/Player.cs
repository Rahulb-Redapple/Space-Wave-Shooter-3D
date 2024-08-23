using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float thrust = 0.1f;

    [SerializeField] private float _maxSpeed = 50;

    private float _currentSpeed = 0;
    private float _horizontalMove;
    private float rotateDirection;

    private bool _canForward;

    Vector3 _defaultForce = new Vector3(0, 0, 50);
    Vector3 _forceToAdd = Vector3.zero;

    private void Update()
    {
        GetInput();
        Rotation();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Rotation()
    {
        rotateDirection = Input.GetAxis("Mouse X") * _turnSpeed;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + rotateDirection, 0);
    }

    private void GetInput()
    {
        //_horizontalMove = Input.GetAxis("Horizontal");
        _canForward = Input.GetKey(KeyCode.W);
    }

    private void Movement()
    {
        _currentSpeed = _rb.velocity.magnitude;

        if (_currentSpeed > _maxSpeed - (_maxSpeed / 4))
        {
            float forceMultiplier = _defaultForce.z * _maxSpeed - (_currentSpeed / _maxSpeed);
            _forceToAdd.z = forceMultiplier;
        }
        else
        {
            _forceToAdd = _defaultForce;
        }
        if (_canForward) _rb.AddRelativeForce(_forceToAdd * Time.fixedDeltaTime, ForceMode.Impulse);

    }
}
