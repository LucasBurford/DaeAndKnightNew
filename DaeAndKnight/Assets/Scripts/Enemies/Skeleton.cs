using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
{
    public Player player;
    public Animator animator;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public NavMeshAgent agent;
    public Vector3 startingPos;

    public float health;
    public float attackDamage;
    public float attackRange;

    public bool isTouchingPlayer;
    public bool canAttack;
    public bool isAttacking;
    public bool hasTakenDamage;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(1, 150) == 1)
        {
            FindObjectOfType<AudioManager>().Play("SkeletonIdle");
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.isStopped = true;
        }

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
        if (!isDead)
        {
            canAttack = false;
            isAttacking = true;

            StartCoroutine(WaitToPlayAttackAnimation());
        }
    }

    public void TakeDamage(float damage)
    {
        if (!hasTakenDamage)
        {
            hasTakenDamage = true;
            animator.SetBool("Hit1", hasTakenDamage);
            StartCoroutine(WaitToResetHurtAnimation());
        }

        // Play skeleton damage sound
        FindObjectOfType<AudioManager>().Play("SkeletonDamageBonesRattle");
        FindObjectOfType<AudioManager>().Play("SkeletonHurt");

        health -= damage;
    }

    private void Die()
    {
        isDead = true;

        animator.Play("Fall1");

        StartCoroutine(WaitToDestroy());
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

    IEnumerator WaitToResetHurtAnimation()
    {
        yield return new WaitForSeconds(1);

        hasTakenDamage = false;
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(2);

        // Give XP
        player.GiveXP(10);
        Destroy(gameObject);
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
