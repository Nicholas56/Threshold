using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    public int itemLimit;
    public Transform dropParent;
    public bool radialSlot;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            if (transform.childCount < itemLimit)
            {//The drop limit will prevent over population
                eventData.pointerDrag.transform.SetParent(dropParent);
                if (radialSlot) eventData.pointerDrag.GetComponent<ItemButton>().PutInRadialSlot();
                else eventData.pointerDrag.GetComponent<ItemButton>().RemoveFromRadial();
            }
        }
    }
}
