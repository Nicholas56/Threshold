using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TradeMenu : MonoBehaviour
{
    public static TradeMenu current;

    public TMP_Text playerNameTxt;
    public TMP_Text npcNameTxt;
    public TMP_Text costGainText;
    int tradeValue = 0;

    public Transform playerItemArea;
    public Transform npcItemArea;
    public Transform playerTradeArea;
    public Transform npcTradeArea;
    public List<TradeButton> playerItemButtons;
    public List<TradeButton> npcItemButtons;
    List<string> playerTradeList;
    List<string> npcTradeList;

    public GameObject buttonPrefab;

    public enum ItemQuantity { x1, x5, x10, All}
    ItemQuantity quantity = ItemQuantity.x1;

    private void Awake()
    {
        current = this;
        //playerItemButtons = new List<TradeButton>();
        //npcItemButtons = new List<TradeButton>();
        playerNameTxt.text = SaveData.current.profile.playerName;
        playerTradeList = new List<string>();
        npcTradeList = new List<string>();
        GetButtons();
        GameEvents.current.onCallBarter += DisplayTrade;
    }
    private void OnDestroy()
    {
        GameEvents.current.onCallBarter -= DisplayTrade;
    }

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    void GetButtons()
    {
        foreach(Transform button in playerItemArea) { button.GetComponent<TradeButton>().GetButton(); }
        foreach(Transform button in npcItemArea) { button.GetComponent<TradeButton>().GetButton(); }
        ResetButtons();
    }
    public void ChangeQuantity(int enumValue) { quantity = (ItemQuantity)enumValue; }

    public void DisplayTrade()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        npcNameTxt.text = GlobalDialogue.Instance.GetNPC().NPCName;
        tradeValue = 0;
        SetCost(tradeValue);
        
        ResetButtons();
        for (int i = 0; i < SaveData.current.profile.inventory.Count; i++)
        {
            if (playerItemButtons[i] == null)
            {//In the event there aren't enough buttons, instantiates a new one
                GameObject newButton = Instantiate(buttonPrefab, playerItemArea);
                AddToMenu(newButton.GetComponent<TradeButton>());
                newButton.GetComponent<TradeButton>().playerItem = true;
                newButton.GetComponent<TradeButton>().SetText(SaveData.current.profile.inventory[i], SaveData.current.profile.itemNums[i]);
            }
            playerItemButtons[i].gameObject.SetActive(true);
            playerItemButtons[i].SetText(SaveData.current.profile.inventory[i], SaveData.current.profile.itemNums[i]);
        }
        for (int j = 0; j < GlobalDialogue.Instance.GetNPC().npcItems.Count; j++)
        {
            npcItemButtons[j].gameObject.SetActive(true);
            npcItemButtons[j].SetText(GlobalDialogue.Instance.GetNPC().npcItems[j], GlobalDialogue.Instance.GetNPC().itemNums[j]);
        }
    }

    public void PutToTrade(string tradeItem, int cost, TradeButton button)
    {//Called when item is put up to trade in the central lists
        //Needs to check how many of item there is, how much is being moved and how much is left (instantiate?)
        int remain = ItemManager.current.GetItemsRemaining(tradeItem);
        int carry = 0;
        switch (quantity)
        {
            case ItemQuantity.x1:carry = 1; break;
            case ItemQuantity.x5:if (remain > 5) { carry = 5; } else { carry = remain; } break;
            case ItemQuantity.x10: if (remain > 10) { carry = 10; } else { carry = remain; } break;
            case ItemQuantity.All: carry = remain; break;
        }//Checks how many 
        remain -= carry;
        if (remain > 0)
        {//determine the button of items left behind
            GameObject newButton = Instantiate(buttonPrefab);
            if (button.isTrade)
            {
                newButton.GetComponent<TradeButton>().isTrade = true;
                if (button.playerItem)
                {
                    newButton.transform.SetParent(playerTradeArea);
                    newButton.GetComponent<TradeButton>().playerItem = true;
                }
                else
                {
                    newButton.transform.SetParent(npcTradeArea);
                }
                ItemManager.current.IncreaseItem(tradeItem, carry);
            }
            else
            {
                if (button.playerItem)
                {
                    newButton.transform.SetParent(playerItemArea);
                    newButton.GetComponent<TradeButton>().playerItem = true;
                }
                else
                {
                    newButton.transform.SetParent(npcItemArea);
                }
                ItemManager.current.DecreaseItem(tradeItem, carry);
            }
            newButton.GetComponent<TradeButton>().SetText(button.nameTxt.text, remain);
            newButton.transform.localScale = new Vector3(1, 1, 1);
            button.SetText(button.nameTxt.text, carry);
        }

        if (button.isTrade)
        {
            if (button.playerItem) { playerTradeList.Add(tradeItem); tradeValue -= cost*carry; }
            else { npcTradeList.Add(tradeItem); tradeValue += cost*carry; }
        }
        else
        {
            if (button.playerItem) { playerTradeList.Remove(tradeItem); tradeValue += cost*carry; }
            else { npcTradeList.Remove(tradeItem); tradeValue -= cost*carry; }
        }
        SetCost(tradeValue);
    }

    void GroupItems(string itemToGroup, Transform area)
    {
        foreach(Transform button in area)
        {//Finds if there is a button already of the item and merges them
            if (button.GetComponent<TradeButton>().nameTxt.text == itemToGroup)
            {

            }
        }
    }

    public void MakeTrade()
    {
        if (SaveData.current.profile.HasEnoughMoney(tradeValue))
        {
            //Determine buttons that are up for trade, then move them
        }
    }
    public void ExitTrade()
    {
        playerTradeList.Clear();
        npcTradeList.Clear();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void MoveButton(TradeButton button, bool returnButton)
    {
        if (returnButton)
        {
            if (button.playerItem) { button.transform.SetParent(playerItemArea); }
            else { button.transform.SetParent(npcItemArea); }
            button.isTrade = false;
        }
        else
        {
            if (button.playerItem) { button.transform.SetParent(npcItemArea); }
            else { button.transform.SetParent(playerItemArea); }
            button.isTrade = false;
        }
    }

    void SetCost(int value)
    {
        if (value >= 0) { costGainText.text = "<color=#E84118>Cost: " + value.ToString("n0") + " g</color>"; }
        else { costGainText.text = "<color=#4CD137>Gain: " + Mathf.Abs(value).ToString("n0") + " g</color>"; }
    }

    public void ResetButtons()
    {
        foreach(TradeButton button in playerItemButtons) { button.gameObject.SetActive(false); }
        foreach(TradeButton button in npcItemButtons) { button.gameObject.SetActive(false); }
    }


    public void AddToMenu(TradeButton button)
    {
        if (button.playerItem) { playerItemButtons.Add(button); }
        else { npcItemButtons.Add(button); }
    }
}
