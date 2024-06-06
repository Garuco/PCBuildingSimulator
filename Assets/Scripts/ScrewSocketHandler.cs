using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;
using System.Xml.Schema;
using System.ComponentModel;
using System;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Oculus.Interaction;

public class ScrewSocketHandler : MonoBehaviour
{
    private XRSocketInteractor socketInteractor;

    public BoxCollider MotherboardBoxCollider;

    public Transform attatchPointTransform;

    public float speed = 1f; // Degrees per second
    public float targetRotationX = 360f; // Target Y rotation value

    private GameObject screw;

    private bool movingDown = true;


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

    void OnDestroy()
    {
        if (socketInteractor != null)
        {
            // Unsubscribe from the events
            socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
            socketInteractor.selectExited.RemoveListener(OnSelectExited);
        }
    }

    void Update()
    {
        if (screw!= null)
        {
            ScrewDriverDetector detectionComponent = screw.GetComponent<ScrewDriverDetector>();

            Vector3 localPosition = attatchPointTransform.localPosition;

            Debug.Log(localPosition.z);

            Debug.Log(screw.name);
            if (detectionComponent.getDetection() == true)
            {

                if (movingDown)
                {
                    attatchPointTransform.Translate(-Vector3.up * Time.deltaTime * speed);
                         
                    if (attatchPointTransform.localPosition.z <= -0.0047)
                    {
                        movingDown = false;
                    }
                }
                else
                {

                    attatchPointTransform.Translate(Vector3.up * Time.deltaTime * speed);

                    if (attatchPointTransform.localPosition.z >= -0.0002)
                    {
                        movingDown = true;
                    }

                }
                attatchPointTransform.Rotate(new Vector3(0f, -500f, 0f) * Time.deltaTime);


            }
        }

        
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        screw = args.interactableObject.transform.gameObject;
        if (screw != null && MotherboardBoxCollider != null)
        {
            Physics.IgnoreCollision(screw.GetComponent<BoxCollider>(), MotherboardBoxCollider);
        }
        //Physics.IgnoreCollision(screw.GetComponent<BoxCollider>(), this.GetComponent<BoxCollider>());
        Debug.Log("Object entered the socket: " + screw.name);
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        screw = null;
        Debug.Log("Object exited the socket: " + args.interactableObject.transform.name);
    }
}
