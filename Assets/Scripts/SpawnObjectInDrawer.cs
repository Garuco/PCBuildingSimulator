using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectInDrawer : MonoBehaviour
{
    public DrawerManager drawerManager;

    void OnTriggerEnter(Collider other)
    {
        // Verifica si la gaveta se cerr� (puedes ajustar esta condici�n seg�n tu l�gica de cierre)
        if (other.CompareTag("cierreGaveta"))
        {
            // La gaveta se ha cerrado, aqu� puedes realizar las acciones necesarias
            Debug.Log("La gaveta se ha cerrado");

            drawerManager.CerrarGaveta();

        }
    }
}
