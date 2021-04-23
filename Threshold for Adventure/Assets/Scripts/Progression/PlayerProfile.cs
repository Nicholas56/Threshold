using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProfile
{
    public string playerName;

    public List<Skill> skills;
    public int modelChoice;

    public List<string> inventory;
    public List<int> itemNums;
    public List<string> radialItems;
    public int radialSlots;
    public string weapon;

    public List<Quest> activeQuests;
    public List<Quest> completedQuests;
    public string log;
    public Vector3 lastPos;

    public int money;
    public int exp;
    public int health;
    public int armor;

    //Modifiers for various player attributes
    public  void StoreModel(int modelStoreNum) { modelChoice = modelStoreNum; }
    //Skills
    public  void AddSkill(string skillToAdd) { skills.Add(new Skill(skillToAdd)); }
    public  void RemoveSkill(string skillToRemove) { skills.Remove(GetSkill(skillToRemove)); }
    public  Skill GetSkill(string skill) { foreach (Skill _skill in skills) { if (_skill.name == skill) return _skill;  };return null; }
    public int GetExp() { return exp; }
    //Inventory
    public void AddItem(string itemName, int NumOfItem)
    {
        inventory.Add(itemName); itemNums.Add(NumOfItem);
        if (QuestManager.current.CheckFor(itemName) != null) { QuestManager.current.CheckFor(itemName).AddToCurrent(NumOfItem); }
    }
    public void RemoveItem(string itemName) 
    { 
        int num = inventory.IndexOf(itemName);
        if (QuestManager.current.CheckFor(itemName) != null) { QuestManager.current.CheckFor(itemName).ReduceCurrent(itemNums[num]); }
        inventory.RemoveAt(num);itemNums.RemoveAt(num);
    }
    public void AddRadial(string item) { radialItems.Add(item); }
    public void RemoveRadial(string item) { radialItems.Remove(item); }
    public void SetRadialSlots(int slots) { radialSlots = slots; }
    //Money
    public bool HasEnoughMoney(int expense) { if (money - expense < 0) { return false; } else { return true; } }

    public PlayerProfile()
    {
        skills = new List<Skill>();

        inventory = new List<string>();
        itemNums = new List<int>();
        radialItems = new List<string>();
        radialSlots = 4;

        activeQuests = new List<Quest>();
        completedQuests = new List<Quest>();
        log = "This is the log where your actions are recorded";

        health = 10;
        armor = 30;
    }
}
