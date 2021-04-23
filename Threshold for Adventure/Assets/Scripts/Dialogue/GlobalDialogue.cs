using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlobalDialogue : MonoBehaviour
{
    public static GlobalDialogue Instance;
    public List<DialogueScriptable> dialogues;
    public List<DialogueResponse> responses;

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public GameObject dialogueScreen;

    public Transform responseTypeArea;
    List<ButtonTypeDialogue> types;
    public Transform responseArea;
    List<ButtonResponseDialogue> pResponses;

    public GameObject decisionPanel;
    DialogueScriptable.DialogueEvent eventDecision;

    NPCDialogue npc;

    private void Awake()
    {
        Instance = this;
        dialogueScreen.SetActive(true);
        SetupButtons();
        dialogueScreen.SetActive(false);
    }

    public void SetupDialogue(string nPCName, NPCDialogue response)
    {
        //Sets the name, butons and dialogues, while creating a cache of the npc
        nameText.text = nPCName;
        ResetButtons();
        dialogueText.text = response.GetDialogue();
        npc = response;

    }

    void SetupButtons()
    {//Caches the objects in the button areas
        types = new List<ButtonTypeDialogue>();
        foreach(Transform obj in responseTypeArea)
        {
            types.Add(obj.GetComponent<ButtonTypeDialogue>());
        }
        pResponses = new List<ButtonResponseDialogue>();
        foreach(Transform obj in responseArea)
        {
            pResponses.Add(obj.GetComponent<ButtonResponseDialogue>());
        }
    }
    public void ResetButtons() { HideButtons();DisplayButtons(); HideDecisionPanel(); }
    public NPCDialogue GetNPC() { return npc; }

    public void YesAnswer()
    {
        HideDecisionPanel();
        switch (eventDecision)
        {
            case DialogueScriptable.DialogueEvent.None: break;
            case DialogueScriptable.DialogueEvent.Quest:
                GameEvents.current.BeginQuest(npc.personalDialogue[npc.currentDialogue].eventId);
                break;
        }
    }
    public void NoAnswer()
    {
        HideDecisionPanel();
    }
    void ShowDecisionPanel() { decisionPanel.SetActive(true); }
    void HideDecisionPanel() { decisionPanel.SetActive(false); }

    void DisplayButtons()
    {
        types[0].gameObject.SetActive(true);
        types[0].ChangeText("None");
        for (int i = 1; i < SaveData.current.profile.skills.Count+1; i++)
        {
            types[i].gameObject.SetActive(true);
            types[i].ChangeText(SaveData.current.profile.skills[i-1].name);
        }
    }
    void HideButtons()
    {
        for (int i = 0; i < types.Count; i++)
        {
            types[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < pResponses.Count; i++)
        {
            pResponses[i].gameObject.SetActive(false);
        }
    }

    public void GetResponses(string keyword)
    {
        List<DialogueResponse> fullResponses = new List<DialogueResponse>();
        //Goes through the global and npc responses and puts them on the buttons
        //If the dialogue doesn't originate from the NPC, then responses can't be extracted
        if (npc.currentDialogue != -1)
        {
            foreach (DialogueResponse response1 in npc.personalDialogue[npc.currentDialogue].GetSkillResponses(keyword))
            {
                fullResponses.Add(response1);
            }
        }
        foreach (DialogueResponse response in responses)
        {
            //May be necessary to remove duplicate keyword holding dialogue responses (should be done through design)
            if (response.skillToCheck == keyword) { fullResponses.Add(response); }
        }

        for (int i = 0; i < fullResponses.Count; i++)
        {
            pResponses[i].gameObject.SetActive(true);
            pResponses[i].SetupButton(fullResponses[i]);
        }
    }

    public void GetNewDialogue(string keyWord,int skillNameNum)
    {
        ResetButtons();
        //Searches for dialogue that matches the keyword given, npc first, then global
        foreach(DialogueScriptable dialogue in npc.personalDialogue)
        {
            if (dialogue.keyWord == keyWord)
            {
                if (dialogue.CheckSkill(skillNameNum))
                {
                    dialogueText.text = dialogue.dialogue;
                    npc.currentDialogue = npc.personalDialogue.IndexOf(dialogue);
                }
                else { dialogueText.text = dialogue.defaultFailResponse;npc.currentDialogue = 0; }
            }
        }
        foreach(DialogueScriptable dialogue1 in dialogues)
        {
            if (dialogue1.keyWord == keyWord)
            {
                if (dialogue1.CheckSkill(skillNameNum))
                {
                    dialogueText.text = dialogue1.dialogue;
                    npc.currentDialogue = -1;
                }
                else { dialogueText.text = dialogue1.defaultFailResponse; npc.currentDialogue = 0; }
            }
        }

        eventDecision = npc.personalDialogue[npc.currentDialogue].dialogueEvent;
        switch (eventDecision)
        {
            //Choose what event is triggered by this dialogue
            case DialogueScriptable.DialogueEvent.None:

                break;
            case DialogueScriptable.DialogueEvent.Barter:
                GameEvents.current.CallBarter();
                break;
            case DialogueScriptable.DialogueEvent.Quest:
                if (QuestManager.current.CheckIfCompleted(npc.personalDialogue[npc.currentDialogue].eventId))
                {
                    dialogueText.text = "You already did what I asked.";
                }
                else if (QuestManager.current.CheckIfActive(npc.personalDialogue[npc.currentDialogue].eventId))
                {
                    dialogueText.text = "You're already doing something for me.";
                }
                else { ShowDecisionPanel(); }
                break;
        }
    }

    public void EndDialogue()
    {
        npc.ResetDialogue();
        ResetButtons();
        dialogueScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
