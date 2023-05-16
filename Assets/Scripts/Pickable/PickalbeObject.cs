using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickalbeObject : MonoBehaviour, IPickable
{
    [field: SerializeField]
    public bool KeepWorldPosition { get; private set; } = true;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public GameObject PickUp()
    {
        //it is for the chest item
        if (rb != null)
        { 
            rb.isKinematic = true;
        }
        transform.rotation = Quaternion.identity;

        return gameObject;
    }
}
