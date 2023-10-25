using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    [SerializeField] HealthPool health;
    [SerializeField] float damageMultiplier = 1;

    public void TakeDamage(float damage)
    {
        var finalDamage = damage * damageMultiplier;
        health.SubtractHealth(finalDamage);
    }
}
