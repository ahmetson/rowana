using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public float lookRadius = 10f;

    public Transform player;
    NavMeshAgent agent;
    public Animator animator;

    private float timeRemaining = 2;
    

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < lookRadius)
        {
            agent.isStopped = false;
            agent.speed = 6f;
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);
            animator.speed = 2.5f;

            
            if (distance <= agent.stoppingDistance)
            {
                animator.SetBool("isWalking", false);
                agent.isStopped = true;
                //Attack 
            }
        }
        else 
        { 
            Patrol();
        }
    }

    private void Patrol()
    {
        agent.isStopped = false;
        agent.speed = 2f;
        animator.SetBool("isWalking", true);
        animator.speed = 0.5f;
        
        Timer();
    }

    private void Timer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            
        }
        else
        {           
            timeRemaining = 10;
            agent.SetDestination(player.position);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
