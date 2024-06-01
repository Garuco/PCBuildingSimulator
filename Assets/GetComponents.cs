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

    // M�todo que se ejecuta cuando otro collider entra en este trigger
    private void OnTriggerEnter(Collider other)
    {
        // Inicia la corrutina que espera y luego ejecuta la acci�n
        objectCase= other.gameObject;
        StartCoroutine(WaitAndExecute(other));
    }

    // Corrutina que espera el tiempo especificado y luego ejecuta la l�gica deseada
    private IEnumerator WaitAndExecute(Collider other)
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(waitTime);

        // Aqu� pones la l�gica que deseas ejecutar despu�s de la espera
        Debug.Log("Esper� 2 segundos antes de ejecutar esta acci�n.");

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
