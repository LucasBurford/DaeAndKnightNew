using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoatBridgeLever : MonoBehaviour
{
    public Vector3 newPos;

    public GameObject bridge;

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
                bridge.transform.position = new Vector3(-1.35f, 1.98f, 104.79f);
                bridge.transform.rotation = new Quaternion(0, 0, 0, 1);
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
