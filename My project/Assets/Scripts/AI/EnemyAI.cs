using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find player GameObject by tag
    }

    private void Update()
    {
        if (player != null)
        {
            // Set the destination of the NavMeshAgent to the player's position
            agent.SetDestination(player.position);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Destroy enemy or deactivate it
        Destroy(gameObject);
    }
}
