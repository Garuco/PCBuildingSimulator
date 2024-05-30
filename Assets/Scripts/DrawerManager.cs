using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class DrawerManager : MonoBehaviour
{
    public GameObject objetoPrefab; // Prefab del objeto que deseas spawnear
    public Transform spawnPoint; // Punto donde deseas que aparezca el objeto
    private int objetosDentro = 0; // Variable para contar objetos dentro de la gaveta
    public string tipoComponente;
    void OnTriggerEnter(Collider other)
    {
        // Incrementar contador de objetos cuando un objeto entra en la gaveta
        if (other.CompareTag(tipoComponente))
        {
            objetosDentro++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Decrementar contador de objetos cuando un objeto sale de la gaveta
        if (other.CompareTag(tipoComponente))
        {
            objetosDentro--;
        }
    }

    public void CerrarGaveta()
    {
        // Verificar si la gaveta está vacía
        if (objetosDentro == 0)
        {
            // Spawnear nuevo objeto si la gaveta está vacía
            SpawnNuevoObjeto();
        }
    }

    void SpawnNuevoObjeto()
    {
        Instantiate(objetoPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
