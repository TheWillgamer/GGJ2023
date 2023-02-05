using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemAI2 : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed;

    public PlayerMovement Player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = FindObjectOfType<PlayerMovement>();
    }
    private void FixedUpdate()
    {
        rb.velocity = (transform.forward * moveSpeed);
    }

    void Update()
    {
        transform.LookAt(Player.transform.position);

    }
}
