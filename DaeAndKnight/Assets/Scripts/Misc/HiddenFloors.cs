using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenFloors : MonoBehaviour
{
    public GameObject hiddenFloors;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hiddenFloors.SetActive(true);
        }
    }
}
