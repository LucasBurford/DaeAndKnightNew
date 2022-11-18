using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Player player;
    public GameObject gold;
    public bool collected;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collected = true;
            player.GiveGold(Random.Range(5, 10));
            FindObjectOfType<AudioManager>().Play("GoldCollection");
            FindObjectOfType<AudioManager>().Play("ChestOpen");
            Destroy(gold);
        }
    }
}
