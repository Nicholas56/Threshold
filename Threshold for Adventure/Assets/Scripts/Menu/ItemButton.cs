using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemButton : MonoBehaviour
{
    string itemString;
    public TMP_Text nameTxt;
    public TMP_Text numTxt;
    public TMP_Text coolTxt;
    ItemManager manager;

    public TMP_Text descriptionText;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (manager == null)
        {
            manager = FindObjectOfType<ItemManager>();
        }
    }

    public void SetText(string itemName,int itemNum, float coolDown)
    {
        nameTxt.text = itemName; itemString = itemName;
        if (itemNum == -1) { numTxt.text = "*"; }
        else { numTxt.text = itemNum.ToString(); }
        coolTxt.text = coolDown.ToString() + 's';
    }

    public void GetItemDescription()
    {
        descriptionText.text = ItemManager.current.GetItemDescription(itemString);
    }

    public void PutInRadialSlot()
    {
        if (!SaveData.current.profile.radialItems.Contains(itemString)) { SaveData.current.profile.AddRadial(itemString); }
    }
    public void RemoveFromRadial()
    {
        if (SaveData.current.profile.radialItems.Contains(itemString)) { SaveData.current.profile.RemoveRadial(itemString); }
    }
}
