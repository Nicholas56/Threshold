using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTypeDialogue : MonoBehaviour, IPointerClickHandler
{
    TMP_Text type;
    string skillType;
    private void Awake()
    {
        type = GetComponentInChildren<TMP_Text>();
    }

    public void ChangeText(string changeTo) { type.text = changeTo; skillType = changeTo; }

    public void OnPointerClick(PointerEventData eventData)
    {
        GlobalDialogue.Instance.ResetButtons();
        if (skillType == "None") { GlobalDialogue.Instance.GetResponses(""); }else
        GlobalDialogue.Instance.GetResponses(skillType);
    }
}
