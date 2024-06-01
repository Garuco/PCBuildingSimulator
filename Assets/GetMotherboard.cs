using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMotherboard : MonoBehaviour
{
    private GameObject motherboard = null;
    void OnTriggerEnter(Collider other)
    {
        // Incrementar contador de objetos cuando un objeto entra en la gaveta
        if (other.CompareTag("Motherboard"))
        {
            motherboard = other.gameObject;
            //Debug.Log("Se agrego una MB");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Decrementar contador de objetos cuando un objeto sale de la gaveta
        if (other.CompareTag("Motherboard"))
        {
            motherboard = null;
            //Debug.Log("Se quitó una MB");
        }
    }

    public GameObject getMotherboard()
    {
        return motherboard;
    }
}
