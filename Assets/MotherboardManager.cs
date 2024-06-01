using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherboardManager : MonoBehaviour
{
    private bool ram = false;
    private bool m2 = false;
    private bool gpu = false;
    private bool cpu = false;   

    public void setComponents(string tag, bool isOrNot)
    {
        if (tag == "CPU")
        {
            cpu = isOrNot;
        }
        else if(tag == "M2")
        {
            m2 = isOrNot;
        }
        else if (tag == "RAM")
        {
            ram = isOrNot;
        }
        else if(tag == "Graphic")
        {
            //Debug.Log("GPU se cambio a " + isOrNot.ToString());
            gpu = isOrNot;
        }
    }

    public List<string> getComponents()
    {
        List<string> components = new List<string>();
        if (!cpu)
        {
            components.Add("No se ha localizado el componente: CPU");
        }
        if (!m2)
        {
            components.Add("No se ha localizado el componente: Disco M.2");
        }
        if (!ram)
        {
            components.Add("No se ha localizado el componente: Memoria RAM");
        }
        if (!gpu)
        {
            components.Add("No se ha localizado el componente: Tarjeta gráfica");
        }
        return components;
    }
}
