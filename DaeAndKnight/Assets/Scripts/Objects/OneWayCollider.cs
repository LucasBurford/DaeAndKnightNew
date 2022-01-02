using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayCollider : MonoBehaviour
{
    public Collider col;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject go = other.gameObject;

            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y + 2.5f, go.transform.position.z);

            // Wait for a while so player can land
            StartCoroutine(WaitForPlayerToLand());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("X");
            if (Input.GetKeyDown(KeyCode.S))
            {
                col.isTrigger = true;
            }
        }
    }

    IEnumerator WaitForPlayerToLand()
    {
        yield return new WaitForSeconds(0.4f);
        col.isTrigger = false;
    }
}
