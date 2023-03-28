using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public float lookRadius = 10f;

    public Transform player;
    NavMeshAgent agent;
    public Animator animator;


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
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);
            if (distance <= agent.stoppingDistance)
            {
                animator.SetBool("isWalking", false);
                //Attack 
            }
        }
        else 
        { 
            animator.SetBool("isWalking", false);
            
        }
        
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
