using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SmellDetector : MonoBehaviour
{
    public Dictionary<GameObject, PLANT_TYPE> plantsInRange = new Dictionary<GameObject, PLANT_TYPE>();

    private void OnTriggerEnter(Collider other)
    {
        Plant p = other.GetComponent<Plant>();

        if (p != null)
        {
            plantsInRange.Add(other.gameObject, p.plantType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Plant p = other.GetComponent<Plant>();

        if (p != null)
        {
            plantsInRange.Remove(other.gameObject);
        }
    }
}

