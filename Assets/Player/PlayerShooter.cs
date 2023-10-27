using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] Transform weaponHolster;
    [SerializeField] float sensitivity = 1f;

    [SerializeField] Transform bulletFirePoint;
    [SerializeField] Bullet bullet;

    void Start()
    {
        // TODO Export handling of mouse state to a manager script
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Aim();
        Fire();
    }

    void Aim()
    {
        var rotate = Input.GetAxis("Mouse Y") * sensitivity;
        weaponHolster.Rotate(new Vector3(-rotate, 0, 0));
    }

    void Fire()
    {
        if (!bullet)
            return;

        if (!Input.GetButtonDown("Fire1"))
            return;

        var bulletObj = Instantiate<Bullet>(bullet, bulletFirePoint.position, bulletFirePoint.rotation);
        bulletObj.Fire(bulletFirePoint.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        var pickup = other.GetComponent<Pickup>();
        if (pickup)
        {
            bullet = pickup.Weapon;
            Destroy(pickup.gameObject);
        }
    }
}
