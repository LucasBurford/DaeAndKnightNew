using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goblin : MonoBehaviour
{
    #region Field
    [Header("References")]
    public Player player;
    public NavMeshAgent agent;
    public Animator animator;
    public Transform attackPoint;
    public LayerMask playerLayer;

    [Header("Gameplay and spec")]
    public float health;
    public float attackDamage;
    public float attackRange;

    public bool canAttack;
    public bool isTouchingPlayer;

    [Header("State")]
    public States state;
    public enum States
    {
        Idle,
        Chasing,
        Attacking
    }

    [Header("DEBUG")]
    public float waitTime;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        CheckState();
    }

    private void CheckState()
    {
        switch (state)
        {
            case States.Idle:
                {
                    agent.isStopped = true;
                    animator.SetBool("run", false);
                    animator.SetBool("idle01", true);
                }
                break;

            case States.Chasing:
                {
                    if (!isTouchingPlayer)
                    {
                        animator.SetBool("idle01", false);
                        animator.SetBool("run", true);
                        agent.isStopped = false;
                        agent.SetDestination(player.transform.position);
                    }

                    if (isTouchingPlayer)
                    {
                        animator.SetBool("run", false);
                        if (canAttack)
                        {
                            StartCoroutine(WaitToAttack());
                            canAttack = false;
                        }
                    }
                }
                break;
        }
    }

    private void Attack()
    {
        animator.SetBool("idle01", false);
        animator.SetBool("attack01", true);

        Collider[] hitObjects = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach(Collider col in hitObjects)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                GameObject go = col.gameObject;
                go.SendMessageUpwards("TakeDamage", attackDamage);
                StartCoroutine(WaitToResetAttack());
            }
        }

        StartCoroutine(WaitToResetAttackAnimation());
    }

    private void CheckDistance()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 5)
        {
            state = States.Chasing;
        }
        else
        {
            state = States.Idle;
        }
    }

    IEnumerator WaitToAttack()
    {
        yield return new WaitForSeconds(waitTime);
        Attack();
    }

    IEnumerator WaitToResetAttack()
    {
        yield return new WaitForSeconds(1);
        canAttack = true;
    }

    IEnumerator WaitToResetAttackAnimation()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("attack01", false);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
