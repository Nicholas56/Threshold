using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChoices : MonoBehaviour
{
    List<bool> toggles = new List<bool> { false, false, false, false, false, false };
    PlayerProfile profile;
    private void Start()
    {
        profile = SaveData.current.profile;
    }
    public void ToggleSkill(int value)
    {
        //For the toggle options adds or removes a skill based on if ticked or not.
        switch (value)
        {
            case 0:
                if (toggles[value]) { ReduceSkill("Swordsmanship"); toggles[value] = false; }
                else { AddSkill("Swordsmanship");toggles[value] = true; WeaponManager.current.Equip("Sword"); }
                break;
            case 1:
                if (toggles[value]) { ReduceSkill("Magic"); toggles[value] = false; }
                else { AddSkill("Magic"); toggles[value] = true; WeaponManager.current.Equip("Magic"); }
                break;
            case 2:
                if (toggles[value]) { ReduceSkill("Archery"); toggles[value] = false; }
                else { AddSkill("Archery"); toggles[value] = true; WeaponManager.current.Equip("Bow"); }
                break;
            case 3:
                if (toggles[value]) { ReduceSkill("Intimidation"); toggles[value] = false; }
                else { AddSkill("Intimidation"); toggles[value] = true; }
                break;
            case 4:
                if (toggles[value]) { ReduceSkill("Charm"); toggles[value] = false; }
                else { AddSkill("Charm"); toggles[value] = true; }
                break;
            case 5:
                if (toggles[value]) { ReduceSkill("Barter"); toggles[value] = false; }
                else { AddSkill("Barter"); toggles[value] = true; }
                break;
        }
    }

    void ReduceSkill(string skillToReduce)
    {
        //Reduces a skill or removes it completely
        if (profile.GetSkill(skillToReduce)!=null) { profile.RemoveSkill(skillToReduce); }
    }

    void AddSkill(string skillToAdd)
    {
        //Adds a skill or compounds it, if it already exists
        if (profile.GetSkill(skillToAdd) == null) { profile.AddSkill(skillToAdd); }
    }
}
