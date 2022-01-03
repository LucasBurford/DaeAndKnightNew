using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceManager : MonoBehaviour
{
    public float randomPlayChance;

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(1, randomPlayChance) == 1)
        {
            FindObjectOfType<AudioManager>().Play("RandomBackgroundMusic1");
        }
    }
}
