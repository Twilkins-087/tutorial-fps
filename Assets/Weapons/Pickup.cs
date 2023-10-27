using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Bullet weapon;

    public Bullet Weapon => weapon;
}
