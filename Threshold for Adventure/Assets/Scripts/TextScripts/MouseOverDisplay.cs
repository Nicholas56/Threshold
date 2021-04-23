using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject display;

    public void OnPointerEnter(PointerEventData eventData)
    {
        display.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        display.SetActive(false);
    }
}
