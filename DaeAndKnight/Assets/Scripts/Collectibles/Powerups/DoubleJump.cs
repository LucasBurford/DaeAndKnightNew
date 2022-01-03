using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerMovement>().hasDoubleJump = true;
            FindObjectOfType<ItemAcquired>().AcquiredItem("Double Jump", "Space x2");
            Destroy(transform.parent.gameObject);
        }
    }
}
