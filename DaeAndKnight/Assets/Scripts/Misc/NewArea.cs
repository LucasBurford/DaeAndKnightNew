using System;
using UnityEngine;

public class NewArea : MonoBehaviour
{
    public int timesTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timesTriggered++;

            if (timesTriggered == 1)
            {
                FindObjectOfType<AudioManager>().Play("NewArea");
            }

            if (timesTriggered >= 2)
            {
                timesTriggered = 0;
            }
        }
    }
}
