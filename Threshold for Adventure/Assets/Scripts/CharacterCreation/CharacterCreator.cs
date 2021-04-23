using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCreator : MonoBehaviour
{
    public GameObject welcomeScreen;
    public TMP_InputField nameField;

    private void Awake()
    {
        SaveData.current.profile = new PlayerProfile();
        Time.timeScale = 0;
    }

    public void InputName()
    {
        string nameOf = welcomeScreen.GetComponentInChildren<TMP_InputField>().text;
        SaveData.current.profile.playerName = nameOf;
        SaveData.current.profile.AddItem("Potion", -1);
        SaveData.current.profile.AddItem("Repair Powder", 10);
        Time.timeScale = 1;
        welcomeScreen.SetActive(false);
        nameField.text = nameOf;
    }
}
