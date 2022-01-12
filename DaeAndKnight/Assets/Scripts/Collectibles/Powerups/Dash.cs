using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerMovement>().hasDash = true;
            FindObjectOfType<PlayerMovement>().canDash = true;
            FindObjectOfType<ItemAcquired>().AcquiredItem("Dash", "L Shift");
        }
    }
}
