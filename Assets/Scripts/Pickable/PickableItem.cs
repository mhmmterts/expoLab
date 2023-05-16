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
            transform.position = new Vector3(0.05f, 0.2f, 0f);
            transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        }
        return this.gameObject;
    }
}
