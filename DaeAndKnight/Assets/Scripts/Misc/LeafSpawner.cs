using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpawner : MonoBehaviour
{
    public GameObject prefab;

    public int numberActive;
    public int maxActive;

    public float spawnChance;

    public float x;
    public float y;

    public float minZ;
    public float maxZ;


    // Update is called once per frame
    void Update()
    {
        if (Random.Range(1, spawnChance) <= 5 && numberActive < maxActive)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        numberActive++;
        Vector3 spawn = new Vector3(x, y, Random.Range(minZ, maxZ));
        Instantiate(prefab, spawn, Quaternion.identity, transform.parent);
    }
}
