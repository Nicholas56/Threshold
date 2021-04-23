using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int id;
    private void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayClose;
    }
    void OnDoorwayClose(int id)
    {
        if (id == this.id)
        {

        }
    }
    void OnDoorwayOpen(int id)
    {
        if (id == this.id)
        {

        }
    }

    private void OnDestroy()
    {
        GameEvents.current.onDoorwayTriggerEnter -= OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerEnter -= OnDoorwayClose;
    }
}
