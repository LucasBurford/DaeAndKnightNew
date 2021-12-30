using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask playerLayer;

    public float health;
    public float attackDamage;
    public float attackRange;

    public bool isTouchingPlayer;
    public bool canAttack;
    public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Attack1h1", isAttacking);

        if (isTouchingPlayer && canAttack)
        {
            Attack();
        }
        if (health <= 0)
        {
            Die();
        }
    }

    private void Attack()
    {
        canAttack = false;
        isAttacking = true;

        StartCoroutine(WaitToPlayAttackAnimation());
    }

    public void TakeDamage(float damage)
    {
        // Play skeleton damage sound


        health -= damage;
    }

    private void Die()
    {
        // Give XP

        Destroy(gameObject);
    }

    IEnumerator WaitToPlayAttackAnimation()
    {
        yield return new WaitForSeconds(0.8f);

        // Cast sphere
        Collider[] hitObjects = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        // Interate through objects
        foreach (Collider col in hitObjects)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                // Damage player
                col.gameObject.SendMessageUpwards("TakeDamage", attackDamage);
            }
        }

        StartCoroutine(WaitToResetAttack());
        StartCoroutine(WaitToResetAttackAnimation());
    }

    IEnumerator WaitToResetAttack()
    {
        yield return new WaitForSeconds(2);

        canAttack = true;
    }

    IEnumerator WaitToResetAttackAnimation()
    {
        yield return new WaitForSeconds(0.1f);

        isAttacking = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player_Knight")
        {
            isTouchingPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
