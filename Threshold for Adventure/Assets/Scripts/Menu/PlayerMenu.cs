using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public bool access = true;
    RadialMenu radial;

    public KeyCode menuKey;
    public GameObject menuScreen;
    [Range(0, 1)]
    public float timeSlow = 0;

    private void Start()
    {
        radial = GetComponent<RadialMenu>();
    }
    void Update()
    {
        if (radial.access)
        {
            if (Input.GetKeyDown(menuKey))
            {
                if (!menuScreen.activeSelf)
                {
                    menuScreen.SetActive(true);
                    GameEvents.current.DisplayMenu();
                    Time.timeScale = timeSlow;
                    access = false;
                }
                else
                {
                    menuScreen.SetActive(false);
                    Time.timeScale = 1;
                    access = true;
                }
            }
        }
    }
}
