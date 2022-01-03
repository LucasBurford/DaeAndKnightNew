using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemAcquired : MonoBehaviour
{
    public TMP_Text itemAcquiredText;

    public void AcquiredItem(string itemName)
    {
        itemAcquiredText.gameObject.SetActive(true);
        itemAcquiredText.text = itemName + " acquired!";
        StartCoroutine(WaitToRemoveText());
        PlaySound();
    }

    /// <summary>
    /// Display just collected item and how to use it
    /// </summary>
    /// <param name="itemName">The item just picked up</param>
    /// <param name="usage">Input button/key only, i.e. "Right click" - NOT "Use with Right Click" etc</param>
    public void AcquiredItem(string itemName, string usage)
    {
        itemAcquiredText.gameObject.SetActive(true);
        itemAcquiredText.text = itemName + " acquired!" + " Use with " + usage;
        StartCoroutine(WaitToRemoveText());
        PlaySound();
    }

    private void PlaySound()
    {
        FindObjectOfType<AudioManager>().Play("ItemAcquired");
    }

    IEnumerator WaitToRemoveText()
    {
        yield return new WaitForSeconds(3);
        itemAcquiredText.gameObject.SetActive(false);
    }
}
