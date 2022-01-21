using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!triggered)
            {
                triggered = true;
                FindObjectOfType<Player>().SetCheckpoint(transform.position);
            }
            else
            {
                print("Checkpoint already set!");
            }
        }
    }
}
