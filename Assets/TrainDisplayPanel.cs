using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TrainDisplayPanel : MonoBehaviour
{
    public TMP_Text fromField;
    public TMP_Text toField;
    public TMP_Text leftChoice;
    public TMP_Text rightChoice;
    public TMP_Text resultChoice;

    public GameplayLogic manager;

    public void DisplayTrainStuff(Train t)
    {
        DisplayText(fromField, manager.GetPlayerName(t.networkObject.From));
        DisplayText(toField, manager.GetPlayerName(t.networkObject.To));
        DisplayText(leftChoice, t.firstChoice);
        DisplayText(rightChoice, t.secondChoice);
        DisplayText(resultChoice, t.response);

        if (t.networkObject.From >= 0 && t.networkObject.From < manager.PlayerColors.Count)
        {
            fromField.color = manager.PlayerColors[t.networkObject.From];
        }
        if (t.networkObject.To >= 0 && t.networkObject.To < manager.PlayerColors.Count)
        {
            toField.color = manager.PlayerColors[t.networkObject.To];
        }
    }

    private void DisplayText(TMP_Text text, string s)
    {
        if (text != null)
        {
            text.text = s;
        }
    }
}
