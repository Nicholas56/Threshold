using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TextDataController : MonoBehaviour
{
    public List<TextData> textLinks;

    public TextData Get(string key)
    {
        return textLinks.FirstOrDefault(t => t.Key == key);
    }
}

[Serializable]
public struct TextData
{
    public string Key;
    public string Description;
}
