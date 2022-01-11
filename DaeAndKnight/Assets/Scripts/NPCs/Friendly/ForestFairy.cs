using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestFairy : MonoBehaviour
{
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("FairyFlyby");

        if (Random.Range(0, 25) == 1)
        {
            FindObjectOfType<Player>().health += 15;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, moveSpeed);
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
}
