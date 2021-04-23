using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager current;

    private void Awake()
    {
        current = this;
    }

    public ItemTemplate gameItems;

    public bool IsItemReusable(string itemName)
    {
        return GetItem(itemName).reusable;
    }

    public float GetItemCooldown(string itemName)
    {
        return GetItem(itemName).cooldown;
    }
    public string GetItemDescription(string itemName)
    {
        return GetItem(itemName).itemDescription;
    }
    public int GetItemcost(string itemName)
    {
        return GetItem(itemName).cost;
    }
    public void IncreaseItem(string itemName, int amount)
    {
        int itemNum = SaveData.current.profile.inventory.IndexOf(itemName);
        SaveData.current.profile.itemNums[itemNum] += amount;
        if (QuestManager.current.CheckFor(itemName) != null) { QuestManager.current.CheckFor(itemName).AddToCurrent(amount); }
    }
    public void DecreaseItem(string itemName, int amount)
    {
        int itemNum = SaveData.current.profile.inventory.IndexOf(itemName);
        SaveData.current.profile.itemNums[itemNum] -= amount;
        //If the amount drops to zero, the item is removed from the inventory
        if (SaveData.current.profile.itemNums[itemNum] <= 0) { SaveData.current.profile.RemoveItem(itemName); } 
        else { if (QuestManager.current.CheckFor(itemName) != null) { QuestManager.current.CheckFor(itemName).ReduceCurrent(amount); } }    
    }

    public int GetItemsRemaining(string itemName)
    {
        int itemNum = SaveData.current.profile.inventory.IndexOf(itemName);
        return SaveData.current.profile.itemNums[itemNum];
    }

    public Item GetItem(string itemName)
    {
        return gameItems.FindItem(itemName);
    }

    public void UseItem(string itemName)
    {
        GetItem(itemName).SetCoolDown();

        //Item effect to be added
    }


}
