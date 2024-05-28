using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketHandler : MonoBehaviour
{
    public Transform component; // Este será el componente RAM
    public Transform socket;    // Este es el socket en la motherboard
    public RamComp motherboardScript;

    private XRSocketInteractor holsterSocket;
    public float activationDistance = 1.5f; // Distancia para activar el Socket

    void Start()
    {
        Debug.Log("SocketHandler Start");
        holsterSocket = GetComponent<XRSocketInteractor>();
        holsterSocket.enabled = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(component.position, socket.position);
        if (distance <= activationDistance && AreComponentsFacingEachOther() && IsRamCompatible())
        {
            Debug.Log("Activando Socket");
            holsterSocket.enabled = true;
        }
        else
        {
            Debug.Log(distance + " " + AreComponentsFacingEachOther() + " " + IsRamCompatible());
            Debug.Log("Desactivando Socket");
            holsterSocket.enabled = false;
        }
    }

    private bool AreComponentsFacingEachOther()
    {

        Vector3 frontDirection = transform.TransformDirection(-component.forward);
        Vector3 backDirection = transform.TransformDirection(socket.forward);
        float dotProduct = Vector3.Dot(frontDirection.normalized, backDirection.normalized);

        return dotProduct < -0.7; // Cara delantera del componente está frente a la cara trasera del socket
    }

    private bool IsRamCompatible()
    {
        Debug.Log("Verificando compatibilidad");
        Compatibilidad ramScript = component.GetComponent<Compatibilidad>();
        Debug.Log("Verificando compatibilidad de la RAM" + ramScript.capacidad);
        Debug.Log("Verificando compatibilidad de la RAM" + motherboardScript.capacidadMaxima);
        Debug.Log("Verificando compatibilidad de la RAM" + motherboardScript.frecuenciaMaxima);
        Debug.Log("Verificando compatibilidad de la RAM" + motherboardScript.tipoCompatible);
        if (ramScript != null && motherboardScript != null)
        {
            Debug.Log("Verificando compatibilidad de la RAM");
            return motherboardScript.VerificarCompatibilidad(component.gameObject);
        }
        return false;
    }
}
