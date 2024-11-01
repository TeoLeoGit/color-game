using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _speed = 20f;
    [SerializeField] float _steer = 30f;
    [SerializeField] Transform _target;
    [SerializeField] Transform _firstDestinate;

    private Transform _originalTarget;
    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();    
    }

    public void SetTarget(Transform target, Transform firstDestinate)
    {
        _originalTarget = target;
        _firstDestinate = firstDestinate;
        _target = _firstDestinate;
    }

    void DestroyProjectile()
    {
        if(_target.TryGetComponent<Ground>(out Ground ground))
        {
            //Do sth.
            ground.CallColumnAttack();
            Destroy(gameObject);
        }
    }
 
    private void FixedUpdate()
    {
        _rb.velocity = transform.up * _speed * Time.fixedDeltaTime * 10f;
        Vector2 direction = (_target.position - transform.position).normalized;
        var rotationSpeed = Vector3.Cross(transform.up, direction).z;
        _rb.angularVelocity = rotationSpeed * _steer * 10f;
        if (Vector2.Distance(transform.position, _target.position) < 1f)
        {
            if (_target == _originalTarget)
            {
                DestroyProjectile();
            } 
            else
            {
                _target = _originalTarget;
            }
        }
    }
}
