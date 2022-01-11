using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int rand = Random.Range(1, 1000);
            if (rand == 1)
            {
                PlaySound("Owl1");
            }
            else if (rand == 100)
            {
                PlaySound("Owl2");
            }
        }
    }

    private void PlaySound(string name)
    {
        FindObjectOfType<AudioManager>().Play(name);
    }
}
