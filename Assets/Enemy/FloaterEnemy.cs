using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloaterEnemy : MonoBehaviour
{


    public void Die()
    {
        Destroy(gameObject);
    }
}
