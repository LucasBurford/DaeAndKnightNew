using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Player>().GiveHealthPotion();
            FindObjectOfType<AudioManager>().Play("ItemAcquired");
            Destroy(gameObject);
        }
    }
}
