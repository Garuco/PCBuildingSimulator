using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketHandler : MonoBehaviour
{
    private GameObject component = null;
    public Transform socket;
    public RamComp motherboardScript;
    private bool allowed = false;
    public MotherboardManager manager;

    private XRSocketInteractor holsterSocket;
    public float activationDistance = 1.5f; // Distancia para activar el Socket


    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(HandleTriggerEnter(other));
    }

    void OnTriggerExit(Collider other)
    {
        StartCoroutine(HandleTriggerExit(other));
    }

    private IEnumerator HandleTriggerEnter(Collider other)
    {
        yield return null; // Espera un frame para evitar problemas con la cola de llamadas

        if (other.tag.Equals("RAM", StringComparison.OrdinalIgnoreCase))
        {
            Compatibilidad ram = other.GetComponent<Compatibilidad>();
            Debug.Log(ram.tipoComponente + " " + ram.frecuencia + " " + ram.capacidad);
            component = other.gameObject;
            if (ram.ObtenerTipo() == motherboardScript.tipoCompatible &&
                ram.ObtenerFrecuencia() <= motherboardScript.frecuenciaMaxima &&
                ram.ObtenerCapacidad() <= motherboardScript.capacidadMaxima)
            {
                allowed = true;
            }
            else
            {
                allowed = false;
            }
        }
        else if (other.tag.Equals("CPU", StringComparison.OrdinalIgnoreCase))
        {
            Compatibilidad cpu = other.GetComponent<Compatibilidad>();
            component = other.gameObject;
            if (motherboardScript.VerificarCompatibilidadCpu(cpu.ObtenerTipo()))
            {
                allowed = true;
            }
            else
            {
                allowed = false;
            }
        }
        else if (other.tag.Equals("Graphic", StringComparison.OrdinalIgnoreCase))
        {
            Compatibilidad graphic = other.GetComponent<Compatibilidad>();
            component = other.gameObject;
            if (motherboardScript.VerificarCompatibilidadGraphic(graphic.ObtenerTipo()))
            {
                allowed = true;
            }
            else
            {
                allowed = false;
            }
        }
        else if (other.tag.Equals("M2", StringComparison.OrdinalIgnoreCase))
        {
            Compatibilidad m2 = other.GetComponent<Compatibilidad>();
            component = m2.gameObject;
            if (motherboardScript.VerificarCompatibilidadM2(m2.ObtenerTipo()))
            {
                allowed = true;
            }
            else
            {
                allowed = false;
            }
        }
    }

    private IEnumerator HandleTriggerExit(Collider other)
    {
        yield return null; // Espera un frame para evitar problemas con la cola de llamadas

        if (other.CompareTag("RAM") && (socket.name == "RamSocket1" || socket.name == "RamSocket2" || socket.name == "RamSocket3" || socket.name == "RamSocket4"))
        {
            manager.SetComponents(other.tag, false);
        }
        else if (other.CompareTag("CPU") && socket.name == "CpuSocket")
        {
            manager.SetComponents(other.tag, false);
        }
        else if (other.CompareTag("Graphic") && socket.name == "GraphicSocket")
        {
            manager.SetComponents(other.tag, false);
        }
        else if (other.CompareTag("M2") && socket.name == "M2Socket")
        {
            manager.SetComponents(other.tag, false);
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
            Transform componentTransform = component.transform;

            float productoPunto = 0;

            if (component.tag.Equals("RAM", StringComparison.OrdinalIgnoreCase))
            {
                // Obtener la direcci�n de las caras
                Vector3 direccionCaraDelantera = transform.TransformDirection(-componentTransform.forward);
                Vector3 direccionCaraTrasera = transform.TransformDirection(socket.forward);
                // Calcular el producto punto
                productoPunto = Vector3.Dot(direccionCaraDelantera.normalized, direccionCaraTrasera.normalized);
            }
            else if (component.tag.Equals("CPU", StringComparison.OrdinalIgnoreCase))
            {
                Vector3 direccionCaraDelantera = transform.TransformDirection(-componentTransform.up);
                Vector3 direccionCaraTrasera = transform.TransformDirection(socket.forward);
                productoPunto = Vector3.Dot(direccionCaraDelantera.normalized, direccionCaraTrasera.normalized);
            }
            else if (component.tag.Equals("Graphic", StringComparison.OrdinalIgnoreCase))
            {
                Vector3 direccionCaraDelantera = transform.TransformDirection(-componentTransform.right);
                Vector3 direccionCaraTrasera = transform.TransformDirection(socket.forward);
                productoPunto = Vector3.Dot(direccionCaraDelantera.normalized, direccionCaraTrasera.normalized);
            }
            else if (component.tag.Equals("M2", StringComparison.OrdinalIgnoreCase))
            {
                Vector3 direccionCaraDelantera = transform.TransformDirection(-componentTransform.forward);
                Vector3 direccionCaraTrasera = transform.TransformDirection(socket.forward);
                productoPunto = Vector3.Dot(direccionCaraDelantera.normalized, direccionCaraTrasera.normalized);
            }

            float distance = Vector3.Distance(componentTransform.position, socket.position);
            //Debug.Log("Distancia " + distance + " productoPunto " + productoPunto);
            if (distance <= activationDistance && allowed)
            {
                // Si el producto punto es negativo, las caras est�n una frente a la otra
                if (productoPunto < -0.7)
                {
                    //Debug.Log("La cara delantera del Cubo1 est� frente a la cara trasera del Cubo2.");
                    holsterSocket.enabled = true;
                    manager.SetComponents(component.tag, true);
                    //Debug.Log("Metieron " + component.tag);
                }
                else
                {
                    //Debug.Log("Las caras no est�n una frente a la otra.");
                    holsterSocket.enabled = false;
                    manager.SetComponents(component.tag, false);
                }
            }
            else
            {
                holsterSocket.enabled = false;
                manager.SetComponents(component.tag, false);
            }
        }
    }
}