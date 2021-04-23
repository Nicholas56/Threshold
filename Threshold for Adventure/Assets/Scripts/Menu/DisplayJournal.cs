using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayJournal : MonoBehaviour
{
    public static DisplayJournal current;
    public TMP_Text log;

    public Transform activeQuestList;
    List<GameObject> activeQuestButtons;
    public Transform completedQuestList;
    List<GameObject> completedQuestButtons;

    Quest questSelect;
    public TMP_Text questParameters;
    public TMP_Text questDescription;

    private void Start()
    {
        GameEvents.current.onDisplayMenu += GetLog;
        current = this;
    }
    private void OnDestroy()
    {
        GameEvents.current.onDisplayMenu -= GetLog;
    }
    public void DisplayPanel()
    {
        GetLog();   SetUpLists();   ShowActiveQuests();
    }
    public void SelectedQuest(string questName)
    {
        //Finds the selected quest and displays the information
        for (int i = 0; i < QuestManager.current.GetQuestList().Count; i++)
        {
            if (QuestManager.current.GetQuestList()[i].questName == questName) { questSelect = QuestManager.current.GetQuestList()[i]; break; }
        }
        questDescription.text = questSelect.questDescription;
        string qType = "";
        switch (questSelect.type)
        {
            case QuestManager.questType.GetItem:qType = "Fetch Items";break;
            case QuestManager.questType.SlayMonster:qType = "Slay Monsters";break;
            case QuestManager.questType.LevelSkill:qType = "Gain Skill Level";break;
        }
        string rType = "";
        switch (questSelect.reward)
        {
            case QuestManager.questReward.Exp:rType = "Experience Reward: ";break;
            case QuestManager.questReward.Money:rType = "Money Reward: ";break;
        }
        questParameters.text = questSelect.questName + ": " + qType + "; " + rType + questSelect.rewardAmount + "; Current: " + questSelect.GetCurrent();
    }
    public void ShowActiveQuests()
    {
        for (int i = 0; i < activeQuestButtons.Count; i++)
        {
            if (i < SaveData.current.profile.activeQuests.Count)
            {
                activeQuestButtons[i].SetActive(true);
                activeQuestButtons[i].GetComponentInChildren<TMP_Text>().text = SaveData.current.profile.activeQuests[i].questName;
            }
            else { activeQuestButtons[i].SetActive(false); }
        }
    }
    public void ShowCompletedQuests()
    {
        for (int i = 0; i < completedQuestButtons.Count; i++)
        {
            if (i < SaveData.current.profile.completedQuests.Count)
            {
                completedQuestButtons[i].SetActive(true);
                completedQuestButtons[i].GetComponentInChildren<TMP_Text>().text = SaveData.current.profile.completedQuests[i].questName;
            }
            else { completedQuestButtons[i].SetActive(false); }
        }
    }
    void GetLog() { log.text = SaveData.current.profile.log; }
    void SetUpLists()
    {
        activeQuestButtons = new List<GameObject>();
        for (int i = 0; i < activeQuestList.childCount; i++)
        {
            activeQuestButtons.Add(activeQuestList.GetChild(i).gameObject);
        }
        completedQuestButtons = new List<GameObject>();
        for (int i = 0; i < completedQuestList.childCount; i++)
        {
            completedQuestButtons.Add(completedQuestList.GetChild(i).gameObject);
        }
        completedQuestList.parent.parent.gameObject.SetActive(false);
    }
}
