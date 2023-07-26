using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 movementInput;


    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private Rigidbody rigidbodyComponent;

    [SerializeField]
    public Vector3 lookDirection;

    // Start is called before the first frame update
    private void Awake()
    {
        // the rigidbody should be set at run time but i am too lazy to do that
        rigidbodyComponent = GetComponentInChildren<Rigidbody>();
        if (!rigidbodyComponent)
            Debug.LogWarning("Player movement: rigidbody not found, the player will not be able to move");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(rigidbodyComponent)
        {
            var forwardVelocity = movementInput.x * moveSpeed * transform.forward;
            var rightVelocity = movementInput.y * moveSpeed * -transform.right;
            rigidbodyComponent.velocity = (forwardVelocity + rightVelocity);
        }
    }

    private void Update()
    {
        var yInput = -Input.GetAxis("Horizontal");
        var xInput = Input.GetAxis("Vertical");
        movementInput = new Vector2(xInput, yInput);
    }
}
