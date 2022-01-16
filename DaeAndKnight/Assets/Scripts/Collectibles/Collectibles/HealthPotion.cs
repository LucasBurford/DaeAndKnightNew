using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public Player player;

    public float respawnTime;

    public bool collected;
    public bool firstTimeCollected;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !collected)
        {
            if (!firstTimeCollected)
            {
                FindObjectOfType<ItemAcquired>().AcquiredItem("Health Potion", "F");
                firstTimeCollected = true;
            }
            player.GiveHealthPotion();
            FindObjectOfType<AudioManager>().Play("ItemAcquired");
            collected = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(WaitToRespawn());
        }
    }

    IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        collected = false;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
