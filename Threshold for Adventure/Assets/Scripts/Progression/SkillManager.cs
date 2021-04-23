using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public SkillList skills;
    public static SkillManager current;

    private void Awake()
    {
        current = this;
    }

    public Skill GetSkill(string skillName)
    {
        for (int i = 0; i < skills.skillList.Count; i++)
        {
            if (skills.skillList[i].name == skillName) { return skills.skillList[i]; }
        }
        return null;
    }
    public bool CheckPlayerHasSkill(string skillName)
    {
        for (int i = 0; i < SaveData.current.profile.skills.Count; i++)
        {
            if (SaveData.current.profile.skills[i].name == skillName) { return true; }
        }
        return false;
    }
}
