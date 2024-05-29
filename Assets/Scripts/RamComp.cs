using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RamComp : MonoBehaviour
{
    public string tipoCompatible;
    public int frecuenciaMaxima;
    public int capacidadMaxima;  // en GB
    public List<string> cpus;
    public List<string> graphicCards;
    public List<string> M2list;

    // M�todo para verificar la compatibilidad
    public bool VerificarCompatibilidad(GameObject ramObject)
    {
        Compatibilidad ram = ramObject.GetComponent<Compatibilidad>();
        if (ram != null)
        {
            if (ram.ObtenerTipo() == tipoCompatible && ram.ObtenerFrecuencia() <= frecuenciaMaxima && ram.ObtenerCapacidad() <= capacidadMaxima)
            {
                Debug.Log("RAM compatible");
                return true;
            }
            else
            {
                Debug.Log("RAM no compatible");
                return false;
            }
        }
        Debug.Log("No se encontr� el componente de compatibilidad en la RAM");
        return false;
    }

    public bool VerificarCompatibilidadCpu(string cpuName)
    {
        foreach (string cpu in cpus)
        {
            if(cpu.Equals(cpuName, StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("las disponibles: " + cpu);
                return true;
            }
        }
        return false;
    }

    public bool VerificarCompatibilidadGraphic(string graphicName)
    {
        foreach (string graphic in graphicCards)
        {
            if (graphic.Equals(graphicName, StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("las disponibles: " + graphic);
                return true;
            }
        }
        return false;
    }

    public bool VerificarCompatibilidadM2(string m2Name)
    {
        foreach (string m2 in M2list)
        {
            if (m2.Equals(m2Name, StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("las disponibles: " + m2);
                return true;
            }
        }
        return false;
    }

}