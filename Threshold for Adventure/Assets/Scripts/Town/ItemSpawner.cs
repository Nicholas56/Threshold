using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> objects;
    public float respawnTime = 60;

    private void Start()
    {
        StartCoroutine("RespawnCycle");
    }

    IEnumerator RespawnCycle()
    {
        yield return new WaitForSeconds(respawnTime);
        Respawn();
        StartCoroutine("RespawnCycle");
    }
    public void Respawn()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].activeSelf == false) { objects[i].SetActive(true); return; }
        }
    }
}
