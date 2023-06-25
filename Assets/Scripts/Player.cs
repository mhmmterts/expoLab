using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    private GameObject pickUpUI;

    public GameObject backcoverUI;
    public GameObject batteryUI;
    public GameObject simCardUI;
    public GameObject schraubenUI;
    public GameObject gehause2UI;
    public GameObject simboardUI;
    public GameObject cameraUI;
    public GameObject hauptplatineUI;
    public GameObject soundCableUI;
    internal void AddHealth(int healthBoost)
    {
        Debug.Log($"Health boosted by {healthBoost}");
    }

    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private GameObject inHandItem;

    [SerializeField]
    private InputActionReference interactionInput, dropInput, useInput, openDoorInput;

    private RaycastHit hit;

    [SerializeField]
    private AudioSource pickUpSource;

    private void Start()
    {
        interactionInput.action.performed += PickUp;
        dropInput.action.performed += Drop;
        useInput.action.performed += Use;
    }

    private void Use(InputAction.CallbackContext obj)
    {
        if (inHandItem != null)
        {
            IUsable usable = inHandItem.GetComponent<IUsable>();
            if (usable != null)
            {
                usable.Use(this.gameObject);
            }
        }
    }

    private void Drop(InputAction.CallbackContext obj)
    {
        if (inHandItem != null)
        {
            Vector3 scale = inHandItem.transform.localScale;
            inHandItem.transform.SetParent(null);
            inHandItem.transform.localScale = scale;
            inHandItem = null;
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    private void PickUp(InputAction.CallbackContext obj)
    {
        if (hit.collider != null && inHandItem == null)
        {
            Debug.Log(hit.collider.name);
            IPickable pickableItem = hit.collider.GetComponent<IPickable>();
            if (pickableItem != null)
            {
                pickUpSource.Play();
                inHandItem = pickableItem.PickUp();
                inHandItem.transform.SetParent(pickUpParent.transform, pickableItem.KeepWorldPosition);
            }
        }
    }

    public GameObject getInHandItem()
    {
        return inHandItem;
    }
    //Oyuncunun elindeki itemin animasyonda kullanilmasi icin parentini null yapiyoruz
    public void setInHandItem()
    {
        Vector3 scale = inHandItem.transform.localScale;
        inHandItem.transform.SetParent(null);
        inHandItem.transform.localScale = scale;
        inHandItem = null;
    }
    public void setPickUpUIVisible()
    {
        pickUpUI.SetActive(true);
    }

    public void setPickUpUIInvisible()
    {
        pickUpUI.SetActive(false);
    }

    private void Update()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            pickUpUI.SetActive(false);
            backcoverUI.SetActive(false);
            batteryUI.SetActive(false);
            hauptplatineUI.SetActive(false);
            simCardUI.SetActive(false);
            schraubenUI.SetActive(false);
            gehause2UI.SetActive(false);
            simboardUI.SetActive(false);
            simboardUI.SetActive(false);
            cameraUI.SetActive(false);
            soundCableUI.SetActive(false);
            hauptplatineUI.SetActive(false);
        }

        if (inHandItem != null)
        {
            return;
        }

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, pickableLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            if (hit.collider.name.Equals("Backcover"))
            {
                backcoverUI.SetActive(true);
            }
            else if (hit.collider.name.Equals("Battery"))
            {
                batteryUI.SetActive(true);
            }
            else if (hit.collider.name.Equals("Hauptplatine"))
            {
                hauptplatineUI.SetActive(true);
            }
            else if (hit.collider.name.Equals("SimBoardInvisible"))
            {
                simCardUI.SetActive(true);
            }
            else if (hit.collider.name.Equals("Schrauben"))
            {
                schraubenUI.SetActive(true);
            }
            else if (hit.collider.name.Equals("Backcover2"))
            {
                gehause2UI.SetActive(true);
            }
            else if (hit.collider.name.Equals("SimBoard"))
            {
                simboardUI.SetActive(true);
            }
            else if (hit.collider.name.Equals("CameraConnector"))
            {
                cameraUI.SetActive(true);
            }
            else if (hit.collider.name.Equals("SoundCable"))
            {
                soundCableUI.SetActive(true);
            }
            else if (hit.collider.name.Equals("Hauptplatine"))
            {
                hauptplatineUI.SetActive(true);
            }
            else
            {
                pickUpUI.SetActive(true);
            }
        }


    }
}
