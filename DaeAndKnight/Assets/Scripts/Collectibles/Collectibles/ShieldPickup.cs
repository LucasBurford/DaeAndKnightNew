using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public PlayerAttack playerAttack;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 3, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerAttack.hasShield = true;
            FindObjectOfType<ItemAcquired>().AcquiredItem("Shield", "Right Click");
            Destroy(gameObject);
        }
    }
}
