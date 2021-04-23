using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestScriptable")]
public class QuestScriptable : ScriptableObject
{
    public List<Quest> quests;
}

[System.Serializable]
public class Quest
{
    public int id;
    public bool repeatable;
    public string questName;
    public QuestManager.questType type;
    public string targetObject;
    public int targetNum;
    int currentNum;
    public string questDescription;

    public QuestManager.questReward reward;
    public int rewardAmount;

    public void AddToCurrent(int amount) { currentNum+=amount; CheckTarget(); }
    public void ReduceCurrent(int amount) { currentNum-=amount; }
    public int GetCurrent() { return currentNum; }
    public void CheckTarget()
    {
        if (currentNum >= targetNum)
        {
            GameEvents.current.EndQuest(id);
        }
    }
}
