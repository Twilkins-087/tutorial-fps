using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class EnemyHealthDisplay : MonoBehaviour
{
    [SerializeField] Color highColour = Color.green;
    [SerializeField] Color lowColour = Color.red;
    TextMeshPro _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
    }

    public void UpdateHealth(HealthStatus status)
    {
        _text.text = status.Health.ToString();
        _text.color = Color.Lerp(lowColour, highColour, status.Health / status.MaxHealth);
    }
}
