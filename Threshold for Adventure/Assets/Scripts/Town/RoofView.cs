using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoofView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Material roofMaterial;
    [Range(0, 1)]
    public float transparency;
    Indoors indoors;
    private void Awake()
    {
        indoors = GetComponentInParent<Indoors>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        roofMaterial.color = new Color(roofMaterial.color.r, roofMaterial.color.g, roofMaterial.color.b, transparency);
        indoors.VisibleParts(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        roofMaterial.color = new Color(roofMaterial.color.r, roofMaterial.color.g, roofMaterial.color.b, 0.8f);
        indoors.VisibleParts(true);
    }
}
