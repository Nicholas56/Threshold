using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TradeButton : MonoBehaviour
{
    TradeMenu menu;

    string itemString;
    public TMP_Text nameTxt;
    public TMP_Text numTxt;
    public TMP_Text costTxt;
    public bool playerItem;
    public bool isTrade;

    private void Awake()
    {
        if (menu == null)
        {
            menu = FindObjectOfType<TradeMenu>();
            menu.AddToMenu(this);
        }
    }
    public void GetButton()
    {
        if (menu == null)
        {
            menu = FindObjectOfType<TradeMenu>();
            TradeMenu.current.AddToMenu(this);
        }
    }
    public void SetText(string itemName, int itemNum)
    {
        nameTxt.text = itemName; itemString = itemName;
        if (itemNum == -1) { numTxt.text = "*"; }
        else { numTxt.text = itemNum.ToString(); }
        costTxt.text = GetItemCost().ToString() + " g";
    }

    int GetItemCost() { return ItemManager.current.GetItemcost(itemString); }

    public void AddToTrade(bool add) { TradeMenu.current.PutToTrade(itemString,  GetItemCost(), this); isTrade = add; }
}
