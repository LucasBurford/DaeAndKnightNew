using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairySpawner : MonoBehaviour
{
    public GameObject prefab;

    public float spawnChance;
    public float numberActive;
    public float maxActive;

    public float minX;
    public float maxX;

    public float y;

    public float minZ;
    public float maxZ;

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, spawnChance) == 1)
        {
            if (numberActive < maxActive)
            {
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        numberActive++;
        Vector3 spawn = new Vector3(Random.Range(minX, maxX), y, minZ);
        Instantiate(prefab, spawn, Quaternion.identity);
    }
}
