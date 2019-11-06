using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendToPersonScript : MonoBehaviour
{
    public Graphic graphicForTint;
    public TMPro.TextMeshProUGUI buttonText;
    public int destination = -1;
    GameplayLogic manager;

    public void SetStuff(int dest, string buttonDestinationString, Color c, GameplayLogic logic)
    {
        graphicForTint.color = c;
        manager = logic;
        destination = dest;
        SetButtonName(buttonDestinationString);
    }

    public void SetButtonName(string name)
    {
        buttonText.text = "Send to " + name;
    }

    public void SendTrainMessage()
    {
        // send it to that person!
        if (destination >= 0)
        {
            manager.SpawnNewTrain(destination);
        }
    }
}
