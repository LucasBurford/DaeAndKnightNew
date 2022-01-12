using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeLog : MonoBehaviour
{
    public Vector3 spawnPoint;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed, 0, 0);

        if (transform.position.x >= 83.4f)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = spawnPoint;
    }
}
