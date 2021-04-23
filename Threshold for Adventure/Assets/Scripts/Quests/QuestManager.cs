using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public enum questType { SlayMonster, GetItem, LevelSkill}
    public enum questReward { Money, Exp}
    public static QuestManager current;
    public QuestScriptable questList;

    private void Start()
    {
        current = this;
        GameEvents.current.onBeginQuest += BeginQuest;
        GameEvents.current.onEndQuest += CompleteQuest;
    }
    private void OnDestroy()
    {
        GameEvents.current.onBeginQuest -= BeginQuest;
        GameEvents.current.onEndQuest -= CompleteQuest;
    }
    public List<Quest> GetQuestList() { return questList.quests; }
    public Quest CheckFor(string objectName)
    {
        for (int i = 0; i < SaveData.current.profile.activeQuests.Count; i++)
        {
            if (SaveData.current.profile.activeQuests[i].targetObject == objectName)
            {
                return SaveData.current.profile.activeQuests[i];
            }
            else continue;
        }
        return null;
    }
    public bool CheckIfCompleted(int id)
    {//Checks the list of completed quests against the id given
        for (int i = 0; i < SaveData.current.profile.completedQuests.Count; i++)
        {
            if (SaveData.current.profile.completedQuests[i].id == id) { return true; }
        }
        return false;
    }
    public bool CheckIfActive(int id)
    {//Checks the list of completed quests against the id given
        for (int i = 0; i < SaveData.current.profile.activeQuests.Count; i++)
        {
            if (SaveData.current.profile.activeQuests[i].id == id) { return true; }
        }
        return false;
    }

    void BeginQuest(int id)
    {
        SaveData.current.profile.activeQuests.Add(questList.quests[id]);
        GameEvents.current.LogEntry("The quest: <color=#9c88ff>" + questList.quests[id].questName + "</color> has begun.");
    }
    void CompleteQuest(int id)
    {
        SaveData.current.profile.activeQuests.Remove(questList.quests[id]);
        switch (questList.quests[id].reward)
        {
            case questReward.Exp:SaveData.current.profile.exp += questList.quests[id].rewardAmount;break;
            case questReward.Money:SaveData.current.profile.money += questList.quests[id].rewardAmount;break;
        }
        if (!questList.quests[id].repeatable)
        {//If the quest is repeatable, it is not added to the complete quest list
            SaveData.current.profile.completedQuests.Add(questList.quests[id]);
        }
        GameEvents.current.LogEntry("The quest: <color=#9c88ff>" + questList.quests[id].questName + "</color> has been completed.");
    }
}
