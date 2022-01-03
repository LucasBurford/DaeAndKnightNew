using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Fields

    #region References
    [Header("References")]
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    #endregion

    #region Gameplay and spec
    [Header("Gameplay and soec")]
    public int criticalChance;
    public float attackDamage;
    public float attackRange;

    public bool canAttack;
    public bool isAttacking;
    bool justHadCritical;
    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsAttacking", isAttacking);

        GetInput();   
    }

    private void GetInput()
    {
        // If player clicks left mouse button
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            isAttacking = true;
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
            foreach(Collider col in hitObjects)
            {
                // If we hit a gameobject with Enemy tag, SendMessage to take damage
                if (col.gameObject.CompareTag("Enemy"))
                {
                    col.gameObject.SendMessageUpwards("TakeDamage", attackDamage);
                }
                print(col.gameObject.name);
            }

            canAttack = false;

            // Prevent attack spam
            StartCoroutine(WaitToResetAttack());
        }
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

    #region Coroutines
    IEnumerator WaitToResetAttack()
    {
        yield return new WaitForSeconds(1);

        if (justHadCritical)
        {
            attackDamage /= 2;
            justHadCritical = false;
        }

        canAttack = true;
    }

    IEnumerator WaitToResetAttackAnimation()
    {
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;
    }
    #endregion

    #region Misc
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    #endregion
}
