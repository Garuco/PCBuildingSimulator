using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherboardManager : MonoBehaviour
{
    private bool ram = false;
    private bool m2 = false;
    private bool gpu = false;
    private bool cpu = false;

    public void SetComponents(string tag, bool isOrNot)
    {
        switch (tag)
        {
            case "CPU":
                cpu = isOrNot;
                break;
            case "M2":
                m2 = isOrNot;
                break;
            case "RAM":
                ram = isOrNot;
                break;
            case "Graphic":
                gpu = isOrNot;
                break;
        }
    }

    public List<string> GetComponents()
    {
        List<string> components = new List<string>();
        if (!cpu) components.Add("No se ha localizado el componente: CPU");
        if (!m2) components.Add("No se ha localizado el componente: Disco M.2");
        if (!ram) components.Add("No se ha localizado el componente: Memoria RAM");
        if (!gpu) components.Add("No se ha localizado el componente: Tarjeta gráfica");
        return components;
    }
}
