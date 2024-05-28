using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketHandler : MonoBehaviour
{
    private Transform component = null;
    public Transform socket;
    public RamComp motherboardScript;
    private bool allowed = false;

    private XRSocketInteractor holsterSocket;
    public float activationDistance = 1.5f; // Distancia para activar el Socket

    void OnTriggerEnter(Collider other)
    {
        // Incrementar contador de objetos cuando un objeto entra en la gaveta
        Compatibilidad ram = other.GetComponent<Compatibilidad>();
        Debug.Log(ram.tipoRAM + " " + ram.frecuencia + " " + ram.capacidad);
        component = other.transform;
        if (ram.ObtenerTipo() == motherboardScript.tipoCompatible && ram.ObtenerFrecuencia() <= motherboardScript.frecuenciaMaxima && ram.ObtenerCapacidad() <= motherboardScript.capacidadMaxima)
        {
            Debug.Log("RAM compatible");
            allowed = true;
        }
        else
        {
            Debug.Log("RAM no compatible");
            allowed = false;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        holsterSocket = GetComponent<XRSocketInteractor>();
        holsterSocket.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(component != null)
        {
            // Obtener la direcci�n de las caras
            Vector3 direccionCaraDelantera = transform.TransformDirection(-component.forward);
            Vector3 direccionCaraTrasera = transform.TransformDirection(socket.forward);

            // Calcular el producto punto
            float productoPunto = Vector3.Dot(direccionCaraDelantera.normalized, direccionCaraTrasera.normalized);

            float distance = Vector3.Distance(component.position, socket.position);
            if (distance <= activationDistance && allowed)
            {
                // Si el producto punto es negativo, las caras est�n una frente a la otra
                if (productoPunto < -0.7)
                {
                    //Debug.Log("La cara delantera del Cubo1 est� frente a la cara trasera del Cubo2.");
                    holsterSocket.enabled = true;
                }
                else
                {
                    //Debug.Log("Las caras no est�n una frente a la otra.");
                    holsterSocket.enabled = false;
                }
            }
            else
            {
                holsterSocket.enabled = false;
            }
        }
    }
}