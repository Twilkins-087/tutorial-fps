using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct HealthStatus
{
    public float MaxHealth;
    public float Health;
}

[System.Serializable]
public class HealthChangeEvent : UnityEvent<HealthStatus> { }

public class HealthPool : MonoBehaviour
{
    [SerializeField] float startingHealth = 100;

    [SerializeField] UnityEvent onDied;
    [SerializeField] HealthChangeEvent onHealthChanged;

    float _health;

    void Start()
    {
        _health = startingHealth;
        InvokeHealthChange();
    }

    public void SubtractHealth(float amount)
    {
        _health -= amount;
        if (_health < 0)
            _health = 0;

        InvokeHealthChange();

        if (_health == 0)
            onDied.Invoke();
    }

    public void AddHealth(float amount)
    {
        _health += amount;
        InvokeHealthChange();
    }

    void InvokeHealthChange()
    {
        onHealthChanged.Invoke(
            new HealthStatus { MaxHealth = startingHealth, Health = _health }
        );
    }
}
