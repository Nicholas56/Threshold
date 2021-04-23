using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayLog : MonoBehaviour
{
    public TMP_Text logText;

    private void Start()
    {
        GameEvents.current.onLogEntry += EnterIntoLog;
        logText.text = SaveData.current.profile.log;
    }
    private void OnDestroy()
    {
        GameEvents.current.onLogEntry -= EnterIntoLog;
    }

    void EnterIntoLog(string entry)
    {
        logText.text = entry + "\n" + logText.text;
        SaveData.current.profile.log = logText.text;
        GameEvents.current.SaveGame();
    }
}
