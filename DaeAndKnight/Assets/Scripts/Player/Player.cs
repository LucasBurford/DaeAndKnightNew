using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public Animator animator;

    public TMP_Text levelText;
    public TMP_Text givexpText;
    public TMP_Text goldText;
    public TMP_Text pressToTeleportText;

    public Slider healthSlider;
    public Slider staminaSlider;

    public float health;
    public float stamina;
    public int xp;
    public int level;
    public int gold;

    public bool isBlocking;
    public bool gotHit;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        CheckValues();
        CheckLevel();

        animator.SetBool("GotHit", gotHit);
    }

    public void TakeDamage(float damage)
    {
        if (isBlocking)
        {
            FindObjectOfType<AudioManager>().Play("ShieldHit1");
            stamina -= damage;
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("KnightHurt");
            health -= damage;
        }
        gotHit = true;
        StartCoroutine(WaitToResetGotHit());
    }

    public void GiveXP(int amount)
    {
        xp += amount;
        // Display given xp text
        givexpText.gameObject.SetActive(true);
        givexpText.text = "+" + amount.ToString();
        StartCoroutine(WaitToRemoveGiveXPText());
    }

    public void GiveGold(int amount)
    {
        gold += amount;
    }

    private void CheckValues()
    {
        if (health <= 0)
        {
            Die();
        }

        if (stamina <= 0)
        {
            Stun();
        }
    }

    private void Stun()
    {

    }

    private void Die()
    {

    }

    private void CheckLevel()
    {
        if (xp >= 100)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        // Play sound

        level++;
    }

    private void UpdateUI()
    {
        healthSlider.value = health;
        staminaSlider.value = stamina;
        levelText.text = level.ToString();
        goldText.text = gold.ToString();
    }

    IEnumerator WaitToRemoveGiveXPText()
    {
        yield return new WaitForSeconds(1.5f);

        givexpText.gameObject.SetActive(false);
    }

    IEnumerator WaitToResetGotHit()
    {
        yield return new WaitForSeconds(0.8f);

        gotHit = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Teleporter"))
        {
            pressToTeleportText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Teleporter"))
        {
            pressToTeleportText.gameObject.SetActive(false);
        }
    }
}
