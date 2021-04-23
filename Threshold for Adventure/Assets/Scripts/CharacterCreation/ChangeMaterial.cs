using UnityEngine.UI;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material matToChange;
    public Image colorChange;

    public void ChangeMatColor()
    {
        //Changes the designated material color to the color selected in the inspector
        matToChange.color = colorChange.color;
    }
}
