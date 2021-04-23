using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HolderInterface : MonoBehaviour
{
    public CharacterHolder holder;
    public GameObject currentChar;
    PlayerProfile profile;

    public TMP_Text listNum;
    int listPos = 0;

    private void Start()
    {
        profile = SaveData.current.profile;
        /*if (profile.skills == null)
        {
            profile.skills = new Dictionary<string, int>();
            profile.skillList = new List<string>();
            profile.modelChoice = 0;
        }*/
        LoadCharModel();
        FindObjectOfType<DisplaySkills>().DisplayText();
    }
    public void Next()
    {
        //Loads the next model in the list of models (or loading the first)
        listPos++;
        if (listPos == holder.charChoices.Count) { listPos = 0; }
        LoadCharModel();
        listNum.text = (listPos + 1).ToString();
    }
    public void Previous()
    {
        //Loads the previous model in the list of models (or loading the last)
        listPos--;
        if (listPos < 0) { listPos = holder.charChoices.Count - 1; }
        LoadCharModel();
        listNum.text = (listPos + 1).ToString();
    }

    void LoadCharModel()
    {
        //Deletes the current model, removes the associated skills, then adds the new model and the new skills
        List<string> skills = GetSkills(currentChar);
        for (int i = 0; i < skills.Count; i++)
        {
            if (profile.GetSkill(skills[i])!=null) { profile.RemoveSkill(skills[i]); }
        }
        Destroy(currentChar);
        currentChar = Instantiate(holder.charChoices[listPos], transform);
        skills = GetSkills(currentChar);
        for (int j = 0; j < skills.Count; j++)
        {
            if (profile.GetSkill(skills[j])==null) { profile.AddSkill(skills[j]); }
        }
        profile.StoreModel(listPos);
    }

    List<string> GetSkills(GameObject model)
    {
        //Returns the list of skills attached to the model
        CharInfo info = model.GetComponent<CharInfo>();
        return info.skills;
    }

}
