﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Player>().TakeDamage(500);
        }
    }
}
