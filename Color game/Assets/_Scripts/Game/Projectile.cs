using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _speed = 20f;
    [SerializeField] float _steer = 30f;
    [SerializeField] Transform _target;

    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();    
    }
 
    private void FixedUpdate()
    {
        _rb.velocity = transform.up * _speed * Time.fixedDeltaTime * 10f;
        var direction = (_target.position - transform.position).normalized;
        var rotationSpeed = Vector3.Cross(transform.up, direction).z;
        _rb.angularVelocity = -rotationSpeed * _steer * 10f;
    }
}
