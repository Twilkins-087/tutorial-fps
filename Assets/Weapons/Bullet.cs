using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float damage = 10;

    public abstract void Fire(Vector3 forward);
}
