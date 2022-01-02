using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text levelText;
    public TMP_Text givexpText;
    public TMP_Text goldText;

    public float health;
    public int xp;
    public int level;
    public int gold;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTextFields();
        CheckLevel();
    }

    public void TakeDamage(float damage)
    {
        FindObjectOfType<AudioManager>().Play("KnightHurt");
        health -= damage;
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

    private void UpdateTextFields()
    {
        healthText.text = health.ToString();
        levelText.text = level.ToString();
        goldText.text = gold.ToString();
    }

    IEnumerator WaitToRemoveGiveXPText()
    {
        yield return new WaitForSeconds(1.5f);

        givexpText.gameObject.SetActive(false);
    }
}
