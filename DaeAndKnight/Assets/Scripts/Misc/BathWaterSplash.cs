using UnityEngine;

public class BathWaterSplash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("BathWaterSplash");
        }
    }
}
