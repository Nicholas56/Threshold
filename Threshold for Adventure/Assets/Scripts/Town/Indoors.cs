using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indoors : MonoBehaviour
{
    public List<GameObject> houseParts;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { VisibleParts(false); Debug.Log("Player enters"); }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        { VisibleParts(true); }
    }

    public void VisibleParts(bool visible)
    {
        foreach(GameObject part in houseParts)
        {
            part.SetActive(visible);
        }
    }
}
