using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public Transform itemArea;
    public Transform wheelSlotArea;
    List<GameObject> iButtons;
    List<GameObject> sButtons;
    ItemManager manager;

    private void Awake()
    {
        //manager = FindObjectOfType<ItemManager>();
    }

    public void DisplayItems()
    {
        if (iButtons == null) { GetButtons(itemArea,wheelSlotArea); }
        //if (manager == null) { manager = FindObjectOfType<ItemManager>(); }

        foreach (Transform button in itemArea) { button.gameObject.SetActive(false); }

        for (int i = 0; i < SaveData.current.profile.inventory.Count; i++)
        {//Sets the button to active and shows the skill name and the level
            iButtons[i].SetActive(true);
            Item item = ItemManager.current.GetItem(SaveData.current.profile.inventory[i]);
            iButtons[i].GetComponentInChildren<ItemButton>().SetText(item.itemName,SaveData.current.profile.itemNums[i],item.cooldown);
        }
        //The same but for the radial buttons, if saved (needs to be added to the drag and drop scripts)
        foreach (Transform button in wheelSlotArea) { button.gameObject.SetActive(false); }
        for (int i = 0; i < SaveData.current.profile.radialSlots; i++)
        {
            sButtons[i].SetActive(true);
        }

        for (int i = 0; i < SaveData.current.profile.radialItems.Count; i++)
        {//Sets the button to active and shows the skill name and the level
            Item item = ItemManager.current.GetItem(SaveData.current.profile.radialItems[i]);
            int numOfItem = SaveData.current.profile.inventory.IndexOf(SaveData.current.profile.radialItems[i]);
            sButtons[i].GetComponentInChildren<ItemButton>().SetText(item.itemName, SaveData.current.profile.itemNums[numOfItem],item.cooldown);
        }
    }


    void GetButtons(Transform area, Transform area2)
    {
        iButtons = new List<GameObject>();
        for (int i = 0; i < area.childCount; i++)
        {
            iButtons.Add(area.GetChild(i).gameObject);
        }
        sButtons = new List<GameObject>();
        for (int i = 0; i < area2.childCount; i++)
        {
            sButtons.Add(area2.GetChild(i).gameObject);
        }
    }
}
