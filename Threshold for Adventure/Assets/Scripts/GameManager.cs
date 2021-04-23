using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<TMP_FontAsset> fonts;
    int currentFont=0;
    public List<Color> colors;
    public static List<ManageFont> texts;

    static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        texts = new List<ManageFont>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame() { GameEvents.current.SaveGame(); }

    public void ChangeFonts(TMP_Dropdown fontNum)
    {
        currentFont = fontNum.value;
        //All texts added to this list will have their font changed
        for (int i = 0; i < texts.Count; i++)
        {
            texts[i].GetFont().font = fonts[fontNum.value];
        }
    }
    public static void ChangeSpecificFont(TMP_Text textToChange)
    {
        textToChange.font = instance.fonts[instance.currentFont];
    }
    public void ChangeFontColor(int fontColor)
    {
        //All texts added to this list will have their color changed
        for (int i = 0; i < texts.Count; i++)
        {
            if(!texts[i].KeepColor)
            texts[i].GetFont().color = colors[fontColor];
        }
    }
}
