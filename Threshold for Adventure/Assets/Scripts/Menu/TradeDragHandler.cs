using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TradeDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public TMP_Text buttonText;
    public TradeItem tItem;
    public bool isTrade;

    Canvas canvas;

    CanvasGroup canvasGroup;
    Transform prevParent;
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void SetItem(TradeItem tradeItem) { tItem = tradeItem;SetText(); }
    public void SetText()
    {
        if (isTrade)
        {
            buttonText.text = tItem.itemName + " #" + tItem.tradeNum + ' ' + ItemManager.current.GetItemcost(tItem.itemName) + 'g';
        }
        else { buttonText.text = tItem.itemName + " #" + tItem.itemNum + ' ' + ItemManager.current.GetItemcost(tItem.itemName) + 'g'; }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        prevParent = transform.parent;
        transform.SetParent(canvas.transform);
        DisplayTrade.current.RemoveFromList(tItem,isTrade);
        //Reduce item num by amount (or completely). Carry item has carry amount
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (transform.parent == canvas.transform) { transform.SetParent(prevParent); }
        DisplayTrade.current.DisplayUpdate();
    }
}
