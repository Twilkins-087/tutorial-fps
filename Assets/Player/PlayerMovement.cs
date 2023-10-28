using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float gravity = 9.81f;

    [SerializeField] float walkAcceleration = 10f;
    [SerializeField] float walkSpeed = 5f;

    [SerializeField] float jumpPower = 5f;

    [SerializeField] Transform groundCheckPoint;
    [SerializeField] float groundCheckRadius = 0.2f;

    CharacterController _characterController;

    [SerializeField] bool _grounded = false;
    [SerializeField] Vector3 _velocity = Vector3.zero;

    bool _canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        Gravity();
        PlanarDecelerate();

        Walk();
        Turn();
        Jump();

        var motion = _velocity * Time.deltaTime;
        var collisionFlags = _characterController.Move(motion);
        CollisionResolution(collisionFlags);
    }

    void Gravity()
    {
        _velocity += Vector3.down * gravity * Time.deltaTime;
    }

    void Walk()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var moveWalk = Mathf.Approximately(vertical, 0) ? 0 : (int)Mathf.Sign(vertical);
        var moveStrafe = Mathf.Approximately(horizontal, 0) ? 0 : (int)Mathf.Sign(horizontal);

        var planarVelocity = Vector3.zero;
        planarVelocity.x = Vector3.Dot(_velocity, transform.right);
        planarVelocity.z = Vector3.Dot(_velocity, transform.forward);
        if (planarVelocity.magnitude > walkSpeed && Vector3.Dot(new Vector3(moveStrafe, 0, moveWalk), planarVelocity) > 0)
        {
            moveStrafe = 0;
            moveWalk = 0;
        }
        else
        {
            if (!Mathf.Approximately(0, planarVelocity.z)
                && Mathf.Approximately(moveWalk, Mathf.Sign(planarVelocity.z))
                && Mathf.Abs(planarVelocity.z) > walkSpeed)
                moveWalk = 0;
            if (!Mathf.Approximately(0, planarVelocity.x)
                && Mathf.Approximately(moveStrafe, Mathf.Sign(planarVelocity.x))
                && Mathf.Abs(planarVelocity.x) > walkSpeed)
                moveStrafe = 0;
        }

        if (moveStrafe == 0 && moveWalk == 0)
            return;

        var vecMove = new Vector3(moveStrafe, 0, moveWalk);
        vecMove /= vecMove.magnitude;

        var trueDirection = transform.forward * vecMove.z + transform.right * vecMove.x;
        _velocity += trueDirection * walkAcceleration * Time.deltaTime;
    }

    void Turn()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
    }

    void Jump()
    {
        if (!_grounded || !_canJump)
            return;

        // Noticing an odd pause between landing and jumping when the jump
        // key is held down

        // With GetKey rather than GetKeyDown we get easy bunny hops
        if (Input.GetButton("Jump"))
        {
            _velocity.y += jumpPower;
            StartCoroutine(ReJumpTimeout());
        }
    }

    IEnumerator ReJumpTimeout()
    {
        _canJump = false;
        yield return new WaitForSeconds(0.2f);
        _canJump = true;
    }

    void PlanarDecelerate()
    {
        if (AnyMovementKeys())
            return;

        var deceleration = Vector3.zero;
        deceleration.x = -Mathf.Sign(_velocity.x);
        deceleration.z = -Mathf.Sign(_velocity.z);

        _velocity +=  walkAcceleration * 0.7f * Time.deltaTime * deceleration;
        if (Mathf.Abs(_velocity.x) < 0.01f)
            _velocity.x = 0;
        if (Mathf.Abs(_velocity.z) < 0.01f)
            _velocity.z = 0;
    }

    void CheckGrounded()
    {
        var overlaps = Physics.OverlapSphere(groundCheckPoint.position, groundCheckRadius);

        _grounded = false;
        foreach (var overlap in overlaps)
        {
            if (overlap != _characterController)
            {
                _grounded = true;
                break;
            }
        }
    }

    void CollisionResolution(CollisionFlags flags)
    {
        if ((flags & CollisionFlags.Below) == CollisionFlags.Below)
        {
            if (_velocity.y < 0) _velocity.y = 0;
        }
    }

    bool AnyMovementKeys() => Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

    private void OnDrawGizmos()
    {
        if (groundCheckPoint)
        {
            Gizmos.color = _grounded ? Color.green : Color.grey;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }
}
