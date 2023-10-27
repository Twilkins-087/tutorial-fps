using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BodyDamage : MonoBehaviour
{
    [SerializeField] float damage = 30;
    [SerializeField] UnityEvent onDealDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        var damageTaker = other.GetComponent<DamageTaker>();
        if (damageTaker)
        {
            damageTaker.TakeDamage(damage);
            onDealDamage.Invoke();
        }
    }
}
