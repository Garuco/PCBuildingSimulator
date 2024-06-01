using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class GetComponents : MonoBehaviour
{
    private bool isCase = false;
    private GameObject objectCase;
    private List<string> messageList;
    // Tiempo de espera en segundos
    public float waitTime = 2.0f;

    // Método que se ejecuta cuando otro collider entra en este trigger
    private void OnTriggerEnter(Collider other)
    {
        // Inicia la corrutina que espera y luego ejecuta la acción
        objectCase= other.gameObject;
        StartCoroutine(WaitAndExecute(other));
    }

    // Corrutina que espera el tiempo especificado y luego ejecuta la lógica deseada
    private IEnumerator WaitAndExecute(Collider other)
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(waitTime);

        // Aquí pones la lógica que deseas ejecutar después de la espera
        Debug.Log("Esperó 2 segundos antes de ejecutar esta acción.");

        getComponentsOfCase();
    }
    public void getComponentsOfCase()
    {
        messageList = objectCase.GetComponent<CaseManager>().getMissingComponents();
        Debug.Log("Imprimiendo lista de mensajes desde el setup ");
        if(messageList.Count == 0 )
        {
            Debug.Log("Todos los componentes estan bien");
        }
        else
        {
            foreach (var component in messageList) { Debug.Log(component); }
        }
    }
}
