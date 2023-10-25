using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDamage : MonoBehaviour
{
    [SerializeField] float damage = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        var damageTaker = other.GetComponent<DamageTaker>();
        if (damageTaker)
            damageTaker.TakeDamage(damage);
    }
}
