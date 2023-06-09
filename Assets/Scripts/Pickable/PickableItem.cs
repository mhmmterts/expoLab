using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable
{
    [field: SerializeField]
    public bool KeepWorldPosition { get; private set; }

    private Rigidbody rb;
    private GameObject screwdriver;
    private Animator screwdriverAnimator;
    private GameObject hebelwerkzeug;
    private Animator hebelwerkzeugAnimator;
    private GameObject backcover;
    private Animator backcoverAnimator;
    private GameObject backcover2;
    private Animator backcover2Animator;
    private GameObject battery;
    private Animator batteryAnimator;
    private GameObject simboard;
    private Animator simboardAnimator;
    private GameObject simboard2;
    private Animator simboardAnimator2;
    private GameObject simboardConnector;
    private Animator simboardConnectorAnimator;
    private GameObject motherboardConnection;
    private Animator motherboardConnectionAnimator;
    private GameObject cameraConnector;
    private Animator cameraConnectorAnimator;
    private GameObject cableConnector;
    private Animator cableConnectorAnimator;
    private GameObject hauptplatine;
    private Animator hauptplatineAnimator;
    public GameObject step11UI;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }
    public GameObject PickUp()
    {
        rb = GetComponent<Rigidbody>();
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
        }
        else if (rb.name == "Backcover")
        {
            transform.position = new Vector3(0.07f, 0.17f, 0.13f);
            transform.rotation = Quaternion.Euler(-108f, -178.6f, transform.rotation.eulerAngles.z);
        }
        else if (rb.name == "Battery")
        {
            transform.position = new Vector3(0.0599999987f, 0.180000007f, 0f);
            transform.rotation = Quaternion.Euler(0, 180, 270);
        }
        else if (rb.name == "SimBoardInvisible")
        {
            transform.position = new Vector3(0.0500000007f, 0.209999993f, 0f);
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (rb.name == "Schrauben")
        {
            transform.position = new Vector3(2.38000011f, 0.25f, 2.75000006f);
        }
        else if (rb.name == "Backcover2")
        {
            transform.position = new Vector3(0.07f, 0.17f, 0.13f);
            transform.rotation = Quaternion.Euler(-108f, -178.6f, transform.rotation.eulerAngles.z);
        }
        else if (rb.name == "Screwdriver")
        {
            transform.position = new Vector3(0.4f, 0f, 0f);
            transform.rotation = Quaternion.Euler(-75f, -5f, transform.rotation.eulerAngles.z);
        }
        else if (rb.name == "Hebelwerkzeug")
        {
            transform.position = new Vector3(0.4f, 0.2f, 0.24f);
            transform.rotation = Quaternion.Euler(352.4f, 270f, 48.21f);
        }
        else if(rb.name == "SoundCable")
        {
            transform.position = new Vector3(0, -0.26f, 0);
        }else if(rb.name == "CameraConnector")
        {
            transform.position = new Vector3(0.06f, 0.23f, 0);
        }
        else if (rb.name == "SimBoard")
        {
            transform.position = new Vector3(0.06f, 0.23f, 0);
        }
        else if (rb.name == "Hauptplatine")
        {
            transform.position = new Vector3(0.06f, 0.2f, 0);
            transform.rotation = Quaternion.Euler(-93.42f, 180f, 0);
        }
        return this.gameObject;
    }

    public void AnimationFinished(string name)
    {
        // Code to be executed after the animation is complete
        screwdriver = GameObject.Find("Screwdriver");
        screwdriverAnimator = screwdriver.GetComponent<Animator>();
        hebelwerkzeug = GameObject.Find("Hebelwerkzeug");
        hebelwerkzeugAnimator = hebelwerkzeug.GetComponent<Animator>();
        backcover = GameObject.Find("Backcover");
        backcoverAnimator = backcover.GetComponent<Animator>();
        backcover2 = GameObject.Find("Backcover2");
        backcover2Animator = backcover2.GetComponent<Animator>();
        battery = GameObject.Find("Battery");
        batteryAnimator = battery.GetComponent<Animator>();
        simboard = GameObject.Find("SimBoardInvisible");
        simboardAnimator = simboard.GetComponent<Animator>();
        simboard2 = GameObject.Find("SimBoard");
        simboardAnimator2 = simboard2.GetComponent<Animator>();
        simboardConnector = GameObject.Find("SimboardConnector");
        simboardConnectorAnimator = simboardConnector.GetComponent<Animator>();
        motherboardConnection = GameObject.Find("MotherBoardConnection");
        motherboardConnectionAnimator = motherboardConnection.GetComponent<Animator>();
        cameraConnector = GameObject.Find("CameraConnector");
        cameraConnectorAnimator = cameraConnector.GetComponent<Animator>();
        cableConnector = GameObject.Find("SoundCable");
        cableConnectorAnimator = cableConnector.GetComponent<Animator>();
        hauptplatine = GameObject.Find("Hauptplatine");
        hauptplatineAnimator = hauptplatine.GetComponent<Animator>();

        if ("Backcover".Equals(name))
        {
            Debug.Log("BackcoverAnimation finished!");
            backcoverAnimator.enabled = false;
            backcover.AddComponent<Rigidbody>();
            backcover.GetComponent<Rigidbody>().isKinematic = true;
            backcover.layer = 6;
        }
        else if ("Screwdriver".Equals(name))
        {
            Debug.Log("ScrewdriverAnimation finished!");
            screwdriverAnimator.enabled = false;
            screwdriver.layer = 6;
        }
        else if ("Battery".Equals(name))
        {
            Debug.Log("BatteryAnimation finished!");
            batteryAnimator.enabled = false;
            battery.AddComponent<Rigidbody>();
            battery.GetComponent<Rigidbody>().isKinematic = true;
            battery.layer = 6;
        }
        else if ("Simboard".Equals(name))
        {
            Debug.Log("SimBoardAnimation finished!");
            simboardAnimator.enabled = false;
            simboard.AddComponent<Rigidbody>();
            simboard.GetComponent<Rigidbody>().isKinematic = true;
            simboard.layer = 6;
        }
        else if ("Backcover2".Equals(name))
        {
            Debug.Log("Backcover2Animation finished!");
            backcover2Animator.enabled = false;
            backcover2.AddComponent<Rigidbody>();
            backcover2.GetComponent<Rigidbody>().isKinematic = true;
            backcover2.layer = 6;
        }
        else if ("Hebelwerkzeug1".Equals(name))
        {
            Debug.Log("Hebelwerkzeug1Animation finished!");
            hebelwerkzeug.AddComponent<Rigidbody>();
            hebelwerkzeug.AddComponent<MeshCollider>();
            hebelwerkzeug.AddComponent<BoxCollider>();
            hebelwerkzeugAnimator.enabled = false;
            hebelwerkzeug.layer = 6;
            motherboardConnectionAnimator.Play("MotherboardConnection");
        }
        else if ("Hebelwerkzeug2".Equals(name))
        {
            Debug.Log("Hebelwerkzeug2Animation finished!");
            hebelwerkzeug.AddComponent<Rigidbody>();
            hebelwerkzeug.AddComponent<MeshCollider>();
            hebelwerkzeug.AddComponent<BoxCollider>();
            hebelwerkzeugAnimator.enabled = false;
            hebelwerkzeug.layer = 6;
            simboardConnectorAnimator.enabled = true;
            simboardConnectorAnimator.Play("SimboardConnector");
        }
        else if ("SimBoardMovement".Equals(name))
        {
            simboardConnectorAnimator.enabled = false;
            Debug.Log("SimBoardMovement is active.");
            simboardAnimator2.enabled = true;
            simboardAnimator2.Play("SimBoard2");
        }
        else if ("SimBoard2".Equals(name))
        {
            Debug.Log("SimBoard2Animation finished!");
            simboardAnimator2.enabled = false;
            simboard2.AddComponent<Rigidbody>();
            simboard2.AddComponent<MeshCollider>();
            simboard2.AddComponent<BoxCollider>();
            simboard2.GetComponent<Rigidbody>().isKinematic = true;
            simboard2.layer = 6;
        }
        else if ("Hebelwerkzeug3".Equals(name))
        {
            Debug.Log("Hebelwerkzeug3Animation finished!");
            hebelwerkzeug.AddComponent<Rigidbody>();
            hebelwerkzeug.AddComponent<MeshCollider>();
            hebelwerkzeug.AddComponent<BoxCollider>();
            hebelwerkzeugAnimator.enabled = false;
            hebelwerkzeug.layer = 6;
            cameraConnectorAnimator.enabled = true;
            cameraConnectorAnimator.Play("CamConnector");
        }
        else if ("CamConnector".Equals(name))
        {
            Debug.Log("CamConnectorAnimation finished!");
            cameraConnectorAnimator.enabled = false;
            cameraConnector.AddComponent<Rigidbody>();
            cameraConnector.AddComponent<MeshCollider>();
            cameraConnector.AddComponent<BoxCollider>();
            cameraConnector.GetComponent<Rigidbody>().isKinematic = true;
            cameraConnector.layer = 6;
        }
        else if ("Hebelwerkzeug4".Equals(name))
        {
            Debug.Log("Hebelwerkzeug4Animation finished!");
            hebelwerkzeug.AddComponent<Rigidbody>();
            hebelwerkzeug.AddComponent<MeshCollider>();
            hebelwerkzeug.AddComponent<BoxCollider>();
            hebelwerkzeugAnimator.enabled = false;
            hebelwerkzeug.layer = 6;
            cableConnectorAnimator.enabled = true;
            cableConnectorAnimator.Play("SoundCable");
        }
        else if ("SoundCable".Equals(name))
        {
            Debug.Log("CableConnectorAnimation finished!");
            cableConnectorAnimator.enabled = false;
            cableConnector.AddComponent<Rigidbody>();
            cableConnector.GetComponent<BoxCollider>().enabled = true;
            cableConnector.GetComponent<Rigidbody>().isKinematic = true;
            cableConnector.layer = 6;
        }
        else if ("Hebelwerkzeug5".Equals(name))
        {
            Debug.Log("Hebelwerkzeug5Animation finished!");
            hebelwerkzeug.AddComponent<Rigidbody>();
            hebelwerkzeug.AddComponent<MeshCollider>();
            hebelwerkzeug.AddComponent<BoxCollider>();
            hebelwerkzeugAnimator.enabled = false;
            hebelwerkzeug.layer = 6;
            hauptplatineAnimator.enabled = true;
            hauptplatineAnimator.Play("Hauptplatine");
        }else if ("Hauptplatine".Equals(name))
        {
            Debug.Log("HauptplatineAnimation finished!");
            hauptplatineAnimator.enabled = false;
            hauptplatine.AddComponent<Rigidbody>();
            hauptplatine.AddComponent<MeshCollider>();
            hauptplatine.AddComponent<BoxCollider>();
            hauptplatine.GetComponent<Rigidbody>().isKinematic = true;
            hauptplatine.layer = 6;
            step11UI.SetActive(true);
           
        }
    }

    public void IsPickable(string pickable)
    {
        if ("True".Equals(pickable))
        {
            Debug.Log("layer 6 yap�ld�");
            screwdriver.layer = 6;
        }
        else
        {
            Debug.Log("layer 0 yap�ld�");
            screwdriver.layer = 0;
        }

    }

}
