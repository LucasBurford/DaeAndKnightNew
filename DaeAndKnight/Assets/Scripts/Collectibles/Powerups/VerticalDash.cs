using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerMovement>().hasVerticalDash = true;
            FindObjectOfType<ItemAcquired>().AcquiredItem("Vertical Dash", "W while mid-air");
            Destroy(gameObject);
        }
    }
}
