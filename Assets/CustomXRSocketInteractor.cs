using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CustomXRSocketInteractor : XRSocketInteractor
{
    public bool objetosUnidos = true;

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        // Si la variable objetosUnidos es true, evita que los objetos se separen
        if (objetosUnidos)
        {
            return;
        }

        // De lo contrario, permite que la selección salga normalmente
        base.OnSelectExiting(args);
    }

    // Método público para cambiar el estado de objetosUnidos
    public void SetObjetosUnidos(bool estado)
    {
        objetosUnidos = estado;

        // Si la variable cambia a false y hay un objeto seleccionado, permite que se separe
        if (!estado)
        {
            OnSelectExiting(new SelectExitEventArgs
            {
                interactableObject = (UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable)firstInteractableSelected,
                interactorObject = this
            });
        }
    }
}
