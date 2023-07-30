using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform DoorObject;
    public float xOffset;
    public float smoothingTime = 2f;

    private bool Open;
    private Vector3 closePosition;
    private Vector3 openPosition;

    private void Start()
    {
        closePosition = DoorObject.position;
        openPosition = closePosition;
        openPosition.x = xOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        DoorObject.position = Vector3.Lerp(DoorObject.position, openPosition, smoothingTime * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        DoorObject.position = Vector3.Lerp(DoorObject.position, closePosition, smoothingTime * Time.deltaTime);
    }
}

