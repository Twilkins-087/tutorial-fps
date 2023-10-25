using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Orb : Bullet
{
    [SerializeField] float fireSpeed = 4;
    [SerializeField] float aliveTime = 6f;

    Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        aliveTime -= Time.deltaTime;
        if (aliveTime < 0)
            Destroy(gameObject);
    }

    public override void Fire(Vector3 forward)
    {
        if (!_rigidbody)
            _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.AddForce(forward * fireSpeed, ForceMode.VelocityChange);
    }
}
