using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlicker : MonoBehaviour
{
    public Light mLight;

    public float flickerRate;

    // Update is called once per frame
    void Update()
    {
        mLight.intensity = Mathf.PingPong(Time.time, flickerRate);
    }
}
