using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RamComp : MonoBehaviour
{
    public string tipoCompatible;
    public int frecuenciaMaxima;
    public int capacidadMaxima;  // en GB

    // Método para verificar la compatibilidad
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
        Debug.Log("No se encontró el componente de compatibilidad en la RAM");
        return false;
    }
}
