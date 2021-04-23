using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButtonScript : MonoBehaviour
{
    string[] files;
    SaveManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<SaveManager>();
    }
    private void OnEnable()
    {
        files = manager.saveFiles;
    }

    public void SelectFile()
    {
        //When clicked, this button should find the correct file by the position of the button
        int fileNum = transform.GetSiblingIndex();
        manager.OnLoad(files[fileNum]);
    }
}
