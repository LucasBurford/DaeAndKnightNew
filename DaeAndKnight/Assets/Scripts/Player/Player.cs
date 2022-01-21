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
    public TMP_Text healthPotionsText;

    public Slider healthSlider;
    public Slider staminaSlider;

    public Vector3 checkpoint;

    public float maxHealth;
    public float currentHealth;
    public float maxStamina;
    public float currentStamina;
    public float staminaRechargeRate;
    public int healthUps;
    public int staminaUps;
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
    public bool hasDied;

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
            currentStamina += staminaRechargeRate;
        }
        if (currentStamina >= maxStamina)
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
            currentStamina -= damage;
            StartCoroutine(WaitToRechargeStamina());
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("KnightHurt");
            currentHealth -= damage;
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
        animator.SetTrigger("Recover");
        StartCoroutine(WaitToBackToIdle());
        transform.position = new Vector3(transform.position.x, checkpoint.y, checkpoint.z);
        //transform.position = new Vector3(0, 1, -5);
        currentHealth = maxHealth;
        hasDied = false;
    }

    private void UseHealthPotion()
    {
        currentHealth += healFactor;
        healthPotions--;
        FindObjectOfType<AudioManager>().Play("PlayerHeal");
    }

    public void CollectedHealthUp()
    {
        healthUps++;

        int numLeft = 3 - healthUps;

        FindObjectOfType<ItemAcquired>().AcquiredStatUp("Health Up", numLeft);

        if (healthUps == 3)
        {
            IncreaseHealth();
        }
    }

    private void IncreaseHealth()
    {
        maxHealth += 25;
        currentHealth = maxHealth;
        FindObjectOfType<ItemAcquired>().IncreaseStat("Health");
        healthSlider.maxValue += 25;
        RectTransform rt = healthSlider.GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rt.rect.size.x + 25);
        healthSlider.transform.position = new Vector3(healthSlider.transform.position.x + 13, healthSlider.transform.position.y, healthSlider.transform.position.z);
        healthUps = 0;
    }

    public void CollectedStaminaUp()
    {
        staminaUps++;

        int numLeft = 3 - staminaUps;

        FindObjectOfType<ItemAcquired>().AcquiredStatUp("Stamina Up", numLeft);

        if (staminaUps == 3)
        {
            IncreaseStamina();
        }
    }

    private void IncreaseStamina()
    {
        maxStamina += 25;
        staminaSlider.maxValue += 25;
        canRechargeStamina = true;
        FindObjectOfType<ItemAcquired>().IncreaseStat("Stamina");
        RectTransform rt = staminaSlider.GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rt.rect.size.x + 25);
        staminaSlider.transform.position = new Vector3(staminaSlider.transform.position.x + 13, staminaSlider.transform.position.y, healthSlider.transform.position.z);
        staminaUps = 0;
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (healthPotions > 0 && currentHealth != maxHealth)
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
        if (currentStamina <= 0)
        {
            isStunned = true;
        }
        else if (currentStamina >= maxStamina)
        {
            isStunned = false;
            currentStamina = maxStamina;
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
        if (!hasDied)
        {
            hasDied = true;

            if (!deathAudioPlayed)
            {
                deathAudioPlayed = true;
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
            }
            playerMovement.canMove = false;
            animator.SetBool("IsDead", true);
            StartCoroutine(WaitToRespawn());
        }
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
        staminaSlider.value = currentStamina;
        levelText.text = level.ToString();
        goldText.text = gold.ToString();
        healthPotionsText.text = healthPotions.ToString();
    }

    #region Coroutines
    IEnumerator WaitToBackToIdle()
    {
        yield return new WaitForSeconds(2);
        animator.SetTrigger("BackToIdle");
        FindObjectOfType<PlayerMovement>().canMove = true;
    }

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
