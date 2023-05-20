using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryModus : MonoBehaviour
{
    public const int maxPoints = 1000;

    private int collectedPoints = 0;

    public GameObject collider1;
    public GameObject collider2;
    public GameObject collider3;
    public TextMeshProUGUI points;

    private Dictionary<string, bool> rooms = new Dictionary<string, bool>();
    // Start is called before the first frame update
    void Start()
    {
        rooms.Add("Room1", false);
        rooms.Add("Room2", false);
        rooms.Add("Room3", false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Dictionary<string, bool> getRooms()
    {
        return rooms;
    }

    private void OnTriggerExit(Collider other)
    {
        string collidedObjectName = other.gameObject.name;
        int result;
        switch (collidedObjectName)
        {
            case "TestCollider1":
                Debug.Log("First room is visited.");
                if (rooms["Room1"] == false)
                {
                    collectedPoints += 100;
                }
                points.SetText(collectedPoints.ToString());
                rooms["Room1"] = true;
                break;
            case "TestCollider2":
                Debug.Log("Second room is visited.");
                if (rooms["Room2"] == false)
                {
                    collectedPoints += 100;
                }
                points.SetText(collectedPoints.ToString());
                rooms["Room2"] = true;
                break;
            case "TestCollider3":
                Debug.Log("Third room is visited.");
                if (rooms["Room3"] == false)
                {
                    collectedPoints += 100;
                }
                points.SetText(collectedPoints.ToString());
                rooms["Room3"] = true;
                break;
            default:
                break;
        }
    }

}
