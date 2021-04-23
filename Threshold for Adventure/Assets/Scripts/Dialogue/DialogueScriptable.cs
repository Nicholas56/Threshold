using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "dialogueScriptable")]
public class DialogueScriptable : ScriptableObject
{
    public string keyWord;
    public int skillCheck;

    [TextArea(3, 15)]
    public string dialogue;

    public DialogueResponse[] playerResponses;


    public string defaultFailResponse;

    public enum DialogueEvent { None, Barter, Quest}
    public DialogueEvent dialogueEvent;
    public int eventId;

    public List<DialogueResponse> GetSkillResponses(string skill)
    {
        //Returns the responses that match a particular skill
        List<DialogueResponse> responses = new List<DialogueResponse>();
        foreach(DialogueResponse response in playerResponses)
        {
            if (response.skillToCheck == skill) { responses.Add(response); }
        }

        return responses;
    }
    public bool CheckSkill(int skillLevel) { if (skillCheck > skillLevel) { return false; } else return true; }
}


[System.Serializable]
public class DialogueResponse
{
    public string skillToCheck;
    public string keyWord;

    [TextArea(2,15)]
    public string playerResponse;
}
