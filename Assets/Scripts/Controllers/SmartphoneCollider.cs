using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartphoneCollider : MonoBehaviour
{
    bool smartphoneIsOnTheTable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (smartphoneIsOnTheTable)
        {
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.name == "Smartphone")
        {
            smartphoneIsOnTheTable = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "Smartphone")
        {
            smartphoneIsOnTheTable = false;
        }
    }

    public bool controlSmartPhonePosition()
    {
        return smartphoneIsOnTheTable;
    }

}
