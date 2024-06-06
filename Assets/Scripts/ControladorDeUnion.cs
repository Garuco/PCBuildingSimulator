using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ControladorDeUnion : MonoBehaviour
{
    private XRSocketInteractor socketInteractor;
    public bool objetosUnidos = false;

    void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        // Suscribirse al evento de selección
        socketInteractor.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs arg)
    {
        if (objetosUnidos)
        {
            // Desactivar la capacidad del socket de soltar el objeto
            socketInteractor.allowSelect = false;
        }
    }

    // Método para cambiar la capacidad de unión de los objetos
    public void SetObjetosUnidos(bool estado)
    {
        objetosUnidos = estado;
        socketInteractor.allowSelect = !estado;
        Debug.Log("Se cambio desde controlador de union");
    }
}
