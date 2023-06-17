using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;

public class ColliderController : MonoBehaviour
{
    public GameObject imageUI;
    public GameObject openImageUI;
    bool playerIsHere;
    private StoryModus story;
    private FirstPersonController player;
    public Dialogue dialogue;
    private DialogueManager dialogueManager;
    private bool isOpen, isFinished = false;
    public GameObject question1;
    public GameObject question1AnswerUI;
    public GameObject question2;
    public GameObject question2AnswerUI;
    public GameObject question3;
    public GameObject question3AnswerUI;
    public GameObject trueAnswer;
    public GameObject wrongAnswer;
    public TextMeshProUGUI wrongAnswerText;
    private int currentQuestion = 0;
    private bool questioning = false, questionAnswered = false, userInputReceived = false, answerResult = false;
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
        dialogueManager = GetComponent<DialogueManager>();
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
                dialogueManager.StartDialogue(dialogue);
                isOpen = true;
                currentQuestion = 0;
                isFinished = false;
            }
            if (Input.GetKeyDown(KeyCode.C) && dialogueManager.controlDialogue())
            {
                isOpen = GetComponent<DialogueManager>().DisplayNextSentence(dialogue);
                if (!isOpen)
                {
                    isFinished = true;
                }
            }
            if (isFinished)
            {
                if (currentQuestion == 0)
                {
                    currentQuestion++;
                    questioning = true;
                }
                // Do something else after the infograph ends
                story.addPoints(50, this.gameObject.name);
                StartCoroutine(HandleQuestions());
            }
        }
    }

    private IEnumerator HandleQuestions()
    {
        if (questioning)
        {
            Dictionary<string, bool> objectives = story.getObjectives();
            if (currentQuestion == 2 && null == question2 || currentQuestion == 3 && null == question3 || currentQuestion == 4)
            {
                player.setMoveSpeed(4);
                questioning = false;
                foreach (GameObject image in dialogue.objects)
                {
                    image.SetActive(false);
                }
                imageUI.SetActive(false);
                openImageUI.SetActive(true);
            }
            else if (currentQuestion == 1 && !question1.activeSelf && !questionAnswered && question1AnswerUI != null)
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
            }
            else if (currentQuestion == 3 && !question3.activeSelf && !question2AnswerUI.activeSelf && !questionAnswered && question3 != null)
            {
                questionAnswered = true;
                Debug.Log("currentQuestion" + currentQuestion);
                question3.SetActive(true);
                yield return StartCoroutine(WaitForUserInput());
                objectives[question3.gameObject.name] = true;
                userInputReceived = false;
                yield return StartCoroutine(ActivateUIForSeconds(3f, question3AnswerUI));
                userInputReceived = false;
                question3.SetActive(false);
                yield return StartCoroutine(WaitForInput(question3AnswerUI));
                currentQuestion++;
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
        else if (currentQuestion == 3)
        {
            if (questions[question3.gameObject.name] == answer)
            {
                story.addPoints(50, question3.gameObject.name);
                answerResult = true;
            }
            else
            {
                answerResult = false;
                wrongAnswerText.SetText("Leider war die richtige Antwort: " + questions[question3.gameObject.name] + " :(");
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
