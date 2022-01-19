using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Player>().CollectedStaminaUp();
            Destroy(gameObject);
        }
    }
}
