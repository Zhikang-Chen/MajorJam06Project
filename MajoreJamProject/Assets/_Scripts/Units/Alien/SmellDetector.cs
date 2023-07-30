using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SmellDetector : MonoBehaviour
{
    public List<GameObject> ObjectSmelled = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        var s = other.GetComponent<SmellAble>();

        if(s != null)
        {
            ObjectSmelled.Add(other.gameObject);
        }
    }
}

