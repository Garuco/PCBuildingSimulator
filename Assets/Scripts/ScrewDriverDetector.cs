using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewDriverDetector : MonoBehaviour
{
    private bool isDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Screwdriver", StringComparison.OrdinalIgnoreCase))
        {
            isDetected = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag.Equals("Screwdriver", StringComparison.OrdinalIgnoreCase))
        {
            isDetected = false;
        }
    }

    public bool getDetection()
    {
        Debug.Log(isDetected);
        return isDetected;
    }

}
