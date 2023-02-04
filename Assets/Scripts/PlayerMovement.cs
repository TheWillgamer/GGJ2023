using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    public float dashLength;
    private bool canMove;
    private Vector3 dashDir;

    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        int verMov = 0;
        int horMov = 0;

        if (Input.GetKey(KeyCode.W))
        {
            verMov++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            verMov--;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horMov--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horMov++;
        }
        if (Input.GetKeyDown(KeyCode.Space) && (verMov != 0 || horMov != 0))
        {
            dashDir = new Vector3(horMov, 0, verMov).normalized;
            canMove = false;
            Invoke("StopDash", dashLength);
        }

        // Move
        if (canMove)
        {
            Vector3 direction = new Vector3(horMov, 0, verMov).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
        else    // Dashing
        {
            transform.Translate(dashDir * dashSpeed * Time.deltaTime, Space.World);
        }
    }

    void StopDash()
    {
        canMove = true;
    }
}
