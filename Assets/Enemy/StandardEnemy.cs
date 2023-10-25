using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class StandardEnemy : MonoBehaviour
{
    [SerializeField] Transform _target;

    NavMeshAgent _nmAgent;

    private void Start()
    {
        _nmAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _nmAgent.SetDestination(_target.position);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
