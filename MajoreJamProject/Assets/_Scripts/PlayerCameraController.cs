using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TD:
// mouse smoothing 
// use the new input system
// better sensitivity handling
public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private PlayerMovement playerMovementComp;

    [SerializeField]
    private Vector3 mouseInput;

    [SerializeField]
    public Vector2 sensitivity;


    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        if (!playerCamera)
            playerCamera = Camera.main;

        playerMovementComp = GetComponentInChildren<PlayerMovement>();
        if (!playerMovementComp)
            Debug.Log("Player camera controller: player movement component not found");

    }

    private void Update()
    {
        // Could just put GetAxis in the vector it self but I have it this way so if for some reason we want controller input it would also work 
        var yInput = -Input.GetAxis("Mouse Y");
        var xInput = Input.GetAxis("Mouse X");
        mouseInput = new Vector3(yInput * sensitivity.x, xInput * sensitivity.y, 0.0f);

        // Rotate the camera base on the mouse input and the sensitivity
        // The pain of working with quaternion is too much so I had to convert them into euler
        // then convert it back to quaternion again
        Cursor.lockState = CursorLockMode.Locked;
        var lookDirection = playerCamera.transform.localRotation.eulerAngles + new Vector3(mouseInput.x, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(lookDirection);


        //Quaternion.LookRotation(playerCamera.transform.forward, Vector3.up)
        //playerMovementComp.transform.localRotation
        if (playerMovementComp)
        {
            var playerDirection = playerCamera.transform.eulerAngles + new Vector3(0,mouseInput.y);
            Debug.Log(playerDirection);
            var rot = new Vector3(0, playerDirection.y, 0);
            playerMovementComp.transform.rotation = Quaternion.Euler(rot);

        }
    }
}