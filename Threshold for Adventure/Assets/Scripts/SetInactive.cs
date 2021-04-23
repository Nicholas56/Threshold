using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactive : MonoBehaviour
{
    public List<GameObject> objsToDisable;
    void Start()
    {
        Inactive();
    }

    void Inactive()
    {
        for (int i = 0; i < objsToDisable.Count; i++)
        {
            objsToDisable[i].SetActive(false);
        }
    }
}
