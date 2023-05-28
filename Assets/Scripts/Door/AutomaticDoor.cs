using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutomaticDoor : MonoBehaviour
{
    public GameObject movingDoor;

    public GameObject doorUI;
    public GameObject warningUI;
    public GameObject warningUI2;
    public GameObject warningUI3;

    public float maximumOpening = 10f;
    public float maximumClosing = 0f;

    public float movementSpeed = 3f;

    bool playerIsHere;
    bool doorIsOpening = false;
    private float oldYPosition;
    private StoryModus story;


    // Start is called before the first frame update
    void Start()
    {
        playerIsHere = false;
        oldYPosition = movingDoor.transform.position.y;
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            story = playerObject.GetComponent<StoryModus>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsHere)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                doorIsOpening = true;
            }
            if (movingDoor.transform.position.y < maximumOpening && doorIsOpening)
            {
                movingDoor.transform.Translate(0f, movementSpeed * Time.deltaTime, 0f);
            }
            else
            {
                doorIsOpening = false;
            }
        }
        else
        {
            if (movingDoor.transform.position.y > oldYPosition)
            {
                if (movingDoor.transform.position.y + (-movementSpeed * Time.deltaTime) < oldYPosition)
                {
                    movingDoor.transform.position = new Vector3(movingDoor.transform.position.x, oldYPosition, movingDoor.transform.position.z);
                }
                else
                {
                    movingDoor.transform.Translate(0f, -movementSpeed * Time.deltaTime, 0f);
                }
            }
        }


    }

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Collided object is: " + col.gameObject.name);
        if (col.gameObject.tag == "Player")
        {
            Dictionary<string, bool> objectives = story.getObjectives();
            if (this.name == "Sensor1")
            {
                playerIsHere = true;
                doorUI.SetActive(true);
            }
            else if (this.name == "Sensor2")
            {
                if (objectives["VideoTrigger1"] == true)
                {
                    playerIsHere = true;
                    doorUI.SetActive(true);
                }
                else
                {
                    warningUI.SetActive(true);
                }
            }else if(this.name == "Sensor3")
            {
                if (objectives["VideoTrigger1"] == true && objectives["VideoTrigger2"] == true)
                {
                    playerIsHere = true;
                    doorUI.SetActive(true);
                }
                else
                {
                    warningUI2.SetActive(true);
                }
            }else if(this.name == "Sensor4")
            {
                if (objectives["VideoTrigger1"] == true && objectives["VideoTrigger2"] == true && objectives["VideoTrigger3"] == true)
                {
                    playerIsHere = true;
                    doorUI.SetActive(true);
                }
                else
                {
                    warningUI3.SetActive(true);
                }
            }
            Debug.Log("Collided object is test: " + this.name);
         
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerIsHere = false;
            doorIsOpening = false;
            doorUI.SetActive(false);
            warningUI.SetActive(false);
            warningUI2.SetActive(false);
            warningUI3.SetActive(false);
        }
    }
}
