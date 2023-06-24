using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;

public class AutomaticDoor : MonoBehaviour
{
    public GameObject movingDoor;

    public GameObject imageUI;
    public GameObject doorUI;
    public GameObject warningUI;
    public GameObject warningUI2;
    public GameObject warningUI3;
    private int currentQuestion = 0;
    private bool isOpen, isFinished = false;
    private bool questioning = false, questionAnswered = false, userInputReceived = false, answerResult = false, roomEntered = false;
    public Dialogue dialogue;
    private DialogueManager dialogueManager;
    public GameObject question1;
    public GameObject question1AnswerUI;
    public GameObject question2;
    public GameObject question2AnswerUI;
    public GameObject trueAnswer;
    public GameObject wrongAnswer;
    public TextMeshProUGUI wrongAnswerText;
    private FirstPersonController player;
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
        dialogueManager = GetComponent<DialogueManager>();
        playerIsHere = false;
        oldYPosition = movingDoor.transform.position.y;
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
        if (Input.GetKeyDown(KeyCode.C) && dialogueManager.controlDialogue())
        {
            isOpen = GetComponent<DialogueManager>().DisplayNextSentence(dialogue);
            if (!isOpen)
            {
                isFinished = true;
            }
            if (isFinished)
            {
                StartCoroutine(HandleQuestions());
            }
        }
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

    private IEnumerator HandleQuestions()
    {
        if (questioning)
        {
            Dictionary<string, bool> objectives = story.getObjectives();
            if (currentQuestion == 1 && !question1.activeSelf && !questionAnswered && question1AnswerUI != null)
            {
                questionAnswered = true;
                Debug.Log("currentQuestion" + currentQuestion);
                question1.SetActive(true);
                yield return StartCoroutine(WaitForUserInput());
                objectives[question1.gameObject.name] = true;
                userInputReceived = false;
                yield return StartCoroutine(ActivateUIForSeconds(3f, question1AnswerUI));
                userInputReceived = false;
                question1.SetActive(false);
                yield return StartCoroutine(WaitForInput(question1AnswerUI));
                currentQuestion++;
                startInfograph();
                
            }
            else if (currentQuestion == 2 && !question2.activeSelf && !question1AnswerUI.activeSelf && !questionAnswered && question2 != null)
            {
                questionAnswered = true;
                Debug.Log("currentQuestion" + currentQuestion);
                question2.SetActive(true);
                yield return StartCoroutine(WaitForUserInput());
                objectives[question2.gameObject.name] = true;
                userInputReceived = false;
                yield return StartCoroutine(ActivateUIForSeconds(3f, question2AnswerUI));
                userInputReceived = false;
                question2.SetActive(false);
                yield return StartCoroutine(WaitForInput(question2AnswerUI));
                currentQuestion++;
                foreach (GameObject image in dialogue.objects)
                {
                    image.SetActive(false);
                }
                imageUI.SetActive(false);
                player.setMoveSpeed(4);
            }
        }
    }

    private IEnumerator WaitForInput(GameObject answerUI)
    {
        while (!userInputReceived)
        {
            yield return null; // Yield control back to the Unity engine
            if (Input.anyKeyDown)
            {
                answerUI.SetActive(false);
                questionAnswered = false;
                break;
            }
        }
    }

    private IEnumerator WaitForUserInput()
    {

        // Wait until the user provides input
        while (!userInputReceived)
        {
            yield return null; // Yield control back to the Unity engine
            if (Input.GetKeyDown(KeyCode.A))
            {
                HandleAnswer("A");
                break;
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                HandleAnswer("B");
                break;
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                HandleAnswer("C");
                break;
            }
            else if (Input.anyKeyDown)
            {
                HandleOtherKey();
            }
        }
    }

    private IEnumerator ActivateUIForSeconds(float duration, GameObject answerUI)
    {
        if (answerResult)
        {
            trueAnswer.SetActive(true);
            yield return new WaitForSeconds(duration);
            trueAnswer.SetActive(false);
        }
        else
        {
            wrongAnswer.SetActive(true);
            yield return new WaitForSeconds(duration);
            wrongAnswer.SetActive(false);
        }
        answerUI.SetActive(true);
        yield return null;
    }

    private void HandleAnswer(string answer)
    {
        Dictionary<string, string> questions = story.getQuestions();
        if (currentQuestion == 1)
        {

            if (questions[question1.gameObject.name] == answer)
            {
                Debug.Log(question1.gameObject.name + " and its answer is " + questions[question1.gameObject.name]);
                story.addPoints(50, question1.gameObject.name);
                answerResult = true;
            }
            else
            {
                answerResult = false;
                wrongAnswerText.SetText("Leider war die richtige Antwort: " + questions[question1.gameObject.name] + " :(");
            }
        }
        else if (currentQuestion == 2)
        {
            if (questions[question2.gameObject.name] == answer)
            {
                story.addPoints(50, question2.gameObject.name);
                answerResult = true;
            }
            else
            {
                answerResult = false;
                wrongAnswerText.SetText("Leider war die richtige Antwort: " + questions[question2.gameObject.name] + " :(");
            }
        }

        userInputReceived = true;

        // Handle the answer
        Debug.Log("User selected answer: " + answer);
        // Perform actions based on the answer (e.g., check correctness, update score, etc.)
    }
    private void HandleOtherKey()
    {
        answerResult = false;
        userInputReceived = false;
        // Handle other key press
        Debug.Log("User pressed a key other than A, B, or C");
        // Perform actions for other key press
    }

    private void startInfograph()
    {
        imageUI.SetActive(true);
        dialogueManager.StartDialogue(dialogue);
        isOpen = true;
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
            }
            else if (this.name == "Sensor3")
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
            }
            else if (this.name == "Sensor4")
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
            else if (this.name == "QuestionSensor" && !roomEntered)
            {
                Debug.Log("HandleQuestions");
                questioning = true;
                currentQuestion++;
                player.setMoveSpeed(0);
                roomEntered = true;
                StartCoroutine(HandleQuestions());
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
