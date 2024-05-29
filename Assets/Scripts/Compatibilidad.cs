using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compatibilidad : MonoBehaviour
{
    public string tipoComponente;
    public int frecuencia;
    public int capacidad;  // en GB

    // M�todo para obtener las propiedades de la RAM
    public string ObtenerTipo()
    {
        return tipoComponente;
    }

    public int ObtenerFrecuencia()
    {
        return frecuencia;
    }

    public int ObtenerCapacidad()
    {
        return capacidad;
    }
}