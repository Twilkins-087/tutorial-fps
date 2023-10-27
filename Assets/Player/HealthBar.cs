using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void UpdateHealth(HealthStatus status)
    {
        _slider.normalizedValue = status.Health / status.MaxHealth;
    }
}
