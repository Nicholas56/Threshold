using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeDropHandler : MonoBehaviour, IDropHandler
{
    public bool toTrade;
    public bool player;
    public Transform dropParent;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<TradeDragHandler>())
        {
                DisplayTrade.current.AddToList(eventData.pointerDrag.GetComponent<TradeDragHandler>().tItem,toTrade);
        }
    }

    public void SetList(bool isPlayer,bool isTrade) { player = isPlayer;toTrade = isTrade; }
}
