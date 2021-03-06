using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    #region Fields

    #region References
    [Header("References")]
    public Player player;
    public Animator animator;
    public TrailRenderer tr;
    public Transform attackPoint;
    public Transform magicAttackPoint;
    public LayerMask enemyLayers;
    public TMP_Text damageText;
    Camera cam;

    public GameObject fireBallPrefab;
    #endregion

    #region Gameplay and spec
    [Header("Gameplay and spec")]
    public Attacks selectedAttack;
    public enum Attacks
    {
        Melee,
        Fireball
    }

    public int criticalChance;
    public int comboCounter;
    public int damageUps;

    public float attackResetTime;
    public float attackDamage;
    public float attackRange;
    public float magicAttackForce;
    public float distanceToWorld;

    public bool canAttack;
    public bool isAttacking;
    public bool justHadCritical;

    public bool hasShield;
    public bool isblocking;
    public bool isInDialogue;

    public bool hasFireball;
    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetBool("IsAttacking", isAttacking);

        GetInput();   
    }

    private void GetInput()
    {
        #region Handle attack input
        // If player clicks left mouse button
        if (Input.GetButtonDown("Fire1") && canAttack && !isInDialogue)
        {
            if (selectedAttack == Attacks.Melee)
            {
                isAttacking = true;

                comboCounter++;

                // tr.enabled = true;

                if (comboCounter == 1)
                {
                    animator.SetBool("IsAttacking", isAttacking);
                }
                else if (comboCounter == 2)
                {
                    animator.SetBool("IsAttacking", isAttacking);
                }
                else if (comboCounter == 3)
                {
                    animator.SetBool("IsAttacking2", isAttacking);
                    attackDamage += 10;
                    tr.startWidth += 1;
                }

                PlayPlayerAttackSound(comboCounter);
                StartCoroutine(WaitToResetAttackAnimation());

                int generateCritical = Random.Range(1, criticalChance);

                if (generateCritical == criticalChance)
                {
                    attackDamage *= 2;
                    justHadCritical = true;
                    PlaySwordSwipeSound(7);
                    print("CRITICAL!");
                }
                else
                {
                    PlaySwordSwipeSound(Random.Range(1, 6));
                }

                // Cast a sphere with a centre of attackPoint and radius of attackRange only on enemyLayers
                Collider[] hitObjects = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

                // Iterate through array and cause damage
                foreach (Collider col in hitObjects)
                {
                    // If we hit a gameobject with Enemy tag, SendMessage to take damage
                    if (col.gameObject.CompareTag("Enemy"))
                    {
                        print("Hit enemy tag");
                        GameObject go = col.gameObject;
                        go.SendMessageUpwards("TakeDamage", attackDamage);

                        damageText.gameObject.SetActive(true);
                        damageText.text = attackDamage.ToString() + " damage!";
                        StartCoroutine(WaitToRemoveDamageText());
                    }

                    print(col.gameObject.name);
                }

                canAttack = false;

                // Prevent attack spam
                StartCoroutine(WaitToResetAttack());
            }

            if (selectedAttack == Attacks.Fireball && hasFireball)
            {
                FireballAttack();
            }

            #region Block
            if (Input.GetMouseButton(1) && hasShield)
            {
                isblocking = true;
            }
            else if (Input.GetMouseButtonUp(1) && hasShield)
            {
                isblocking = false;
            }

            CheckBlock();
            #endregion
        }
        #endregion
        #region Handle attack selection input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedAttack = Attacks.Melee;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedAttack = Attacks.Fireball;
        }
        #endregion

    }

    #region Magic attacks
    private void FireballAttack()
    {
        canAttack = false;
        StartCoroutine(WaitToResetAttack());

        Vector3 mousePos = Input.mousePosition;
        mousePos.x = distanceToWorld;

        GameObject fireBall = Instantiate(fireBallPrefab, mousePos, Quaternion.identity);
        Rigidbody rb = fireBall.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * magicAttackForce;

        FindObjectOfType<AudioManager>().Play("FireballAttack");
    }
    #endregion

    private void CheckBlock()
    {
        animator.SetBool("IsBlocking", isblocking);
        player.isBlocking = isblocking;
    }

    public void CollectedDamageUp()
    {
        damageUps++;

        int numLeft = 3 - damageUps;

        FindObjectOfType<ItemAcquired>().AcquiredStatUp("Damage Up", numLeft);

        if (damageUps == 3)
        {
            IncreaseAttackDamage();
        }
    }

    private void IncreaseAttackDamage()
    {
        attackDamage += 5;
        damageUps = 0;
        FindObjectOfType<ItemAcquired>().IncreaseStat("Damage");
    }

    private void PlaySwordSwipeSound(int number)
    {
        switch (number)
        {
            case 1:
                {
                    FindObjectOfType<AudioManager>().Play("SwordSwipe1");
                }
                break;

            case 2:
                {
                    FindObjectOfType<AudioManager>().Play("SwordSwipe2");
                }
                break;

            case 3:
                {
                    FindObjectOfType<AudioManager>().Play("SwordSwipe3");
                }
                break;

            case 4:
                {
                    FindObjectOfType<AudioManager>().Play("SwordSwipe4");
                }
                break;

            case 5:
                {
                    FindObjectOfType<AudioManager>().Play("SwordSwipe5");
                }
                break;

            case 6:
                {
                    FindObjectOfType<AudioManager>().Play("SwordSwipe6");
                }
                break;

            case 7:
                {
                    FindObjectOfType<AudioManager>().Play("SwordSwipeCritical");
                }
                break;
        }
    }

    private void PlayPlayerAttackSound(int number)
    {
        switch (number)
        {
            case 1:
                {
                    FindObjectOfType<AudioManager>().Play("PlayerAttack1");
                }
                break;

            case 2:
                {
                    FindObjectOfType<AudioManager>().Play("PlayerAttack2");
                }
                break;

            case 3:
                {
                    FindObjectOfType<AudioManager>().Play("PlayerAttack3");
                }
                break;
        }
    }

    #region Coroutines
    IEnumerator WaitToResetAttack()
    {
        yield return new WaitForSeconds(attackResetTime);

        canAttack = true;

        if (justHadCritical)
        {
            attackDamage /= 2;
            justHadCritical = false;
        }
    }

    IEnumerator WaitToResetAttackAnimation()
    {
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;

       // tr.enabled = false;

        if (comboCounter == 3)
        {
            animator.SetBool("IsAttacking2", isAttacking);
            attackDamage -= 10;
            comboCounter = 0;
        }
        else
        {
            animator.SetBool("IsAttacking", isAttacking);
        }
    }

    IEnumerator WaitToRemoveDamageText()
    {
        yield return new WaitForSeconds(2);
        damageText.gameObject.SetActive(false);
    }
    #endregion

    #region Misc
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    #endregion
}
