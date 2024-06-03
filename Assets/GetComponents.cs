using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class GetComponents : MonoBehaviour
{
    private GameObject objectCase;
    private List<string> messageList = null;
    public MonitorController monitorController;
    // Tiempo de espera en segundos
    public float waitTime = 2.0f;

    // Método que se ejecuta cuando otro collider entra en este trigger
    private void OnTriggerEnter(Collider other)
    {
        // Inicia la corrutina que espera y luego ejecuta la acción
        if (other.CompareTag("Case"))
        {
            objectCase= other.gameObject;
            StartCoroutine(WaitAndExecute());
        }
            
        
    }

    // Método que se ejecuta cuando otro collider SALE en este trigger
    private void OnTriggerExit(Collider other)
    {
        // Inicia la corrutina que espera y luego ejecuta la acción
        if (other.CompareTag("Case"))
        {
            objectCase = null;
            monitorController.SetBlackMaterial();
        }
            

    }


    private void Update()
    {
        if (objectCase != null) { StartCoroutine(WaitAndExecute()); }
    }

    // Corrutina que espera el tiempo especificado y luego ejecuta la lógica deseada
    private IEnumerator WaitAndExecute()
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(waitTime);

        getComponentsOfCase();
    }
    public void getComponentsOfCase()
    {
        messageList = objectCase?.GetComponent<CaseManager>().getMissingComponents();
        Debug.Log("Imprimiendo lista de mensajes desde el setup ");
        if(messageList.Count == 0 )
        {
            Debug.Log("Todos los componentes estan bien");
            monitorController.SetDeskMaterial();
        }
        else
        {
            monitorController.SetBlueScreenMaterial(messageList);
            foreach (var component in messageList) { Debug.Log(component); }
        }
    }
}
