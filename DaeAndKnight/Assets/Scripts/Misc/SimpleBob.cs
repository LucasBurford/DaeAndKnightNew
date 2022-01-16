using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBob : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float y = Mathf.PingPong(Time.time, 1);

        transform.Translate(0, y, 0);
    }
}

