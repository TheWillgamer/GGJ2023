using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemAI2 : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed;
    public GameObject projectile;
    public float health;
    public bool alreadyAttacked;
    public PlayerMovement Player;
    public float timeBetweenAttacks;
    public Transform player;
    public float sightRange, attackRange;
    public LayerMask whatIsPlayer;
    public bool playerInSightRange, playerInAttack;
    private float timer;
    public float cd;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = FindObjectOfType<PlayerMovement>();
        player = GameObject.Find("Player").transform;
        timer = 0f;
        cd = 0;
        alreadyAttacked = false;

    }

    private void FixedUpdate()
    {
        rb.velocity = (transform.forward * moveSpeed);
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        transform.LookAt(Player.transform.position);
        
        if (timer > cd)
        {
            AttackPlayer();
        }

        timer += Time.deltaTime;
    }
    private void AttackPlayer()
    {
        transform.LookAt(player);

        if (!alreadyAttacked)
        {

            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
       
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {   
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
