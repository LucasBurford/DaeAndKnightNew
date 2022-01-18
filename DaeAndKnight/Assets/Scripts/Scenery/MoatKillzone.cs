using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoatKillzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Player>().TakeDamage(500);
        }
    }
}
