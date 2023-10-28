using UnityEngine;

/// <summary>
/// Handles aiming up and down shooting
/// </summary>
public class PlayerShooter : MonoBehaviour
{
    [SerializeField] Transform weaponHolster;

    [SerializeField] Transform bulletFirePoint;
    [SerializeField] Bullet bulletObj;

    private void Start()
    {
        // This makes it so the game captures the mouse
        // Without it the mouse scrolls out the game window constantly
        // Try commenting out this line
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Aim();
        Fire();

        // Having the cursor captured prevents us from
        // doing anything else in the editor
        // So this makes it so that hitting the "Cancel" button
        // restores the mouse to regular functionality
        if (Input.GetButtonDown("Cancel"))
            Cursor.lockState = CursorLockMode.None;
    }

    private void Aim()
    {
        // Using the mouse's vertical movement we rotate the weaponHolster
        // Similar to how we would turn in PlayerMovement
        // Although, would we rotate on the same axis?


        // EXTENSION

        // Are you satisfied with the aim speed?
        // How could you make the more smooth?
    }

    private void Fire()
    {
        // Upon pressing the fire button

        // We want to Instantiate a bulletObj at the position and rotation
        // of bulletFirePoint
        // Instantiate creates a copy of a prefab and places it in the game world

        // Finally we call the Fire method on the bullet, passing in the direction
        // we want to fire it at
    }

    private void OnTriggerEnter(Collider other)
    {
        // NOT FOR SESSION 1

        // Check to see if other has a Pickup component
        // If it does, assign it's Weapon field to bulletObj
        // and destroy the pickup
    }
}
