using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Door))]
public class DoorLock : MonoBehaviour
{
    // Could make this better but it's just faster to make this way
    // Will have to change if we want another lock door
    static public UnityAction unlockDoor;

    Door door;
    Vector3 OpenPosition;
    private void Awake()
    {
        door = GetComponent<Door>();
        OpenPosition = door.OpenPosition;
        door.OpenPosition = Vector3.zero;

        unlockDoor = Unlock;
    }

    private void Unlock()
    {
        door.OpenPosition = OpenPosition;
    }
}
