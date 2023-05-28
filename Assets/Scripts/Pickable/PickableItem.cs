using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable
{
    [field: SerializeField]
    public bool KeepWorldPosition { get; private set; }

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public GameObject PickUp()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        if (rb.name == "Smartphone")
        {
            transform.position = new Vector3(0.07f, -0.16f, 0.13f);
            transform.rotation = Quaternion.Euler(-0.5f, -178.6f, transform.rotation.eulerAngles.z);
        }else if( rb.name == "Screwdriver")
        {
            transform.position = new Vector3(0.4f, 0f, 0f);
            transform.rotation = Quaternion.Euler(-75f, -5f, transform.rotation.eulerAngles.z);
        }
        return this.gameObject;
    }
}
