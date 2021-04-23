using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageFont : MonoBehaviour
{
    TMP_Text textToAlter;
    public bool KeepColor = false;
    
    void Start()
    {
        textToAlter = gameObject.GetComponent<TMP_Text>();
        GameManager.texts.Add(this);
        GameManager.ChangeSpecificFont(textToAlter);
    }

    public TMP_Text GetFont() { return textToAlter; }
}
