using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(int levelToLoad)
    {
        //Loads to the scene indicated in the inspector
        SceneManager.LoadScene(levelToLoad);
    }

    public void AddLevel(int levelToAdd)
    {
        //Adds the scene indicated to the existing scenes
        SceneManager.LoadScene(levelToAdd, LoadSceneMode.Additive);
    }

    public void LeaveGame()
    {
        //Exits the game
        Application.Quit();
    }
}
