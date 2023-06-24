using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryModus : MonoBehaviour
{
    public const int maxPoints = 1000;

    public int collectedPoints = 0;

    public GameObject collider1;
    public GameObject collider2;
    public GameObject collider3;
    public AudioSource startAudioSource;
    public TextMeshProUGUI points;

    private Dictionary<string, bool> objectives = new Dictionary<string, bool>();
    private Dictionary<string, string> questions = new Dictionary<string, string>();
    // Start is called before the first frame update
    void Start()
    {
        objectives.Add("VideoTrigger1", false);
        objectives.Add("Room1Question1", false);
        objectives.Add("Room1Question2", false);
        objectives.Add("Room1Question3", false);
        objectives.Add("Room1Question4", false);
        objectives.Add("Room1Question5", false);
        objectives.Add("Room1Question6", false);
        objectives.Add("Room1Question7", false);
        objectives.Add("Image1", false);
        objectives.Add("Image2", false);
        objectives.Add("VideoTrigger2", false);
        objectives.Add("Room2Question1", false);
        objectives.Add("Room2Question2", false);
        objectives.Add("Room2Question3", false);
        objectives.Add("VideoTrigger3", false);
        objectives.Add("Room3Question1", false);
        objectives.Add("Room3Question2", false);
        objectives.Add("Room3Question3", false);
        objectives.Add("Room3Question4", false);
        objectives.Add("Image3", false);
        objectives.Add("Image4", false);
        objectives.Add("Image5", false);
        objectives.Add("Image6", false);
        objectives.Add("Image7", false);
        objectives.Add("Image8", false);
        objectives.Add("Image9", false);
        objectives.Add("Image10", false);
        objectives.Add("Image11", false);

        questions.Add("Room1Question1", "C"); //Tamam
        questions.Add("Room1Question2", "A"); //Tamam
        questions.Add("Room1Question3", "C");
        questions.Add("Room1Question4", "C");
        questions.Add("Room1Question5", "C");
        questions.Add("Room1Question6", "C");
        questions.Add("Room1Question7", "C");
        questions.Add("Room2Question1", "C"); //Tamam
        questions.Add("Room2Question2", "C"); //Tamam
        questions.Add("Room2Question3", "B");
        questions.Add("Room3Question1", "A");
        questions.Add("Room3Question2", "B");
        questions.Add("Room3Question3", "B");
        questions.Add("Room3Question4", "C");


        //StartCoroutine(PlayDelayed());
    }

    private IEnumerator PlayDelayed()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Play the audio clip
        startAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Dictionary<string, bool> getObjectives()
    {
        return objectives;
    }

    public Dictionary<string, string> getQuestions()
    {
        return questions;
    }

    public void addPoints(int x, string name)
    {
        if (objectives[name] == false)
        {
            collectedPoints += x;
            points.SetText(collectedPoints.ToString());
            objectives[name] = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        string collidedObjectName = other.gameObject.name;
    }

}
