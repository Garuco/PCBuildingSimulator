using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectInDrawer : MonoBehaviour
{
    public DrawerManager drawerManager;

    void OnTriggerEnter(Collider other)
    {
        // Verifica si la gaveta se cerró (puedes ajustar esta condición según tu lógica de cierre)
        if (other.CompareTag("cierreGaveta"))
        {
            // La gaveta se ha cerrado, aquí puedes realizar las acciones necesarias
            Debug.Log("La gaveta se ha cerrado");

            drawerManager.CerrarGaveta();

        }
    }
}
