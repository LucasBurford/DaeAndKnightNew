using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teleporter : MonoBehaviour
{
    // Pad teleporting to
    public GameObject target;

    public TMP_Text pressToTeleportText;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject go = other.gameObject;

            pressToTeleportText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.W))
            {
                go.transform.position = target.gameObject.transform.position;
                FindObjectOfType<AudioManager>().Play("Teleport");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pressToTeleportText.gameObject.SetActive(false);
        }
    }
}
