using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DungeonGrate : MonoBehaviour
{
    public GameObject dungeonGrate;

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
                FindObjectOfType<AudioManager>().Play("MoatBridgeClose");
                dungeonGrate.transform.position = new Vector3(-5, 0.42f, 61.5f);
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
