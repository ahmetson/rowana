using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [Header("ScriptSettings")]
    public float lookRadius = 10f;
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    public GameObject restartMenu;

    [Header("ScriptsConnect")]
    public Collider jawCollider;
    public PlayerMovement playerMovement;
    public HPbar hpBar;
    private VictoryScript victoryScript;
    private float timeRemaining = 2;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        victoryScript = FindObjectOfType<VictoryScript>();
        player = GameObject.FindWithTag("Player").transform;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < lookRadius)
        {
            agent.isStopped = false;
            agent.speed = victoryScript.enemySpeed;
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);
            animator.speed = 2.5f;

            
            if (distance <= agent.stoppingDistance)
            {
                animator.SetBool("isWalking", false);
                agent.isStopped = true;

            }
        }
        else 
        { 
            Patrol();
        }

        transform.localScale = victoryScript.enemyScale;  //Scale rex
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            
            Die();       
        }
    }   
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            playerMovement.velosity.x = Mathf.Sqrt(0);
        }
    }

    private void Die()
    {
        hpBar.HPstat -= victoryScript.enemyDamage;
        if (hpBar.HPstat == 0 )
        {
            restartMenu.SetActive(true);
            Destroy(player.gameObject);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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
