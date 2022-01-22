using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPrefab : MonoBehaviour
{
    public GameObject hitEffect;

    public Rigidbody2D rb;

    public float damage;
    public float speed;

    private void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WorldCollider"))
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }
        else
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.SendMessageUpwards("TakeDamage", damage);
            }

            //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            //Destroy(effect, 0.2f);
            print(collision.gameObject.name);
            FindObjectOfType<AudioManager>().Play("FireballExplosion");
            Destroy(gameObject);
        }
    }
}
