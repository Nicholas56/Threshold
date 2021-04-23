using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadialMenu : MonoBehaviour
{
    public bool access = true;
    PlayerMenu pMenu;

    public KeyCode pauseMenuKey;
    public GameObject pauseScreen;
    [Range(0,1)]
    public float timeSlow = 0;

    public Transform radial;
    public Transform radialMenu;
    List<CreativeButtons> buttons;

    public TMP_Text itemInfo;
    public ItemManager manager;

    // Start is called before the first frame update
    void Start()
    {
        GetRadialButtons();
        pMenu = GetComponent<PlayerMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pMenu.access)
        {
            if (Input.GetKeyDown(pauseMenuKey))
            {
                if (!pauseScreen.activeSelf)
                {
                    pauseScreen.SetActive(true);
                    Time.timeScale = timeSlow;
                    radial.gameObject.SetActive(true);
                    ResetButtons();
                    access = false;
                }
                else
                {
                    HidePause();
                    Time.timeScale = 1;
                }
            }

            if (pauseScreen.activeSelf)
            {
                //Works out the angle of the mouse and the wheel and rotates it to match
                Vector3 orbVector = Input.mousePosition - radial.position;
                float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;

                radial.rotation = Quaternion.Euler(radial.rotation.eulerAngles.x, radial.rotation.eulerAngles.y, angle + 225);
            }
        }
    }
    public void ButtonSelect(CreativeButtons button)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] == button) 
            {
                button.SelectButton();
                if (SaveData.current.profile.radialItems.Count >= i)
                {
                    ItemManager.current.UseItem(SaveData.current.profile.radialItems[i]);
                    ShowItemData(button);
                }
            }
            else { buttons[i].DimButton(false); }
        }
        radial.gameObject.SetActive(false);
    }
    string GetButtonItem(CreativeButtons button)
    {
        int itemNum = buttons.IndexOf(button);
        if (SaveData.current.profile.radialItems.Count <= itemNum) { return null; }
        return SaveData.current.profile.radialItems[itemNum];
    }

    public void ShowItemData(CreativeButtons button)
    {
        string itemName = GetButtonItem(button);
        if (itemName == null) { return; }
        string numRemain = ItemManager.current.GetItemsRemaining(itemName).ToString();
        if (numRemain == "-1") { numRemain = "*"; }
        itemInfo.text = itemName + "\nRemaining: " + numRemain + "\n" + ItemManager.current.GetItem(itemName).GetTimeRemaining();
    }

    public void HideItemData() { itemInfo.text = ""; }
    public Sprite GetItemSprite(CreativeButtons button) 
    {
        string itemString = GetButtonItem(button);
        if (itemString == null) { return null; }
        return ItemManager.current.GetItem(itemString).itemSprite;
    }

    public void ResetButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].DimButton(true);
        }
    }
    void GetRadialButtons()
    {
        buttons = new List<CreativeButtons>();
        for (int i = 0; i < radialMenu.childCount; i++)
        {
            if (radialMenu.GetChild(i).GetComponent<CreativeButtons>()) { buttons.Add(radialMenu.GetChild(i).GetComponent<CreativeButtons>()); }
        }
    }

    public void HidePause()
    {
        pauseScreen.SetActive(false);
        access = true;
    }
}
