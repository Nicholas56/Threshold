using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item")]
public class ItemTemplate : ScriptableObject
{
    public List<Item> items;

    public Item FindItem(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == itemName) { return items[i]; } else continue;
        }
        return null;
    }
}

[System.Serializable]
public class Item
{
    public string itemName;
    public bool reusable;
    public float cooldown;
    public string itemDescription;
    public int cost;
    public Sprite itemSprite;

    float timeTilUse;

    public void SetCoolDown() {timeTilUse = Time.time + cooldown; }
    public string GetTimeRemaining() { if (timeTilUse - Time.time < 0) { return "Ready to use"; } else return (timeTilUse - Time.time).ToString("n0"); }
}
