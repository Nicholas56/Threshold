using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Tutorial used: https://www.youtube.com/watch?v=m1lBHP5lxeY
public class CreativeButtons : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler
{
    Image image;
    Button thisButton;
    RadialMenu menu;
    public Image itemInfo;

    private void Awake()
    {
        image = GetComponent<Image>();
        thisButton = GetComponent<Button>();
        menu = FindObjectOfType<RadialMenu>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(thisButton.interactable)
        menu.ButtonSelect(this);
    }

    void Start()
    {
        image.alphaHitTestMinimumThreshold = 0.1f;
    }
    private void OnEnable()
    {//This resets the sprite and checks with the menu to see if item preseent
        if (menu.GetItemSprite(this) == null) { itemInfo.GetComponent<CanvasGroup>().alpha = 0.2f;itemInfo.sprite = null; return; }
        itemInfo.sprite = menu.GetItemSprite(this);
        itemInfo.GetComponent<CanvasGroup>().alpha = 1;
    }
    public void SelectButton() { image.color = new Color(image.color.r, image.color.g, image.color.b, 1); }
    public void DimButton(bool interactive) 
    { 
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.1f);
        thisButton.interactable = interactive;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        menu.ShowItemData(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        menu.HideItemData();
    }
}
