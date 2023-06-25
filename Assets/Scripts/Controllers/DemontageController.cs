using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemontageController : MonoBehaviour
{
    private Dictionary<string, bool> demontageSchritte = new Dictionary<string, bool>();
    private Player player;
    private SmartphoneCollider smartphoneCollider;
    private GameObject screwdriver;
    private GameObject hebelwerkzeug;
    private Animator screwdriverAnimator;
    private Animator backcoverAnimator;
    private Animator backcover2Animator;
    private Animator batteryAnimator;
    private Animator simboardAnimator;
    private Animator hebelwerkzeugAnimator;
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
    public GameObject simboard;
    public GameObject cameraConnector;
    public GameObject soundCable;
    public GameObject hauptplatine;
    private bool demontageIsActive = false;
    private int stepCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        GameObject placeTrigger = GameObject.Find("SmartphonePlaceTrigger");
        screwdriver = GameObject.Find("Screwdriver");
        backcover = GameObject.Find("Backcover");
        battery = GameObject.Find("Battery");
        microSDcard = GameObject.Find("SimBoardInvisible");
        simboard = GameObject.Find("SimBoard");
        schrauben = GameObject.Find("Schrauben");
        backcover2 = GameObject.Find("Backcover2");
        cameraConnector = GameObject.Find("CameraConnector");
        soundCable = GameObject.Find("SoundCable");
        hauptplatine = GameObject.Find("Hauptplatine");
        hebelwerkzeug = GameObject.Find("Hebelwerkzeug");
        player = playerObject.GetComponent<Player>();
        smartphoneCollider = placeTrigger.GetComponent<SmartphoneCollider>();
        demontageSchritte.Add("TurnSmartphone", false); //Step0
        demontageSchritte.Add("Backcover", false); //Step1
        demontageSchritte.Add("Battery", false); //Step2
        demontageSchritte.Add("MicroSDcard", false); //Step3
        demontageSchritte.Add("10screws", false); //Step4
        demontageSchritte.Add("Backcover2", false); //Step5  
        demontageSchritte.Add("SimBoard", false); //Step6  
        demontageSchritte.Add("CameraConnector", false); //Step7
        demontageSchritte.Add("SoundCable", false); //Step8
        demontageSchritte.Add("VibratingModule", false); //Step9
        schrauben.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        //Demontage 
        if (Input.GetKeyDown(KeyCode.X) && !player.getInHandItem() && playerIsHere)
        {
            demontageIsActive = true;
            startStepUI.SetActive(false);
        }
        //Umdrehen
        if (Input.GetKeyDown(KeyCode.E) && stepCounter == 0 && demontageIsActive && playerIsHere)
        {
            stepCounter++;
            GameObject.Find("Smartphone").transform.rotation = Quaternion.Euler(-270f, 0f, 90f);
            turnSmartphoneUI.SetActive(false);
        }
        //Backcover
        if (Input.GetKeyDown(KeyCode.F) && stepCounter == 1 && playerIsHere)
        {
            backcover.transform.parent = null;
            backcoverAnimator = backcover.GetComponent<Animator>();
            backcoverAnimator.enabled = true;
            backcoverAnimator.Play("Backcover");
            stepCounter++;
            step1UI.SetActive(false);
        }
        //Battery
        if (Input.GetKeyDown(KeyCode.E) && stepCounter == 2 && playerIsHere)
        {

            battery.transform.parent = null;
            batteryAnimator = battery.GetComponent<Animator>();
            batteryAnimator.enabled = true;
            batteryAnimator.Play("Battery");
            stepCounter++;
            step2UI.SetActive(false);
        }
        //Simkarten
        if (Input.GetKeyDown(KeyCode.F) && stepCounter == 3 && playerIsHere)
        {
            Destroy(GameObject.Find("SdCardBoard"));
            Destroy(GameObject.Find("SimCardBoard"));
            microSDcard.transform.parent = null;
            simboardAnimator = microSDcard.GetComponent<Animator>();
            simboardAnimator.enabled = true;
            simboardAnimator.Play("Simboard");
            stepCounter++;
            step3UI.SetActive(false);

        }
        //Schrauben
        if (Input.GetKeyDown(KeyCode.E) && stepCounter == 4 && playerIsHere && "Screwdriver".Equals(player.getInHandItem().name))
        {
            player.setInHandItem();
            screwdriver.layer = 0;
            GameObject schraubenImBackcover = GameObject.Find("SchraubenImBackcover");
            screwdriverAnimator = screwdriver.GetComponent<Animator>();
            screwdriverAnimator.enabled = true;
            screwdriverAnimator.Play("Screwdriver");
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
        if (Input.GetKeyDown(KeyCode.F) && stepCounter == 5 && playerIsHere)
        {
            simboard.transform.parent = null;
            backcover2.transform.parent = null;
            backcover2Animator = backcover2.GetComponent<Animator>();
            backcover2Animator.enabled = true;
            backcover2Animator.Play("Backcover2");
            stepCounter++;
            step5UI.SetActive(false);
        }
        //Anschluss des Bildschirms
        if (Input.GetKeyDown(KeyCode.E) && stepCounter == 6 && playerIsHere && "Hebelwerkzeug".Equals(player.getInHandItem().name))
        {
            player.setInHandItem();
            cameraConnector.transform.parent = null;
            hebelwerkzeug.layer = 0;
            hebelwerkzeugAnimator = hebelwerkzeug.GetComponent<Animator>();
            hebelwerkzeugAnimator.enabled = true;
            Destroy(hebelwerkzeug.GetComponent<Rigidbody>());
            Destroy(hebelwerkzeug.GetComponent<MeshCollider>());
            Destroy(hebelwerkzeug.GetComponent<BoxCollider>());
            hebelwerkzeugAnimator.Play("Hebelwerkzeug1");
            stepCounter++;
            step6UI.SetActive(false);
        }
        //Anschluss des Simboards
        if (Input.GetKeyDown(KeyCode.F) && stepCounter == 7 && playerIsHere && "Hebelwerkzeug".Equals(player.getInHandItem().name))
        {
            player.setInHandItem();
            hebelwerkzeug.layer = 0;
            hebelwerkzeugAnimator = hebelwerkzeug.GetComponent<Animator>();
            hebelwerkzeugAnimator.enabled = true;
            Destroy(hebelwerkzeug.GetComponent<Rigidbody>());
            Destroy(hebelwerkzeug.GetComponent<MeshCollider>());
            Destroy(hebelwerkzeug.GetComponent<BoxCollider>());
            hebelwerkzeugAnimator.Play("Hebelwerkzeug2");
            stepCounter++;
            step7UI.SetActive(false);
        }
        //Camera connector
        if (Input.GetKeyDown(KeyCode.E) && stepCounter == 8 && playerIsHere && "Hebelwerkzeug".Equals(player.getInHandItem().name))
        {
            soundCable.transform.parent = null;
            player.setInHandItem();
            hebelwerkzeug.layer = 0;
            hebelwerkzeugAnimator = hebelwerkzeug.GetComponent<Animator>();
            hebelwerkzeugAnimator.enabled = true;
            Destroy(hebelwerkzeug.GetComponent<Rigidbody>());
            Destroy(hebelwerkzeug.GetComponent<MeshCollider>());
            Destroy(hebelwerkzeug.GetComponent<BoxCollider>());
            hebelwerkzeugAnimator.Play("Hebelwerkzeug3");
            stepCounter++;
            step8UI.SetActive(false);
        }
        //SoundCable connector
        if (Input.GetKeyDown(KeyCode.F) && stepCounter == 9 && playerIsHere && "Hebelwerkzeug".Equals(player.getInHandItem().name))
        {
            hauptplatine.transform.parent = null;
            player.setInHandItem();
            hebelwerkzeug.layer = 0;
            hebelwerkzeugAnimator = hebelwerkzeug.GetComponent<Animator>();
            hebelwerkzeugAnimator.enabled = true;
            Destroy(hebelwerkzeug.GetComponent<Rigidbody>());
            Destroy(hebelwerkzeug.GetComponent<MeshCollider>());
            Destroy(hebelwerkzeug.GetComponent<BoxCollider>());
            hebelwerkzeugAnimator.Play("Hebelwerkzeug4");
            stepCounter++;
            step9UI.SetActive(false);
        }
        //Hauptplatine
        if (Input.GetKeyDown(KeyCode.E) && stepCounter == 10 && playerIsHere && "Hebelwerkzeug".Equals(player.getInHandItem().name))
        {
            player.setInHandItem();
            hebelwerkzeug.layer = 0;
            hebelwerkzeugAnimator = hebelwerkzeug.GetComponent<Animator>();
            hebelwerkzeugAnimator.enabled = true;
            Destroy(hebelwerkzeug.GetComponent<Rigidbody>());
            Destroy(hebelwerkzeug.GetComponent<MeshCollider>());
            Destroy(hebelwerkzeug.GetComponent<BoxCollider>());
            hebelwerkzeugAnimator.Play("Hebelwerkzeug5");
            stepCounter++;
            step10UI.SetActive(false);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        playerIsHere = true;
        if (!demontageIsActive)
        {
            startStepUI.SetActive(true);
        }
        if (stepCounter == 11 && !step10UI.activeSelf)
        {
            step11UI.SetActive(true);
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
        if (stepCounter == 5 && !step5UI.activeSelf)
        {
            step5UI.SetActive(true);
        }
        if (stepCounter == 6 && !step5UI.activeSelf)
        {
            step6UI.SetActive(true);
        }
        if (stepCounter == 7 && !step6UI.activeSelf)
        {
            step7UI.SetActive(true);
        }
        if (stepCounter == 8 && !step7UI.activeSelf)
        {
            step8UI.SetActive(true);
        }
        if (stepCounter == 9 && !step8UI.activeSelf)
        {
            step9UI.SetActive(true);
        }
        if (stepCounter == 10 && !step9UI.activeSelf)
        {
            step10UI.SetActive(true);
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
    }

}
