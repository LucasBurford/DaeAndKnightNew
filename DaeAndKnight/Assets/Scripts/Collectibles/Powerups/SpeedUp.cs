using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerMovement>().knightMoveSpeed += speed;
            FindObjectOfType<ItemAcquired>().AcquiredItem("Speed Up, max move speed increased!");
            Destroy(gameObject);
        }
    }
}
