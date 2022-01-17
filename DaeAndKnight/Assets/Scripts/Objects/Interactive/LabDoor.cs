using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LabDoor : MonoBehaviour
{
    public GameObject door;

    public TMP_Text useText;

    public bool used;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !used)
        {
            useText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                used = true;
                FindObjectOfType<AudioManager>().Play("Lever");
                door.transform.Rotate(-90, 180, 100);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            useText.gameObject.SetActive(false);
        }
    }
}
