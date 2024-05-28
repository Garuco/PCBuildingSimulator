using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerManager : MonoBehaviour
{
    public GameObject objetoPrefab; // Prefab del objeto que deseas spawnear
    public Transform spawnPoint; // Punto donde deseas que aparezca el objeto
    private int objetosDentro = 0; // Variable para contar objetos dentro de la gaveta

    void OnTriggerEnter(Collider other)
    {
        // Incrementar contador de objetos cuando un objeto entra en la gaveta
        if (other.CompareTag("Objeto"))
        {
            objetosDentro++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Decrementar contador de objetos cuando un objeto sale de la gaveta
        if (other.CompareTag("Objeto"))
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
