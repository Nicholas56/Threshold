using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestButton : MonoBehaviour
{
    TMP_Text buttonText;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
    }

    public void ButtonSelected()
    {
        DisplayJournal.current.SelectedQuest(buttonText.text);
    }
}
