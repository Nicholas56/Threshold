using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonResponseDialogue : MonoBehaviour, IPointerClickHandler
{
    TMP_Text response;
    string keyWord;
    string skillWord;
    private void Awake()
    {
        response = GetComponentInChildren<TMP_Text>();
    }

    public void SetupButton(DialogueResponse dialogue)
    {
        response.text = dialogue.playerResponse;
        keyWord = dialogue.keyWord;
        skillWord = dialogue.skillToCheck;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Global search for keyword and put in next dialogue
        if (skillWord == "") { GlobalDialogue.Instance.GetNewDialogue(keyWord, 0); }else
        GlobalDialogue.Instance.GetNewDialogue(keyWord,SaveData.current.profile.GetSkill(skillWord).GetLevel());
    }
}
