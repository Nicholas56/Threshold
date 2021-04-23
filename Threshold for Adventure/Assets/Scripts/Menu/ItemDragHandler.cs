using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Tutorial used: https://www.youtube.com/watch?v=Pc8K_DVPgVM
public class ItemDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Canvas canvas;

    CanvasGroup canvasGroup;
    Vector3 startPos;
    Transform prevParent;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        startPos = transform.localPosition;
        prevParent = transform.parent;
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = startPos;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (transform.parent == canvas.transform) { transform.SetParent(prevParent); }
    }

}
