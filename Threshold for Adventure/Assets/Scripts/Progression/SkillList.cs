using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SkillList")]
public class SkillList : ScriptableObject
{
    public List<Skill> skillList;
}


[System.Serializable]
public class Skill
{
    public string name;
    int level = 0;
    int exp = 0;
    public int levelFactor = 24;
    public string description;

    public Skill(string newSkill) { name = newSkill; }

    public int GetLevel() { return level; }
    public int GetExp() { return exp; }

    public void AddExp(int expToAdd)
    {
        exp += expToAdd;
        if (CalculateExpToNextLevel() < 0)
        {
            level++;
            GameEvents.current.LogEntry("Congratulations! You levelled up in <color=#7f8fa6>" + name + "</color>. You are now level <color=#7f8fa6>" + level + "</color>.");
        }
    }

    public int CalculateExpToNextLevel()
    {
        int num = GetXpThreshold();
        return (num - exp);
    }

    public int GetXpThreshold() { return ((int)Mathf.Pow(2, level)) + (levelFactor*level); }
}
