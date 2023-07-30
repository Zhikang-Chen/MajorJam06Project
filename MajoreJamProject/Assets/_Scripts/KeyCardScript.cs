using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (DoorLock.unlockDoor != null)
        {
            DoorLock.unlockDoor.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
