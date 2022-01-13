using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLeaf : MonoBehaviour
{
    public float fallSpeed;
    public float rotateSpeed;
    public float distanceTravelled;
    public float maxDistance;

    // Update is called once per frame
    void Update()
    {
        FallDown();

        if (distanceTravelled >= maxDistance)
        {
            FindObjectOfType<LeafSpawner>().numberActive--;
            Destroy(gameObject);
        }
    }

    private void FallDown()
    {
        transform.Translate(0, -fallSpeed, 0);
        transform.Rotate(0, rotateSpeed, 0);

        distanceTravelled += 0.1f;
    }
}
