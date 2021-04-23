using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickUpItem : MonoBehaviour, IPointerClickHandler
{
    public string item;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameEvents.current.ResetCurrentActionSubscriptions();
        GameEvents.current.onCurrentAction += PickUpAction;
    }

    void PickUpAction()
    {
        SaveData.current.profile.exp++;
        GameEvents.current.LogEntry("You picked up 1 <color=#4cd137>" + item + "</color>.");
        if (SaveData.current.profile.inventory.Contains(item))
        {
            ItemManager.current.IncreaseItem(item, 1);
        }
        else
        {
            SaveData.current.profile.AddItem(item, 1);
        }
        gameObject.SetActive(false);

        GameEvents.current.onCurrentAction -= PickUpAction;
    }
}
