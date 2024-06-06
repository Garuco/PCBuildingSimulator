using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketController : MonoBehaviour
{
    public Transform pistola;
    public Transform funda;

    private XRSocketInteractor holsterSocket;
    public float activationDistance = 1.5f; // Distancia para activar el Socket

    // Start is called before the first frame update
    void Start()
    {
        holsterSocket = GetComponent<XRSocketInteractor>();
        holsterSocket.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener la dirección de las caras
        Vector3 direccionCaraDelantera = transform.TransformDirection(pistola.forward);
        Vector3 direccionCaraTrasera = transform.TransformDirection(-funda.forward);

        // Calcular el producto punto
        float productoPunto = Vector3.Dot(direccionCaraDelantera.normalized, direccionCaraTrasera.normalized);

        float distance = Vector3.Distance(pistola.position, funda.position);
        if (distance <= activationDistance)
        {
            // Si el producto punto es negativo, las caras están una frente a la otra
            if (productoPunto < -0.7)
            {
                Debug.Log("La cara delantera del Cubo1 está frente a la cara trasera del Cubo2.");
                holsterSocket.enabled = true;
            }
            else
            {
                Debug.Log("Las caras no están una frente a la otra.");
                holsterSocket.enabled = false;
            }
        }
        else
        {
            holsterSocket.enabled = false;
        }

    }
}
