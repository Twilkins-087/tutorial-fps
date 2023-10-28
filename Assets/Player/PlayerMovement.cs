using System.Collections;
using UnityEngine;

// This is an attribute. In C#, attributes describe properties
// about a field, function or in this case a class
// The RequireComponent attribute makes it so that the editor
// forces the component in typeof(XXXXX) - in this case
// CharacterController - to be present on any GameObject
// PlayerMovement is present

// These are summary tags, they are used to give a class, struct, method, etc.
// documentation. If you hover your mouse over the class name you will notice
// a pop up appears with the text between the summary tags providing a description.
// It's good practice to write documentation like this for all public classes and
// methods. It makes it easier for yourself or your team to know what does what.

/// <summary>
/// Handles player walking, ground deceleration, jumping and
/// turning
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float gravity = 9.81f;

    [SerializeField] float walkSpeed = 5f;

    [SerializeField] float jumpPower = 5f;

    [SerializeField] Transform groundCheckPoint;
    [SerializeField] float groundCheckRadius = 0.2f;

    CharacterController _characterController;

    bool _grounded = false;
    Vector3 _velocity = Vector3.zero;

    bool _canJump = true;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckGrounded();
        Gravity();

        Walk();
        Turn();
        Jump();

        // We need to get the distance we travel this frame
        // so we multiply the velocity by the time step (Time.deltaTime)
        // and use the result to call CharacterController's Move method
        var motion = _velocity * Time.deltaTime;
        var collisionFlags = _characterController.Move(motion);
        CollisionReaction(collisionFlags);
    }

    private void Gravity()
    {
        // Applying gravity is a simple operation
        // As gravity is a rate of change of velocity we need to multiply
        // the gravity variable by the timestep (Time.deltaTime)
        // and a Vector3 direction then add it to the _velocity variable
        // What direction would be appropriate?
    }

    private void Walk()
    {
        // First we get the movement direction using the Input class

        // Do we have movement? If not, exit the method
        //if ( no movement )
        //    return;

        // Then we plug the movement directions in the correct fields
        // of a movement vector
        //var movement = new Vector3(
        //    ?????,
        //    ?????,
        //    ?????
        //);

        // Then we calculate the directional speed and factor in
        // the walkSpeed field
        // We need to make sure this is relative to the direction
        // we are currently facing
        // Hint: The variables transform.forward and transform.right


        // Finally, we set the velocity
        // _velocity = ?????

        // EXTENSIONS

        // When we're in midair, we can stop falling by walking
        // How can we stop this?
        // Hint: Should we set all of x, y, and z on _velocity?

        // You can print out the player's true speed using _velocity.magnitude
        // You may notice that when travelling diagonally, the player moves
        // faster.
        // How can we fix this?

        // Right now we set the player to move at their max speed
        // How can we make the player accelerate?
        // How can we make the player accelerate to but never go faster than walkSpeed?
    }

    private void Turn()
    {
        // Using transform.Rotate, get the player to rotate based on the mouse movement
        // Keep in mind this is the MOVEMENT script so we only want the player to
        // rotate as if they are turning their neck
        // Looking up and down will be handled with a different script
        // Hint: Input.GetAxis(....)
    }

    private void Jump()
    {
        // First we need a guard
        // A method guard checks the state to see if the conditions are
        // acceptable, if not it leaves the method early
        // Under what conditions would we want to not be able to jump?

        // Now we just check to see if the jump button is pressed
        // then add the jump power
        // What field of the velocity (x, y, z) would we want to apply the jump
        // power to


        // EXTENSIONS

        // With the way we're detecting the ground, it is possible that even after
        // jumping being _grounded may still be true
        // Using a coroutine, add a timeout that prevents you from being able
        // to jump so soon
        // Hint: StartCoroutine
    }

    private IEnumerator ReJumpTimeout()
    {
        _canJump = false;
        var waitTime = 0f; // What do you think would be an appropriate value?
        yield return new WaitForSeconds(waitTime);
        _canJump = true;
    }

    private void CheckGrounded()
    {
        // Above we have fields for the check position and the check radius, which can define a sphere
        // How can we use these to get everything that overlaps that sphere?
        Collider[] overlaps = new Collider[0];

        _grounded = false;
        // Go over all the overlaps
        foreach (var overlap in overlaps)
        {
            // If the overlap sphere is too close and too large
            // the CharacterController will be included in the overlaps
            // With a simple if statement we make sure we don't consider it
            if (overlap != _characterController)
            {
                _grounded = true;
                // We only want to check if one other collder is
                // ovrlapping
                // This break statement forcibly exits the loop, in this
                // case 'foreach (var overlap in overlaps)'
                break;
            }
        }
    }

    private void CollisionReaction(CollisionFlags flags)
    {
        // Simple collision reaction
        // If a below collision has been detected
        if ((flags & CollisionFlags.Below) == CollisionFlags.Below)
        {
            // And we are moving down
            if (_velocity.y < 0)
            {
                // Set the Y velocity to zero
                _velocity.y = 0;
            }
        }
    }

    // OnDrawGizmos and OnDrawGizmosSelected allow you to draw
    // 'gizmos' in the scene. OnDrawGizmosSelected only draws when
    // the GameObject this script is on is selected.
    private void OnDrawGizmos()
    {
        if (groundCheckPoint)
        {
            // This is the ternary operator. It can be used to condense a simple
            // if statement that sets a variable, like this:
            //    if (_grounded)
            //    {
            //        Gizmos.color = Color.green;
            //    }
            //    else
            //    {
            //        Gizmos.color = Color.grey;
            //    }
            // into one line:
            Gizmos.color = _grounded ? Color.green : Color.grey;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }
}
