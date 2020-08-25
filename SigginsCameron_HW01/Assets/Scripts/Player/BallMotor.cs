using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMotor : MonoBehaviour {

    [SerializeField] float _maxSpeed = 10f;
    public float MaxSpeed
    {
        get => _maxSpeed;
        set => _maxSpeed = value;
    }
    
    public Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 movement)
    {
        // if the player's current speed is faster than we want
        if (_rb.velocity.magnitude < _maxSpeed)
        {
            _rb.AddForce(movement * _maxSpeed);
        }
    }
}
