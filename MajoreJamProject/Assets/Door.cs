using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform DoorObject;
    public Vector3 OpenPosition;
    public float smoothingTime = .3f;

    private bool Open;
    private Vector3 closePosition;
    private Vector3 targetPosition;

    private void Start()
    {
        closePosition = DoorObject.localPosition;
        targetPosition = closePosition; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Open)
        {
            Open = true;
            StartCoroutine(MoveDoor(targetPosition, OpenPosition));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Open)
        {
            Open = false;
            StartCoroutine(MoveDoor(targetPosition, closePosition));
        }
    }

    private IEnumerator MoveDoor(Vector3 start, Vector3 end)
    {
        float elapsedTime = 0f;
        while (elapsedTime < smoothingTime)
        {
            DoorObject.localPosition = Vector3.Lerp(start, end, elapsedTime / smoothingTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        DoorObject.localPosition = end; 
        targetPosition = end; 
    }
}
