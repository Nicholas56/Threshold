using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CursorAnimation")]
public class CursorAnimation : ScriptableObject
{
    public CursorManager.CursorType cursorType;
    public Texture2D[] textureArray;
    public float frameRate;
    public Vector2 offset;
}
