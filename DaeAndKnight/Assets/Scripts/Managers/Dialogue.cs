using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject continueButton;
    public Animator animator;
    public TextMeshProUGUI textDisplay;

    public string[] sentences;

    private int index;

    public float typingSpeed;

    private void Start()
    {
       // StartCoroutine(Type());
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void NextSentence()
    {
        animator.SetTrigger("Change");
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            FindObjectOfType<PlayerAttack>().isInDialogue = false;
        }
    }

   public IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
