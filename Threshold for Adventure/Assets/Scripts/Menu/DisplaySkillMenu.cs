using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplaySkillMenu : MonoBehaviour
{
    public Transform skillArea;
    List<GameObject> buttons;
    Skill skill;

    public TMP_Text skillTitle;
    public UIProgressBar progress;
    public TMP_Text progressTxt;
    public TMP_Text description;

    public TMP_Text expAmount;

    public void DisplaySkills()
    {
        if (buttons == null) { GetButtons(); }

        foreach (Transform button in skillArea) { button.gameObject.SetActive(false); }

        for (int i = 0; i < SaveData.current.profile.skills.Count; i++)
        {//Sets the button to active and shows the skill name and the level
            buttons[i].SetActive(true);
            string displayTxt = SaveData.current.profile.skills[i].name;
            displayTxt += " - Lvl: " + SaveData.current.profile.GetSkill(displayTxt).GetLevel();
            buttons[i].GetComponentInChildren<TMP_Text>().text = displayTxt;
        }

        ShowExpAmount();
    }

    public void SelectSkill(Skill currentSkill)
    {
        skill = currentSkill;
        skillTitle.text = skill.name + " - Lvl: " + skill.GetLevel();

        //May need more information about skills to proceed here
        progressTxt.text = skill.CalculateExpToNextLevel() + "xp to next level";
        progress.current = skill.GetExp();progress.maximum = skill.GetXpThreshold(); 
        if (skill.GetLevel() > 0) { progress.minimum = ((int)Mathf.Pow(2, skill.GetLevel() - 1)) + (skill.levelFactor*(skill.GetLevel() - 1)); } else { progress.minimum = 0; }
    }

    public void AddExpToSkill()
    {
        if (SaveData.current.profile.exp > 0 && skill!=null)
        {
            SaveData.current.profile.exp--;
            skill.AddExp(1);
            SelectSkill(skill);
            ShowExpAmount();
        }
    }
    void ShowExpAmount()
    {
        expAmount.text = SaveData.current.profile.exp.ToString("n0") + " xp to assign";
    }

    void GetButtons()//This is a REPEATED function. See: SaveManager.
    {
        buttons = new List<GameObject>();
        for (int i = 0; i < skillArea.childCount; i++)
        {
            buttons.Add(skillArea.GetChild(i).gameObject);
        }
    }

    private void Start()
    {
        GameEvents.current.onDisplayMenu += ShowExpAmount;
    }
    private void OnDestroy()
    {
        GameEvents.current.onDisplayMenu -= ShowExpAmount;
    }
}
