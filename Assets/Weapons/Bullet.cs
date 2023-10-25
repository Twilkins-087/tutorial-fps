using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float damage = 10;

    protected abstract float Damage { get; }

    public abstract void Fire(Vector3 forward);

    private void OnTriggerEnter(Collider other)
    {
        DamageTaker damageTaker = other.GetComponent<DamageTaker>();
        if (!damageTaker)
            return;

        damageTaker.TakeDamage(Damage);
    }
}
