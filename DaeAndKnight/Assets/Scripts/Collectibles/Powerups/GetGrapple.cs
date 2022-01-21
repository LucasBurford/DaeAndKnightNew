using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGrapple : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Grapple>().hasGrapple = true;
            FindObjectOfType<ItemAcquired>().AcquiredItem("Grapple", "Left click on blue and orange portals");
            Destroy(gameObject);
        }
    }
}
