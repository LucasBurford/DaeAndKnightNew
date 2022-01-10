using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MushroomEnemy : MonoBehaviour
{
    #region Fields

    [Header("References")]
    public Player player;
    public NavMeshAgent agent;
    public MushroomMon_Ani_Test anim;
    public Transform attackPoint;
    public LayerMask playerLayer;

    [Header("Gameplay and spec")]
    public float health;
    public float attackDamage;
    public float attackRange;

    public bool canAttack;
    public bool isDead;

    [Header("State")]
    public States state;
    public enum States
    {
        Idle,
        Chasing
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<MushroomMon_Ani_Test>();
        state = States.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        CheckDistance();

        if (health <= 0)
        {
            Die();
        }
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

    private void CheckState()
    {
        switch (state)
        {
            case States.Idle:
                {
                    anim.IdleAni();
                    agent.isStopped = true;
                }
                break;

            case States.Chasing:
                {
                    anim.RunAni();
                    agent.isStopped = false;
                    agent.SetDestination(player.transform.position);
                }
                break;
        }
    }

    private void Attack()
    {
        canAttack = false;

        anim.AttackAni();

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

    public void TakeDamage(float damage)
    {
        health -= damage;
        anim.DamageAni();
    }

    private void Die()
    {
        isDead = true;
        FindObjectOfType<AudioManager>().Play("MushroomDeath");
        player.GiveXP(10);
        anim.DeathAni();
        Destroy(gameObject);
    }

    IEnumerator WaitToResetAttack()
    {
        yield return new WaitForSeconds(2);

        canAttack = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canAttack && !isDead)
        {
            Attack();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
