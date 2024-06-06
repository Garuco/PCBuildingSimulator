using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GetMotherboard : MonoBehaviour
{
    private GameObject motherboard = null;
    private ControladorDeUnion unionHandler;
    private CustomXRSocketInteractor customXRSocket;
    void OnTriggerEnter(Collider other)
    {
        // Incrementar contador de objetos cuando un objeto entra en la gaveta
        if (other.CompareTag("Motherboard"))
        {
            motherboard = other.gameObject;
            StartCoroutine(WaitAndExecute());
        }
    }

    private IEnumerator WaitAndExecute()
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(4.0f);

        //unionHandler = GetComponent<ControladorDeUnion>();
        //unionHandler?.SetObjetosUnidos(true);
        customXRSocket = GetComponent<CustomXRSocketInteractor>();
        customXRSocket?.SetObjetosUnidos(true);
    }

    void OnTriggerExit(Collider other)
    {
        // Decrementar contador de objetos cuando un objeto sale de la gaveta
        if (other.CompareTag("Motherboard"))
        {
            motherboard = null;
            //Debug.Log("Se quitó una MB");
        }
    }

    public GameObject getMotherboard()
    {
        return motherboard;
    }
}
