using UnityEngine.EventSystems;
using UnityEngine;

public class DragCharacterRotate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 initialMousePosition;
    public Transform characterModel;//To rotate
    bool isPointer;

    private void Update()
    {
        //If the mouse is over the object, the mouse click will begin to rotate the char model around the y-axis at a speed proportional to the distance from the click point
        if (isPointer)
        {
            if (Input.GetMouseButtonDown(0)) { initialMousePosition = Input.mousePosition; }
            if (Input.GetMouseButton(0)) { characterModel.RotateAround(characterModel.position, Vector3.up, (initialMousePosition.x - Input.mousePosition.x)*Time.deltaTime); }
        }        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //While the mouse is over this object, the dragging can occur
        isPointer = true;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        isPointer = false;
    }
}
