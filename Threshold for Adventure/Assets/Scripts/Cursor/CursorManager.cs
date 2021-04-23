using System;
using System.Collections.Generic;
using UnityEngine;
//Tutorial used: https://www.youtube.com/watch?v=8Fm37H1Mwxw
public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    public List<CursorAnimation> cursorAnimationList;

    CursorAnimation cursorAnimation;

    int currentFrame;
    float frameTimer;
    int frameCount;

    public enum CursorType { Arrow, Grab, Select, Attack, Move}

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SetActiveCursorType(CursorType.Arrow);
    }
    private void Update()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0f)
        {
            frameTimer += cursorAnimation.frameRate;
            currentFrame = (currentFrame + 1) % frameCount;
            Cursor.SetCursor(cursorAnimation.textureArray[currentFrame], cursorAnimation.offset, CursorMode.Auto);
        }
    }

    public void SetActiveCursorType(CursorType cursorType)
    {
        SetActiveCursorAnimation(GetCursorAnimation(cursorType));
    }

    CursorAnimation GetCursorAnimation(CursorType cursorType)
    {
        foreach (CursorAnimation cursorAnimation in cursorAnimationList)
        {
            if(cursorAnimation.cursorType == cursorType) { return cursorAnimation; }
        }
        //Couldn't find this cursor type
        return null;
    }
    void SetActiveCursorAnimation(CursorAnimation cursorAnimation)
    {
        this.cursorAnimation = cursorAnimation;
        currentFrame = 0;
        frameTimer = cursorAnimation.frameRate;
        frameCount = cursorAnimation.textureArray.Length;
    }
}
