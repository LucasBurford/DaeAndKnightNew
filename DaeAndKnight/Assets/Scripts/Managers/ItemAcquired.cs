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

    public void AcquiredStatUp(string itemName, int numLeft)
    {
        itemAcquiredText.gameObject.SetActive(true);

        if (itemName == "Damage Up")
        {
            itemAcquiredText.text = "Damage Up acquired! Collect " + numLeft + " to increase attack damage!"; 
        }
        else if (itemName == "Stamina Up")
        {
            itemAcquiredText.text = "Stamina Up acquired! Collect " + numLeft + " more to increase stamina!";
        }
        else if (itemName == "Health Up")
        {
            itemAcquiredText.text = "Health Up acquired! Collect " + numLeft + " more to increase health!";
        }

        StartCoroutine(WaitToRemoveText());
        PlaySound();
    }

    public void IncreaseStat(string statName)
    {
        if (statName == "Damage")
        {
            itemAcquiredText.text = "Attack damage increased!";
        }
        else if (statName == "Stamina")
        {
            itemAcquiredText.text = "Stamina increased!";
        }
        else if (statName == "Health")
        {
            itemAcquiredText.text = "Health increased!";
        }

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
