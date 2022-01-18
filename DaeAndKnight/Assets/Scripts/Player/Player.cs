﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public Animator animator;

    public TMP_Text levelText;
    public TMP_Text givexpText;
    public TMP_Text goldText;
    public TMP_Text pressToTeleportText;
    public TMP_Text healthPotionsText;

    public Slider healthSlider;
    public Slider staminaSlider;

    public Vector3 checkpoint;

    public float maxHealth;
    public float currentHealth;
    public float stamina;
    public float staminaRechargeRate;
    public int xp;
    public int level;
    public int gold;

    public int healthPotions;
    public float healFactor;

    public bool isBlocking;
    public bool gotHit;
    public bool isStunned;
    public bool canRechargeStamina;
    public bool deathAudioPlayed;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        CheckValues();
        CheckLevel();
        SetAnimatorValues();
        GetInput();

        if (canRechargeStamina)
        {
            stamina += staminaRechargeRate;
        }
        if (stamina >= 100)
        {
            canRechargeStamina = false;
        }

        if (isBlocking)
        {
            canRechargeStamina = false;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isBlocking)
        {
            FindObjectOfType<AudioManager>().Play("ShieldHit1");
            stamina -= damage;
            StartCoroutine(WaitToRechargeStamina());
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("KnightHurt");
            currentHealth -= damage / 2; // For some reason TakeDamage is being called twice and I can't figure out why
                                  // So for time being, divide incoming damage by 2 to keep normal damage figures
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

    public void GiveHealthPotion()
    {
        healthPotions++;
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        print("Checkpoint set at " + newCheckpoint);
        checkpoint = newCheckpoint;
    }

    public void Respawn()
    {
        transform.position = new Vector3(0, 1, -5);
        currentHealth = maxHealth;
    }

    private void UseHealthPotion()
    {
        currentHealth += healFactor;
        healthPotions--;
        FindObjectOfType<AudioManager>().Play("PlayerHeal");
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (healthPotions > 0)
            {
                UseHealthPotion();
            }
            else
            {
                print("No more health potions!");
            }
        }
    }

    private void CheckValues()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        #region Stun
        if (stamina <= 0)
        {
            isStunned = true;
        }
        else if (stamina >= 100)
        {
            isStunned = false;
            stamina = 100;
        }

        if (isStunned)
        {
            playerMovement.canMove = false;

            canRechargeStamina = true;
        }
        else
        {
            playerMovement.canMove = true;
        }
        #endregion
    }

    private void SetAnimatorValues()
    {
        animator.SetBool("GotHit", gotHit);
        animator.SetBool("IsStunned", isStunned);
    }

    private void Die()
    {
        if (!deathAudioPlayed)
        {
            deathAudioPlayed = true;
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
        }
        animator.SetBool("IsDead", true);
        StartCoroutine(WaitToRespawn());
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
        healthSlider.value = currentHealth;
        staminaSlider.value = stamina;
        levelText.text = level.ToString();
        goldText.text = gold.ToString();
        healthPotionsText.text = healthPotions.ToString();
    }

    #region Coroutines

    IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(2);
        deathAudioPlayed = false;
        animator.SetBool("IsDead", false);
        Respawn();
    }

    IEnumerator WaitToRechargeStamina()
    {
        yield return new WaitForSeconds(3);

        canRechargeStamina = true;
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
    #endregion

    #region Misc Methods
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
    #endregion
}
