using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    Camera playerCamera;
    Vector3 offSet = new Vector3(3, 11, -7);
    Vector3 unit;
    Vector2 limits = new Vector2(8, 30);
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onZoomIn += ZoomIn;
        GameEvents.current.onZoomOut += ZoomOut;
        playerCamera = Camera.main;
        unit = offSet.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCamera) { playerCamera.transform.position = transform.position + offSet; }
    }

    public void ZoomIn()
    {
        if (offSet.magnitude > limits.x)
            offSet -= unit;
    }
    public void ZoomOut()
    {
        if (offSet.magnitude < limits.y)
            offSet += unit;
    }
    private void OnDestroy()
    {
        GameEvents.current.onZoomIn -= ZoomIn;
        GameEvents.current.onZoomOut -= ZoomOut;
    }
}
