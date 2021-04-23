using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomControl : MonoBehaviour
{
    public void ZoomIn() { GameEvents.current.ZoomIn(); }
    public void ZoomOut() { GameEvents.current.ZoomOut(); }
}
