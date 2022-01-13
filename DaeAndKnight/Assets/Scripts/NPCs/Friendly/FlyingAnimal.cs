using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAnimal : MonoBehaviour
{
    public Vector3 startPos;

    public float distance;
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, moveSpeed);

        distance += 0.5f;

        if (Vector3.Distance(transform.position, startPos) > distance)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = startPos;
    }
}
