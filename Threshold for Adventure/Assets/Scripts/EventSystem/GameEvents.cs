using System;
using System.Collections.Generic;
using UnityEngine;

//Tutorial used: https://www.youtube.com/watch?v=gx0Lt4tCDE0
public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onSave;
    public void SaveGame()
    {
        onSave();
    }

    public event Action onZoomIn;
    public event Action onZoomOut;
    public void ZoomIn() { onZoomIn(); }
    public void ZoomOut() { onZoomOut(); }

    public event Action onCallBarter;
    public void CallBarter()
    {
        if (onCallBarter != null) { onCallBarter(); }
    }
    public event Action onDisplayMenu;
    public void DisplayMenu()
    {
        if (onDisplayMenu != null) { onDisplayMenu(); }
    }
    public event Action<int> onBeginQuest;
    public void BeginQuest(int id)
    {
        if (onBeginQuest != null) { onBeginQuest(id); }
    }
    public event Action<int> onEndQuest;
    public void EndQuest(int id)
    {
        if (onEndQuest != null) { onEndQuest(id); }
    }
    public event Action<string> onLogEntry;
    public void LogEntry(string entry)
    {
        if (onLogEntry != null) { onLogEntry(entry); }
    }

    public event Action<int> onDoorwayTriggerEnter;
    public void DoorwayTriggerEnter(int id)
    {
        if (onDoorwayTriggerEnter != null)
        {
            onDoorwayTriggerEnter(id);
        }
    }

    public event Action<int> onDoorwayTriggerExit;
    public void DoorwayTriggerExit(int id)
    {
        if (onDoorwayTriggerExit != null)
        {
            onDoorwayTriggerExit(id);
        }
    }
    public event Action onCurrentAction;
    public void CurrentAction()
    {
        if (onCurrentAction != null)
        {
            onCurrentAction();
        }
    }
    public void ResetCurrentActionSubscriptions() => onCurrentAction = delegate { };

}
