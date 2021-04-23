using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButtonScript : MonoBehaviour
{
    DisplaySkillMenu skillMenu;
    Skill skillName;
    private void Awake()
    {
        skillMenu = FindObjectOfType<DisplaySkillMenu>();
    }
    private void OnEnable()
    {
        skillName = SaveData.current.profile.skills[transform.GetSiblingIndex()];
    }

    public void SelectButton()
    {
        skillMenu.SelectSkill(skillName);
    }
}
