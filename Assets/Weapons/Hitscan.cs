using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitscan : Bullet
{
    [SerializeField] float distance = 10;
    [SerializeField] LayerMask hitLayer;

    public override void Fire(Vector3 forward)
    {
        Ray hitscan = new Ray(transform.position, forward);
        RaycastHit[] hits = Physics.RaycastAll(hitscan, distance, hitLayer);

        if (hits.Length > 0)
        {
            var hit = hits[0];
            //var damageTaker = hit.collider.GetComponent<DamageTaker>()
            //damageTaker.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
