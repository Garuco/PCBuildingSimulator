using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveScrewCounter : MonoBehaviour
{
    public ScrewSocketHandler screwHandler1;
    public ScrewSocketHandler screwHandler2;
    public ScrewSocketHandler screwHandler3;
    public ScrewSocketHandler screwHandler4;



    // Función para contar la cantidad de sockets activos
    public int CountActiveSockets()
    {
        int activeCount = 0;

        if (screwHandler1 != null && screwHandler1.isActive)
        {
            activeCount++;
        }

        if (screwHandler2 != null && screwHandler2.isActive)
        {
            activeCount++;
        }

        if (screwHandler3 != null && screwHandler3.isActive)
        {
            activeCount++;
        }

        if (screwHandler4 != null && screwHandler4.isActive)
        {
            activeCount++;
        }

        return activeCount;
    }




}
