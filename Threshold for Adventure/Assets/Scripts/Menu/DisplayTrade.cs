using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTrade : MonoBehaviour
{
    public static DisplayTrade current;

    public TMP_Text playerNameTxt;
    public TMP_Text npcNameTxt;
    public TMP_Text costGainText;
    public TMP_Text moneyText;
    int tradeValue = 0;

    public Transform playerItemArea;
    public List<Transform> playerItemButtons;
    public Transform npcItemArea;
    List<Transform> npcItemButtons;
    public Transform playerTradeArea;
    public List<Transform> playerTradeButtons;
    public Transform npcTradeArea;
    List<Transform> npcTradeButtons;
    public List<TradeItem> playerItemList;
    List<TradeItem> npcItemList;

    public int carryItem;

    public GameObject buttonPrefab;

    public enum ItemQuantity { x1, x5, x10, All }
    ItemQuantity quantity = ItemQuantity.x1;

    private void Awake()
    {
        current = this;
        playerNameTxt.text = SaveData.current.profile.playerName;
        playerItemList = new List<TradeItem>();        npcItemList = new List<TradeItem>();
        playerItemButtons = new List<Transform>();        npcItemButtons = new List<Transform>();
        playerTradeButtons = new List<Transform>();        npcTradeButtons = new List<Transform>();
        GetButtons();     ResetButtons();      RefreshButtons();      AdjustCost();     HideScreen();
        GameEvents.current.onCallBarter += ShowScreen;
    }
    private void OnDestroy()
    {
        GameEvents.current.onCallBarter -= ShowScreen;
    }


    public void AddToList(TradeItem item,bool trade)
    {
        if (trade) { item.tradeNum += carryItem; }
        else { item.itemNum += carryItem; }
    }
    public void RemoveFromList(TradeItem item,bool trade)
    {//Reduces the item amounts and determines how much is being carried
        if (trade)
        {
            if (GetQuantity() > item.tradeNum) { carryItem = item.tradeNum; item.tradeNum = 0; } else { carryItem = GetQuantity(); item.tradeNum = item.tradeNum - GetQuantity(); }
        }
        else
        {
            if (GetQuantity() > item.itemNum) { carryItem = item.itemNum; item.itemNum = 0; } else { carryItem = GetQuantity(); item.itemNum = item.itemNum - GetQuantity(); }
        }
        DisplayUpdate();
    }
    public void DisplayUpdate() { RefreshButtons();AdjustCost(); }
    public int GetQuantity()
    {
        switch (quantity)
        {
            case ItemQuantity.x1:return 1;
            case ItemQuantity.x5:return 5;
            case ItemQuantity.x10:return 10;
            case ItemQuantity.All:return 100;
        }
        return 0;
    }
    public void ChangeQuantity(int value) { quantity = (ItemQuantity)value; }
    public void ShowScreen()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        npcNameTxt.text = GlobalDialogue.Instance.GetNPC().NPCName;
        moneyText.text = "Money: " + SaveData.current.profile.money.ToString("n0") + " g";
        SetupItems();       RefreshButtons();
    }
    public void HideScreen() 
    {
        ReturnItems();
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void MakeTrade()
    {
        if (SaveData.current.profile.money - tradeValue >= 0)
        {
            SaveData.current.profile.exp += Mathf.FloorToInt(tradeValue / 10);
            SaveData.current.profile.money -= tradeValue;
            if (tradeValue < 0) { GameEvents.current.LogEntry("You gained <color=#fbc531>" + Mathf.Abs(tradeValue).ToString("n0") + "g</color> in trade."); }
            else { GameEvents.current.LogEntry("You spent <color=#fbc531>" + tradeValue.ToString("n0") + "g</color> in trade."); }
            //Checks all items in trade, reduce the trade number and add to opposing inventory (mark as belonging to player or not)
            foreach (TradeItem item in npcItemList)
            {
                if (CheckForItem(item, playerItemList) != null)
                {
                    CheckForItem(item, playerItemList).itemNum += item.tradeNum;
                    ItemManager.current.IncreaseItem(item.itemName, item.tradeNum);
                }
                else { SaveData.current.profile.AddItem(item.itemName, item.tradeNum); }
                //Reduce npc item num
                GlobalDialogue.Instance.GetNPC().ChangeItemNum(item.itemName, -item.tradeNum);
                item.tradeNum = 0;
            }
            foreach (TradeItem item in playerItemList)
            {
                if (CheckForItem(item, npcItemList) != null) 
                {
                    CheckForItem(item, npcItemList).itemNum += item.tradeNum;
                    GlobalDialogue.Instance.GetNPC().ChangeItemNum(item.itemName,item.tradeNum); 
                }
                else { GlobalDialogue.Instance.GetNPC().AddItemToNpc(item.itemName,item.tradeNum); }
                //Reduce traders amount of item
                ItemManager.current.DecreaseItem(item.itemName, item.tradeNum);
                item.tradeNum = 0;
            }
            SetupItems();    RefreshButtons();
            moneyText.text = "Money: " + SaveData.current.profile.money.ToString("n0") + " g";
        }
        else
        {
            Debug.Log("Not enough money");
            GameEvents.current.LogEntry("You do not have enough money for this transaction.");
        }
    }
    TradeItem CheckForItem(TradeItem item, List<TradeItem> list) 
    {//Returns the item if it exists in the opposing inventory        
        foreach(TradeItem tItem in list) { if (tItem.itemName == item.itemName) { return tItem; } }      
        return null;
    }
    int Resale(int value)
    {
        float resaleMod = 0.5f;
        if (SaveData.current.profile.GetSkill("Barter")!=null)
        {
            resaleMod += (SaveData.current.profile.GetSkill("Barter").GetLevel()) / 10;
        }
        return Mathf.FloorToInt(resaleMod * value);
    }
    void ReturnItems()
    {
        foreach(TradeItem item in playerItemList)
        {
            item.itemNum += item.tradeNum;
            item.tradeNum = 0;
        }
        foreach (TradeItem item in npcItemList)
        {
            item.itemNum += item.tradeNum;
            item.tradeNum = 0;
        }
    }
    void CreateNewButton(Transform area, TradeItem tItem)
    {
        GameObject button = Instantiate(buttonPrefab);
        button.transform.SetParent(area);
        button.transform.localScale = new Vector3(1, 1, 1);
        button.GetComponent<TradeDragHandler>().SetItem(tItem);
        if(area==playerItemArea) { playerItemButtons.Add(button.transform); }
        if(area==npcItemArea) { npcItemButtons.Add(button.transform); }
        if(area==playerTradeArea) { playerTradeButtons.Add(button.transform); }
        if(area==npcTradeArea) { npcTradeButtons.Add(button.transform); }
    }
    void AdjustCost()
    {
        if (tradeValue >= 0) { costGainText.text = "<color=#E84118>Cost: " + tradeValue.ToString("n0") + " g</color>"; }
        else { costGainText.text = "<color=#4CD137>Gain: " + Mathf.Abs(tradeValue).ToString("n0") + " g</color>"; }
    }
    void RefreshButtons()
    {
        tradeValue = 0;
        ResetButtons();

        //Checks player and npc items, tells screen which are for trade or not
        for (int i = 0; i < playerItemList.Count; i++)
        {
            if (playerItemList[i].itemNum > 0) 
            {
                playerItemButtons[i].gameObject.SetActive(true);
                playerItemButtons[i].GetComponent<TradeDragHandler>().SetItem(playerItemList[i]);
            }
            if (playerItemList[i].tradeNum > 0)
            {
                playerTradeButtons[i].gameObject.SetActive(true);
                playerTradeButtons[i].GetComponent<TradeDragHandler>().SetItem(playerItemList[i]);
                tradeValue -= Resale((playerItemList[i].tradeNum) * (ItemManager.current.GetItemcost(playerItemList[i].itemName)));
            }            
        }
        for (int i = 0; i < npcItemList.Count; i++)
        {
            if (npcItemList[i].itemNum > 0)
            {
                npcItemButtons[i].gameObject.SetActive(true);
                npcItemButtons[i].GetComponent<TradeDragHandler>().SetItem(npcItemList[i]);
            }
            if (npcItemList[i].tradeNum > 0)
            {
                npcTradeButtons[i].gameObject.SetActive(true);
                npcTradeButtons[i].GetComponent<TradeDragHandler>().SetItem(npcItemList[i]);
                tradeValue += (npcItemList[i].tradeNum) * (ItemManager.current.GetItemcost(npcItemList[i].itemName));
            }
        }
    }
    void SetupItems()
    {
        playerItemList.Clear();     npcItemList.Clear();
        for (int i = 0; i < SaveData.current.profile.inventory.Count; i++)
        {
            if (SaveData.current.profile.itemNums[i] < 0) { continue; }
            TradeItem tItem = new TradeItem(SaveData.current.profile.inventory[i], SaveData.current.profile.itemNums[i],true,false);
            playerItemList.Add(tItem);

        }
        for (int j = 0; j < GlobalDialogue.Instance.GetNPC().npcItems.Count; j++)
        {
            TradeItem tItem = new TradeItem(GlobalDialogue.Instance.GetNPC().npcItems[j], GlobalDialogue.Instance.GetNPC().itemNums[j],false,false);
            npcItemList.Add(tItem);
        }
        playerItemArea.parent.parent.GetComponent<TradeDropHandler>().SetList(true,false);
        npcItemArea.parent.parent.GetComponent<TradeDropHandler>().SetList(false,false);
        playerTradeArea.parent.parent.GetComponent<TradeDropHandler>().SetList(true,true);
        npcTradeArea.parent.parent.GetComponent<TradeDropHandler>().SetList(false,true);
    }
    void ResetButtons()
    {
        foreach (Transform button in playerItemArea) { button.gameObject.SetActive(false); }
        foreach (Transform button in npcItemArea) { button.gameObject.SetActive(false); }
        foreach (Transform button in playerTradeArea) { button.gameObject.SetActive(false); }
        foreach (Transform button in npcTradeArea) { button.gameObject.SetActive(false); }
    }
    void GetButtons()
    {
        playerItemButtons.Clear();npcItemButtons.Clear();playerTradeButtons.Clear();npcTradeButtons.Clear();

        foreach (Transform button in playerItemArea) { playerItemButtons.Add(button); }
        foreach (Transform button in npcItemArea) { npcItemButtons.Add(button); }
        foreach (Transform button in playerTradeArea) { playerTradeButtons.Add(button); }
        foreach (Transform button in npcTradeArea) { npcTradeButtons.Add(button); }
    }        
}
[System.Serializable]
public class TradeItem
{
    public string itemName;
    public int itemNum;
    public int tradeNum;
    public bool isPlayer;
    public bool isTrade;

    public TradeItem(string iName, int iNum,bool player, bool trade)
    {
        itemName = iName;
        itemNum = iNum;
        tradeNum = 0;
        isPlayer = player;
        isTrade = trade;
    }
}
