using TMPro;
using UnityEngine;

public class DisplaySkills : MonoBehaviour
{
    public TMP_Text skillsText;
    PlayerProfile profile;

    private void Start()
    {
        profile = SaveData.current.profile;
    }

    public void DisplayText()
    {//<link=linkKey>LinkKey</link> This is the format to turn words into click links that bring up the popup. Also adds color.
        //Shows the current skills for the character
        skillsText.text = "Character Skills: ";
        for (int i = 0; i < SaveData.current.profile.skills.Count; i++)
        {
            string str = SaveData.current.profile.skills[i].name;
            skillsText.text += "<color=green><link=" + str + ">"+str+"</link></color>, ";
        }
    }
}
