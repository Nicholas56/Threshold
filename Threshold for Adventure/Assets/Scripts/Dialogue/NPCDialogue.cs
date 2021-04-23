using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class NPCDialogue : MonoBehaviour, IPointerClickHandler
{
    public string NPCName;
    
    public List<DialogueScriptable> personalDialogue;
    public int currentDialogue;
    public List<string> npcItems;
    public List<int> itemNums;

    public string GetDialogue() { return personalDialogue[currentDialogue].dialogue; }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetEvent();
    }

    public void NPCAction()
    {
        GlobalDialogue.Instance.dialogueScreen.SetActive(true);
        currentDialogue = 0;
        GlobalDialogue.Instance.SetupDialogue(NPCName, this);
        Time.timeScale = 0;

        GameEvents.current.onCurrentAction -= NPCAction;
        GameEvents.current.LogEntry("You spoke with <color=#00a8ff>" + NPCName + "</color>.");
    }

    public void AddItemToNpc(string itemToAdd,int numToAdd) { npcItems.Add(itemToAdd);itemNums.Add(numToAdd); }
    public void ChangeItemNum(string itemToChange,int change) { int num = npcItems.IndexOf(itemToChange);itemNums[num] += change; }

    public void ResetDialogue() { currentDialogue = 0; }

    public void SetEvent()
    {
        GameEvents.current.ResetCurrentActionSubscriptions();
        GameEvents.current.onCurrentAction += NPCAction;
    }
}

