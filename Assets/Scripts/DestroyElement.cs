using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyElement : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if ((!other.CompareTag("Player")) && (!other.CompareTag("Case")))
        {
            Debug.Log("Elemento " + other.name + " eliminado");
            Destroy(other.gameObject);
        }
        
    }
}
