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
    public Collider col;

    public float health;
    public float attackDamage;
    public float attackRange;
    public float walkSpeed;

    public bool isTouchingPlayer;
    public bool canAttack;
    public bool isAttacking;
    public bool hasTakenDamage;
    public bool isDead;
    public bool justTookDamage;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
        col = GetComponent<Collider>();
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
            walkSpeed = 1;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.isStopped = true;
            walkSpeed = 0;
        }

        animator.SetFloat("WalkSpeed", walkSpeed);
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

            animator.Play("Attack1h1");

            StartCoroutine(WaitToCastAttack());
        }
    }

    public void TakeDamage(float damage)
    {
        justTookDamage = true;

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

        StartCoroutine(WaitToResetJustTookDamage());
    }

    private void Die()
    {
        isDead = true;

        col.isTrigger = true;

        agent.isStopped = true;

        animator.Play("Fall1");

        StartCoroutine(WaitToDestroy());
    }

    IEnumerator WaitToCastAttack()
    {
        if (!isDead)
        {
            yield return new WaitForSeconds(0.7f);

            Collider[] hitObjects = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

            foreach (Collider col in hitObjects)
            {
                if (col.gameObject.CompareTag("Player"))
                {
                    col.gameObject.SendMessageUpwards("TakeDamage", attackDamage);
                }
            }

            StartCoroutine(WaitToResetAttack());
        }
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

    IEnumerator WaitToResetJustTookDamage()
    {
        yield return new WaitForSeconds(1f);
        justTookDamage = false;
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
