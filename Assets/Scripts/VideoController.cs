using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using StarterAssets;
using System;
using TMPro;

public class VideoController : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public GameObject infoUI;
    public GameObject question1;
    public GameObject question2;
    public GameObject question3;
    public GameObject trueAnswer;
    public GameObject wrongAnswer;
    private FirstPersonController player;
    private bool userInputReceived = false;
    bool playerIsHere;
    private StoryModus story;
    private int currentQuestion = 0;
    public TextMeshProUGUI wrongAnswerText;
    private bool answerResult = false;
    private bool questioning = false;
    // Update is called once per frame
    void Update()
    {
        if (playerIsHere)
        {
            if (Input.GetKeyDown(KeyCode.X) && !videoPlayer.isPlaying && questioning != true)
            {
                StartVideo();
                player.setMoveSpeed(0);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartVideo();
                player.setMoveSpeed(0);
            }

            if (videoPlayer.isPaused)
            {
                if (currentQuestion == 4)
                {
                    player.setMoveSpeed(4);
                }
                else if (currentQuestion == 0)
                {
                    currentQuestion++;
                    questioning = true;
                }
                // Do something else after the video ends
                story.addPoints(100, this.gameObject.name);
                StartCoroutine(HandleQuestions());
            }

            if (videoPlayer.isPlaying)
            {
                infoUI.SetActive(false);
            }
        }
    }

    private IEnumerator HandleQuestions()
    {
        Dictionary<string, bool> objectives = story.getObjectives();
        if (currentQuestion == 1 && !question1.activeSelf)
        {
            Debug.Log("currentQuestion" + currentQuestion);
            question1.SetActive(true);
            yield return StartCoroutine(WaitForUserInput());
            objectives[question1.gameObject.name] = true;
            userInputReceived = false;
            yield return StartCoroutine(ActivateUIForSeconds(3f));
            userInputReceived = false;
            question1.SetActive(false);
            currentQuestion++;
        }
        else if (currentQuestion == 2 && !question2.activeSelf)
        {
            Debug.Log("currentQuestion" + currentQuestion);
            question2.SetActive(true);
            yield return StartCoroutine(WaitForUserInput());
            objectives[question2.gameObject.name] = true;
            userInputReceived = false;
            yield return StartCoroutine(ActivateUIForSeconds(3f));
            userInputReceived = false;
            question2.SetActive(false);
            currentQuestion++;
        }
        else if (currentQuestion == 3 && !question3.activeSelf)
        {
            Debug.Log("currentQuestion" + currentQuestion);
            question3.SetActive(true);
            yield return StartCoroutine(WaitForUserInput());
            objectives[question3.gameObject.name] = true;
            userInputReceived = false;
            yield return StartCoroutine(ActivateUIForSeconds(3f));
            userInputReceived = false;
            question3.SetActive(false);
            currentQuestion++;
            questioning = false;
        }
    }

    private void Start()
    {
        // Start playing the video on start
        //StartVideo();
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<FirstPersonController>();
        if (playerObject != null)
        {
            story = playerObject.GetComponent<StoryModus>();
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

    private IEnumerator ActivateUIForSeconds(float duration)
    {
        if (answerResult == true)
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
        yield return null;
    }

    private void HandleAnswer(string answer)
    {
        Dictionary<string, string> questions = story.getQuestions();
        if (currentQuestion == 1)
        {

            if (questions[question1.gameObject.name] == answer)
            {
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

    public void StartVideo()
    {
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
    }

    public void RestartVideo()
    {
        videoPlayer.Stop();
        videoPlayer.Play();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerIsHere = true;
            infoUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerIsHere = false;
            infoUI.SetActive(false);
        }
    }
}
