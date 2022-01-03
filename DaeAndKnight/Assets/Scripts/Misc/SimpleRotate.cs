using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public bool x;
    public bool y;
    public bool z;

    // Update is called once per frame
    void Update()
    {
        if (x)
        {
            transform.Rotate(3, 0, 0);
        }
        if (y)
        {
            transform.Rotate(0, 3, 0);
        }
        if (z)
        {
            transform.Rotate(0, 0, 3);
        }
    }
}
