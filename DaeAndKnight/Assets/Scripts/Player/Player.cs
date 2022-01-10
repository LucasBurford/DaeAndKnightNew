using System.Collections;
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

    public Slider healthSlider;
    public Slider staminaSlider;

    public float health;
    public float stamina;
    public float staminaRechargeRate;
    public int xp;
    public int level;
    public int gold;

    public bool isBlocking;
    public bool gotHit;
    public bool isStunned;
    public bool canRechargeStamina;

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
        SetAnimatorValues();

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
            health -= damage / 2; // For some reason TakeDamage is being called twice and I can't figure out why
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

    private void CheckValues()
    {
        if (health <= 0)
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
        animator.Play("Die");
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

    #region Coroutines

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
