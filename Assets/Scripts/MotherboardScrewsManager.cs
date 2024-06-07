using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class MotherboardScrewsManager : MonoBehaviour
{
    private XRSocketInteractor socketInteractor;

    private ActiveScrewCounter activeScrews;

    private GameObject motherboard = null;

    public SphereCollider rightHand;
    public SphereCollider leftHand;

    private float reactivationDelay = 2.0f;

    void Awake()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();

        if (socketInteractor != null)
        {
            // Subscribe to the events
            socketInteractor.selectEntered.AddListener(OnSelectEntered);
            socketInteractor.selectExited.AddListener(OnSelectExited);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (motherboard != null)
        {
            if (activeScrews.CountActiveSockets() >= 1 && socketInteractor.socketActive)
            {
                Physics.IgnoreCollision(motherboard.GetComponent<BoxCollider>(), rightHand, true);
                Physics.IgnoreCollision(motherboard.GetComponent<BoxCollider>(), leftHand, true);
            }
            else if (activeScrews.CountActiveSockets() == 0)
            {
                Physics.IgnoreCollision(motherboard.GetComponent<BoxCollider>(), rightHand, false);
                Physics.IgnoreCollision(motherboard.GetComponent<BoxCollider>(), leftHand, false);
            }
        }
    }


    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        motherboard = args.interactableObject.transform.gameObject;
        activeScrews = motherboard.GetComponent<ActiveScrewCounter>();
        Debug.Log("Sockets Activos: " + activeScrews.CountActiveSockets());
        if (activeScrews.CountActiveSockets() >= 1)
        {
            socketInteractor.socketActive = false;
            StartCoroutine(ReenableSocketInteractorAfterDelay());
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        motherboard = null;
        Debug.Log("Object exited the socket: " + args.interactableObject.transform.name);
    }

    private IEnumerator ReenableSocketInteractorAfterDelay()
    {
        yield return new WaitForSeconds(reactivationDelay);
        socketInteractor.socketActive = true;
        Debug.Log("Socket Interactor re-enabled after delay.");
    }

}
