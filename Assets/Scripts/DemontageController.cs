using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemontageController : MonoBehaviour
{
    private Dictionary<string, bool> demontageSchritte = new Dictionary<string, bool>();
    private Player player;
    private SmartphoneCollider smartphoneCollider;
    bool playerIsHere;
    public GameObject step1UI;
    public GameObject step2UI;
    public GameObject step3UI;
    public GameObject step4UI;
    public GameObject step5UI;
    public GameObject step6UI;
    public GameObject step7UI;
    public GameObject step8UI;
    public GameObject backcover;
    public GameObject step9UI;
    public GameObject step10UI;
    public GameObject step11UI;
    public GameObject step12UI;
    public GameObject step13UI;
    public GameObject step14UI;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        GameObject placeTrigger = GameObject.Find("SmartphonePlaceTrigger");
        player = playerObject.GetComponent<Player>();
        smartphoneCollider = placeTrigger.GetComponent<SmartphoneCollider>();

        demontageSchritte.Add("Backcover", false);
        demontageSchritte.Add("Battery", false);
        demontageSchritte.Add("MicroSDcard", false);
        demontageSchritte.Add("10screws", false);
        demontageSchritte.Add("Backcover2", false);
        demontageSchritte.Add("ConnectionHolder", false);
        demontageSchritte.Add("CameraConnector", false);
        demontageSchritte.Add("LoudspeakerCable", false);
        demontageSchritte.Add("VibratingModule", false);
        backcover = GameObject.Find("Backcover");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && smartphoneCollider.controlSmartPhonePosition() && !player.getInHandItem())
        {
            GameObject.Find("Smartphone").transform.rotation = Quaternion.Euler(90f, 0f, 180f);
        }

        if (Input.GetKeyDown(KeyCode.Z) && smartphoneCollider.controlSmartPhonePosition())
        {
            Debug.Log("Test z");
            backcover.transform.parent = null;
            backcover.transform.position = new Vector3(transform.position.x, 1f, 0f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        playerIsHere = true;
        if (demontageSchritte["Backcover"] == false && !player.getInHandItem())
        {
            step1UI.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        playerIsHere = false;
        step1UI.SetActive(false);
    }

}
