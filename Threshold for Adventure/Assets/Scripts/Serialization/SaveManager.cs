using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public GameObject loadButtonPrefab;
    public Transform loadArea;
    List<GameObject> buttons;

    private void Start()
    {
        GameEvents.current.onSave += OnSave;
    }
    private void OnDestroy()
    {
        GameEvents.current.onSave -= OnSave;
    }

    public void OnSave()
    {
        //Called to enact a save function
        SerializationManager.Save(SaveData.current.profile.playerName, SaveData.current);
    }

    public void OnLoad(string saveFile)
    {
        //Called to load a file
        SaveData.current = SerializationManager.Load(saveFile);
    }

    public string[] saveFiles;
    public void GetLoadFiles()
    {
        //Produces an array of all the save files for this game
        if(!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }

        saveFiles = Directory.GetFiles(Application.persistentDataPath + "/saves/");
    }

    public void ShowLoadScreen()
    {
        //Populates the save files in the load area. adds button function for loading
        GetLoadFiles();
        if (buttons == null) { GetButtons(); }

        foreach(Transform button in loadArea) { button.gameObject.SetActive(false); }

        for (int i = 0; i < saveFiles.Length; i++)
        {
            buttons[i].SetActive(true);
            string fileName = saveFiles[i];
            string[] nameParts = fileName.Split('/');
            buttons[i].GetComponentInChildren<TMP_Text>().text = nameParts[nameParts.Length-1];

        }
    }

    void GetButtons()
    {
        buttons = new List<GameObject>();
        for (int i = 0; i < loadArea.childCount; i++)
        {
            buttons.Add(loadArea.GetChild(i).gameObject);
        }
    }
}
