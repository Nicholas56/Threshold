using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterHolder modelList;
    int modelNum;
    public Dictionary<string, int> skills;
    public List<string> skillList;

    public static Character instance;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            skills = new Dictionary<string, int>();
            skillList = new List<string>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    //Statically holds the current character skills and the model used for this playthrough. will be needed for saving and loading
    public static void StoreModel(int modelStoreNum) { instance.modelNum = modelStoreNum; }
    public static GameObject GetModel() { return instance.modelList.charChoices[instance.modelNum]; }
    public static void AddSkill(string skillToAdd) { instance.skills.Add(skillToAdd, 0); instance.skillList.Add(skillToAdd); }
    public static void RemoveSkill(string skillToRemove) { instance.skills.Remove(skillToRemove); instance.skillList.Remove(skillToRemove); }
    public static void IncreaseSkill(string skillToIncrease) { instance.skills[skillToIncrease]++; }
    public static void DecreaseSkill(string skillToDecrease) { instance.skills[skillToDecrease]--; }
    public static void LoadSkills(Dictionary<string,int> skillLoad, List<string> listOfSkills) { instance.skills = skillLoad; instance.skillList = listOfSkills; }
    public static Dictionary<string,int> GetSkills() { return instance.skills; }
    public static List<string> GetSkillList() { return instance.skillList; }
}
