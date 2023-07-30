using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// TD
// Add a proper ground collision check
// Make the movement less floaty 
// Fix all the edge case about the input
// Have Different timing of the step for each walk speed 
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
    private float runSpeed = 15f;

    [SerializeField]
    private float currentSpeed;

    [SerializeField]
    private Rigidbody rigidbodyComponent;

    // What are you doing step timer?
    // I do not regret making this joke
    [SerializeField]
    private float stepTimer = 0.2f;

    private float stepTime = 0.0f;

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
    private bool isCrouch = false;

    [SerializeField]
    private float crouchHeight = 0.5f;

    [SerializeField]
    private float standHeight;

    [SerializeField]
    private float crouchRate = 0.5f;

    //
    [Header("Stamina")]
    [SerializeField]
    private bool isRunning = false;

    [SerializeField]
    private float maxStamina = 100.0f;

    [SerializeField]
    private float currentStamina;

    [SerializeField]
    private float staminaRegen = 10.0f;

    // Comsumption per second
    [SerializeField]
    private float runStaminaConsumption = 30.0f;

    [SerializeField]
    private bool isRegening = false;

    // Start is called before the first frame update
    private void Awake()
    {
        // the rigidbody should be set at run time but i am too lazy to do that
        rigidbodyComponent = GetComponentInChildren<Rigidbody>();
        if (!rigidbodyComponent)
            Debug.LogWarning("Player movement: rigidbody not found, the player will not be able to move");

        standHeight = transform.localScale.y;
        currentSpeed = walkSpeed;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Movement State
        // Enum and a swtich would better but again I am lazy
        var currentHeight = transform.localScale.y;
        if (isCrouch)
        {
            currentHeight = Mathf.Lerp(currentHeight, crouchHeight, crouchRate);
            transform.localScale = new Vector3(1f, currentHeight, 1f);
            currentSpeed = crouchSpeed;
        }
        else if(isRunning && !isCrouch && currentStamina > 0)
        {
            currentSpeed = runSpeed;
            currentStamina -= runStaminaConsumption * Time.fixedDeltaTime;
            PlayerUIManager.OnStaminaUpdate(currentStamina / maxStamina);
        }
        else
        {
            currentHeight = Mathf.Lerp(currentHeight, standHeight, crouchRate);
            transform.localScale = new Vector3(1f, currentHeight, 1f);
            currentSpeed = walkSpeed;

            if(!isRegening)
                StartCoroutine(StartStaminaRegen());
        }

        if(movementInput.magnitude > 0 && isGrounded)
        {
            // Too much work to have different step timer for each movement speed
            // so the walk speed will be the standard
            if(stepTime >= stepTimer * (walkSpeed / currentSpeed))
            {
                AudioManager.Instance.PlayAudio2D("Player_Step_1");
                stepTime = 0;
            }
            stepTime += Time.fixedDeltaTime;
        }
        else
        {
            stepTime = 0;
        }


        // Apply Movement
        if (rigidbodyComponent)
        {
            var forwardVelocity = movementInput.x * currentSpeed * transform.forward;
            var rightVelocity = movementInput.y * currentSpeed * -transform.right;
            var upVelocity = new Vector3();
            if (jumpNextFrame && isGrounded && currentStamina > 0)
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
    }

    private void Update()
    {
        // Input handling
        var yInput = -Input.GetAxis("Horizontal");
        var xInput = Input.GetAxis("Vertical");
        movementInput = new Vector2(xInput, yInput);

        if(!jumpNextFrame && isGrounded)
            jumpNextFrame = Input.GetKeyDown(KeyCode.Space);

        isCrouch = Input.GetKey(KeyCode.LeftControl);
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // Put this somewhere else
        if (Input.GetKey(KeyCode.Escape))
        {
            PlayerUIManager.OnPauseGame();
        }
    }

    // I am lazy so this would do for now
    // Need to change it later
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }


    private IEnumerator StartStaminaRegen()
    {
        isRegening = true;
        while (currentStamina < maxStamina && !isRunning)
        {
            currentStamina += staminaRegen * Time.fixedDeltaTime;
            PlayerUIManager.OnStaminaUpdate(currentStamina / maxStamina);
            yield return new WaitForFixedUpdate();
        }

        StopCoroutine("WaitForStaminaRegen");
        StopCoroutine("StartStaminaRegen");
        isRegening = false;
    }
}
