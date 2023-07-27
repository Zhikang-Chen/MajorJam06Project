using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// TD
// Add a proper ground collision check
// Make the movement less floaty 
public class PlayerMovement : MonoBehaviour
{
    //
    [Header("Movement")]
    [SerializeField]
    private Vector2 movementInput;

    [SerializeField]
    [FormerlySerializedAs("moveSpeed")]
    private float walkSpeed = 10f;

    [SerializeField]
    private float crouchSpeed = 5f;

    [SerializeField]
    private float currentSpeed;

    [SerializeField]
    private Rigidbody rigidbodyComponent;

    //
    [Header("Jumping")]
    [SerializeField]
    private float jumpForce = 100f;

    [SerializeField]
    private bool isGrounded = true;

    [SerializeField]
    private bool jumpNextFrame = false;

    //
    [Header("Crouch")]
    [SerializeField]
    private bool crouch = false;

    [SerializeField]
    private float crouchHeight = 0.5f;

    [SerializeField]
    private float standHeight;

    // Start is called before the first frame update
    private void Awake()
    {
        // the rigidbody should be set at run time but i am too lazy to do that
        rigidbodyComponent = GetComponentInChildren<Rigidbody>();
        if (!rigidbodyComponent)
            Debug.LogWarning("Player movement: rigidbody not found, the player will not be able to move");

        standHeight = transform.localScale.y;
        currentSpeed = walkSpeed;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(rigidbodyComponent)
        {
            var forwardVelocity = movementInput.x * currentSpeed * transform.forward;
            var rightVelocity = movementInput.y * currentSpeed * -transform.right;
            var upVelocity = new Vector3();
            if (jumpNextFrame && isGrounded)
            {
                upVelocity = jumpForce * transform.up;
                jumpNextFrame = false;
                isGrounded = false;
            }

            var grav = rigidbodyComponent.velocity;
            grav.x = 0;
            grav.z = 0;
            rigidbodyComponent.velocity = forwardVelocity + rightVelocity + upVelocity + grav;
        }

        var currentHeight = transform.localScale.y;
        if (crouch)
        {
            currentHeight = Mathf.Lerp(currentHeight, crouchHeight, crouchSpeed);
            transform.localScale = new Vector3(1f, currentHeight, 1f);
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentHeight = Mathf.Lerp(currentHeight, standHeight, crouchSpeed);
            transform.localScale = new Vector3(1f, currentHeight, 1f);
            currentSpeed = walkSpeed;
        }
    }

    private void Update()
    {
        // Input handling
        var yInput = -Input.GetAxis("Horizontal");
        var xInput = Input.GetAxis("Vertical");
        movementInput = new Vector2(xInput, yInput);

        if(!jumpNextFrame)
            jumpNextFrame = Input.GetKeyDown(KeyCode.Space);

        crouch = Input.GetKey(KeyCode.LeftControl);
    }

    // I am lazy so this would do for now
    // Need to change it later
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}
