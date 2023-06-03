using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemontageController : MonoBehaviour
{
    private Dictionary<string, bool> demontageSchritte = new Dictionary<string, bool>();
    private Player player;
    private SmartphoneCollider smartphoneCollider;
    bool playerIsHere;
    public GameObject startStepUI;
    public GameObject turnSmartphoneUI;
    public GameObject step1UI;
    public GameObject step2UI;
    public GameObject step3UI;
    public GameObject step4UI;
    public GameObject step5UI;
    public GameObject step6UI;
    public GameObject step7UI;
    public GameObject step8UI;
    public GameObject step9UI;
    public GameObject step10UI;
    public GameObject step11UI;
    public GameObject step12UI;
    public GameObject step13UI;
    public GameObject step14UI;
    public GameObject backcover;
    public GameObject backcover2;
    public GameObject battery;
    public GameObject microSDcard;
    public GameObject schrauben;
    private bool demontageIsActive = false;
    private int stepCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        GameObject placeTrigger = GameObject.Find("SmartphonePlaceTrigger");
        player = playerObject.GetComponent<Player>();
        smartphoneCollider = placeTrigger.GetComponent<SmartphoneCollider>();
        demontageSchritte.Add("TurnSmartphone", false); //Step0
        demontageSchritte.Add("Backcover", false); //Step1
        demontageSchritte.Add("Battery", false); //Step2
        demontageSchritte.Add("MicroSDcard", false); //Step3
        demontageSchritte.Add("10screws", false); //Step4
        demontageSchritte.Add("Backcover2", false); //Step5  
        demontageSchritte.Add("ConnectionHolder", false); //Step6  buradan devam edecennn
        demontageSchritte.Add("CameraConnector", false); //Step7
        demontageSchritte.Add("LoudspeakerCable", false); //Step8
        demontageSchritte.Add("VibratingModule", false); //Step9 
        //Komponenten
        backcover = GameObject.Find("Backcover");
        battery = GameObject.Find("Battery");
        microSDcard = GameObject.Find("SimBoardInvisible");
        schrauben = GameObject.Find("Schrauben");
        schrauben.SetActive(false);
        backcover2 = GameObject.Find("Backcover2");

    }

    // Update is called once per frame
    void Update()
    {
        //Demontage 
        if (Input.GetKeyDown(KeyCode.X) && !player.getInHandItem())
        {
            demontageIsActive = true;
            startStepUI.SetActive(false);
        }
        //Umdrehen
        if (Input.GetKeyDown(KeyCode.E) && stepCounter == 0 && demontageIsActive)
        {
            stepCounter++;
            GameObject.Find("Smartphone").transform.rotation = Quaternion.Euler(-270f, 0f, 90f);
            turnSmartphoneUI.SetActive(false);
        }
        //Backcover
        if (Input.GetKeyDown(KeyCode.F) && stepCounter == 1)
        {
            stepCounter++;
            backcover.transform.parent = null;
            backcover.transform.position = new Vector3(-38.2779999f, 0.377999991f, 3.36500001f);
            backcover.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            step1UI.SetActive(false);
            backcover.layer = 6;
            backcover.AddComponent<Rigidbody>();
            backcover.GetComponent<Rigidbody>().isKinematic = true;
        }
        //Battery
        if (Input.GetKeyDown(KeyCode.E) && smartphoneCollider.controlSmartPhonePosition() && stepCounter == 2)
        {
            stepCounter++;
            battery.transform.parent = null;
            battery.transform.position = new Vector3(-37.9f, 0.4f, 3.2f);
            battery.transform.rotation = Quaternion.Euler(-90f, -90f, -180f);
            step2UI.SetActive(false);
            battery.layer = 6;
            battery.AddComponent<Rigidbody>();
            battery.GetComponent<Rigidbody>().isKinematic = true;
        }
        //Simkarten
        if (Input.GetKeyDown(KeyCode.F) && smartphoneCollider.controlSmartPhonePosition() && stepCounter == 3)
        {
            GameObject simboard = GameObject.Find("SimBoard");
            foreach (Transform child in simboard.transform)
            {
                // Destroy the child object
                Destroy(child.gameObject);
                // Or if you want to destroy the child object immediately, use DestroyImmediate(child.gameObject);
            }
            stepCounter++;
            microSDcard.transform.parent = null;
            microSDcard.transform.position = new Vector3(-37.6f, 0.4f, 3.2f);
            microSDcard.transform.rotation = Quaternion.Euler(-90f, -90f, -180f);
            step3UI.SetActive(false);
            microSDcard.layer = 6;
            microSDcard.AddComponent<Rigidbody>();
            microSDcard.GetComponent<Rigidbody>().isKinematic = true;
        }
        //Schrauben
        if (Input.GetKeyDown(KeyCode.E) && smartphoneCollider.controlSmartPhonePosition() && stepCounter == 4)
        {
            GameObject schraubenImBackcover = GameObject.Find("SchraubenImBackcover");
            foreach (Transform child in schraubenImBackcover.transform)
            {
                // Destroy the child object
                Destroy(child.gameObject);
                // Or if you want to destroy the child object immediately, use DestroyImmediate(child.gameObject);
            }

            stepCounter++;
            step4UI.SetActive(false);
            schrauben.layer = 6;
            schrauben.SetActive(true);
        }
        //Backcover2
        if (Input.GetKeyDown(KeyCode.F) && smartphoneCollider.controlSmartPhonePosition() && stepCounter == 5)
        {
            stepCounter++;
            backcover2.transform.parent = null;
            backcover2.transform.position = new Vector3(-37.05f, 0.377999991f, 3.36500001f);
            backcover2.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            step5UI.SetActive(false);
            backcover2.layer = 6;
            backcover2.AddComponent<Rigidbody>();
            backcover2.GetComponent<Rigidbody>().isKinematic = true;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        playerIsHere = true;
        if (!demontageIsActive)
        {
            startStepUI.SetActive(true);
        }

    }

    public void OnTriggerStay(Collider other)
    {
        if (demontageIsActive && stepCounter == 0 && !turnSmartphoneUI.activeSelf)
        {
            turnSmartphoneUI.SetActive(true);
        }
        if (stepCounter == 1 && !step1UI.activeSelf)
        {
            step1UI.SetActive(true);
        }
        if (stepCounter == 2 && !step2UI.activeSelf)
        {
            step2UI.SetActive(true);
        }
        if (stepCounter == 3 && !step3UI.activeSelf)
        {
            step3UI.SetActive(true);
        }
        if (stepCounter == 4 && !step4UI.activeSelf)
        {
            step4UI.SetActive(true);
        }
        if(stepCounter == 5 && !step5UI.activeSelf)
        {
            step5UI.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        playerIsHere = false;
        turnSmartphoneUI.SetActive(false);
        startStepUI.SetActive(false);
        step1UI.SetActive(false);
        step2UI.SetActive(false);
        step3UI.SetActive(false);
        step4UI.SetActive(false);
        step5UI.SetActive(false);
        step6UI.SetActive(false);
        step7UI.SetActive(false);
        step8UI.SetActive(false);
        step9UI.SetActive(false);
        step10UI.SetActive(false);
        step11UI.SetActive(false);
        step11UI.SetActive(false);
    }

}
