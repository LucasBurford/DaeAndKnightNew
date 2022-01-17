using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    #region Fields
    [Header("References")]
    public Player player;
    public NavMeshAgent agent;
    public Animator animator;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public Collider col;

    [Header("Gameplay and spec")]
    public float health;
    public float attackDamage;
    public float attackRange;
    public float moveSpeed;

    public bool isTouchingPlayer;
    public bool canAttack;
    public bool isDead;
    public bool deathAudioPlayed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider>();
        agent.speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();

        if (isTouchingPlayer)
        {
            if (canAttack)
            {
                Attack();
            }
        }

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 10)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            animator.SetFloat("MoveSpeed", agent.speed);
        }
        else if (distance > 10)
        {
            agent.isStopped = true;
            animator.SetFloat("MoveSpeed", 0);
        }
        else if (distance <= 1)
        {
            animator.SetFloat("MoveSpeed", 0);
        }
    }

    private void Attack()
    {
        animator.SetBool("Attack", true);

        canAttack = false;

        StartCoroutine(WaitToCastAttack());
        StartCoroutine(WaitToResetAttackAnimation());
        StartCoroutine(WaitToResetAttack());
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        FindObjectOfType<AudioManager>().Play("GoblinHurt");
        animator.SetBool("TookDamage", true);
        col.isTrigger = true;
        StartCoroutine(WaitToResetHurtAnimation());
    }

    private void Die()
    {
        agent.isStopped = true;
        isDead = true;
        animator.SetTrigger("Dead");
        if (!deathAudioPlayed)
        {
            deathAudioPlayed = true;
            FindObjectOfType<AudioManager>().Play("GoblinDeath");
        }
        StartCoroutine(WaitToDie());
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
    }

    IEnumerator WaitToCastAttack()
    {
        yield return new WaitForSeconds(0.8f);

        // Attack logic
        Collider[] hitObjects = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (Collider col in hitObjects)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.SendMessageUpwards("TakeDamage", attackDamage);
            }
        }


    }

    IEnumerator WaitToResetAttack()
    {
        yield return new WaitForSeconds(3);
        canAttack = true;
    }

    IEnumerator WaitToResetAttackAnimation()
    {
        yield return new WaitForSeconds(3);
        animator.SetBool("Attack", false);
    }

    IEnumerator WaitToResetHurtAnimation()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("TookDamage", false);
    }

    IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
