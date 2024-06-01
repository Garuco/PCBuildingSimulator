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
        //Debug.Log("Tratando de pegar una "+ other.tag);
        if (other.tag.Equals("RAM", StringComparison.OrdinalIgnoreCase))
        {
            // Incrementar contador de objetos cuando un objeto entra en la gaveta
            Compatibilidad ram = other.GetComponent<Compatibilidad>();
            Debug.Log(ram.tipoComponente + " " + ram.frecuencia + " " + ram.capacidad);
            component = other.gameObject;
            if (ram.ObtenerTipo() == motherboardScript.tipoCompatible && ram.ObtenerFrecuencia() <= motherboardScript.frecuenciaMaxima && ram.ObtenerCapacidad() <= motherboardScript.capacidadMaxima)
            {
                //Debug.Log("RAM compatible");
                allowed = true;
            }
            else
            {
                //Debug.Log("RAM no compatible");
                allowed = false;
            }
        }
        else if (other.tag.Equals("CPU", StringComparison.OrdinalIgnoreCase))
        {
            Compatibilidad cpu = other.GetComponent<Compatibilidad>();
            component = other.gameObject;
            if (motherboardScript.VerificarCompatibilidadCpu(cpu.ObtenerTipo()))
            {

                //Debug.Log("CPU compatible");
                allowed = true;
            }
            else
            {
                //Debug.Log("CPU no compatible" + cpu.ObtenerTipo());
                allowed = false;
            }
        }
        else if (other.tag.Equals("Graphic", StringComparison.OrdinalIgnoreCase))
        {
            Compatibilidad graphic = other.GetComponent<Compatibilidad>();
            component = other.gameObject;
            if (motherboardScript.VerificarCompatibilidadGraphic(graphic.ObtenerTipo()))
            {

                //Debug.Log("Graphic compatible");
                allowed = true;
            }
            else
            {
                //Debug.Log("Graphic no compatible" + graphic.ObtenerTipo());
                allowed = false;
            }
        }
        else if (other.tag.Equals("M2", StringComparison.OrdinalIgnoreCase))
        {
            Compatibilidad m2 = other.GetComponent<Compatibilidad>();
            component = m2.gameObject;
            if (motherboardScript.VerificarCompatibilidadM2(m2.ObtenerTipo()))
            {

                //Debug.Log("M2 compatible"); 
                allowed = true;
            }
            else
            {
                //Debug.Log("M2 no compatible" + m2.ObtenerTipo());
                allowed = false;
            }
        }

    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Sacaron " + other.tag);
        manager.setComponents(other.gameObject.tag, false);
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
                    manager.setComponents(component.tag, true);
                    //Debug.Log("Metieron " + component.tag);
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