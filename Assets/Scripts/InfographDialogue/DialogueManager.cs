using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    private bool dialogueIsOpen = false;


    private Queue<string> sentences;
    private Queue<GameObject> images;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
        images = new Queue<GameObject>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("starting conversation with" + dialogue.name);
        dialogueIsOpen = true;
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (GameObject image in dialogue.objects)
        {
            images.Enqueue(image);
        }

        DisplayNextSentence(dialogue);
    }

    public bool controlDialogue()
    {
        if (dialogueIsOpen)
        {
            return true;
        }
        return false;
    }

    public bool DisplayNextSentence(Dialogue dialogue)
    {
        if (sentences.Count == 0)
        {
            foreach (GameObject infograph in dialogue.objects)
            {
                if (dialogue.objects.Length - 1 != Array.IndexOf(dialogue.objects, infograph))
                {
                    infograph.SetActive(false);
                }
            }

            dialogueIsOpen = false;
            return false;
        }

        string sentence = sentences.Dequeue();
        GameObject image = images.Dequeue();
        image.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        return true;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
