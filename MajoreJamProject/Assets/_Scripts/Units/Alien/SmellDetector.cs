using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SmellDetector : MonoBehaviour
{
    public bool PlantInRange;
    public GameObject NearestSmell;

    private void OnTriggerEnter(Collider other)
    {
        var s = other.GetComponent<SmellAble>();

        if(s != null)
        {
            NearestSmell = other.gameObject;
            PlantInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        NearestSmell = other.gameObject;
        PlantInRange = false;
    }
}

