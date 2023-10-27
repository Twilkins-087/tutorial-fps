using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StandardEnemy : Enemy
{
    NavMeshAgent _nmAgent;

    private void Start()
    {
        _nmAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target != null)
            _nmAgent.SetDestination(target.position);
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
