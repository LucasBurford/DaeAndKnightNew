using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MushroomEnemy : MonoBehaviour
{
    #region Fields

    [Header("References")]
    public Animator animator;
    public Player player;
    public NavMeshAgent agent;
    public MushroomMon_Ani_Test anim;

    [Header("Gameplay and spec")]
    public float health;
    public float attackDamage;
    public float moveSpeed;

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
        animator = GetComponent<Animator>();
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
}
