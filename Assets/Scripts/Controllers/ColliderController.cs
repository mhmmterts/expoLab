using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ColliderController : MonoBehaviour
{
    public GameObject imageUI;
    public GameObject openImageUI;
    bool playerIsHere;
    private StoryModus story;
    private FirstPersonController player;
    public Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {
        playerIsHere = false;
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<FirstPersonController>();
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
                openImageUI.SetActive(false);
                imageUI.SetActive(true);
                player.setMoveSpeed(0);
                GetComponent<DialogueManager>().StartDialogue(dialogue);
                
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                GetComponent<DialogueManager>().DisplayNextSentence();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                imageUI.SetActive(false);
                openImageUI.SetActive(true);
                player.setMoveSpeed(4);
                story.addPoints(50, this.gameObject.name);
            }
        }

    }

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Collided object is: " + col.gameObject.name);
        if (col.gameObject.tag == "Player")
        {
            Dictionary<string, bool> objectives = story.getObjectives();
            if (this.name.Contains("Image") || this.name.Contains("Schild"))
            {
                playerIsHere = true;
                openImageUI.SetActive(true);
            }
            Debug.Log("Collided object is test: " + this.name);

        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerIsHere = false;
            openImageUI.SetActive(false);
        }
    }
}
