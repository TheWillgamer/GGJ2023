using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    public float dashLength;
    public float cd;
    public Transform playerBullet;

    private float timer;
    private bool canMove;
    private Vector3 dashDir;
    private RhythmManager rm;

    void Start()
    {
        canMove = true;
        rm = GameObject.FindWithTag("UI").GetComponent<RhythmManager>();
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
        if (Input.GetKeyDown(KeyCode.Space) && (verMov != 0 || horMov != 0) && timer > cd)
        {
            if(rm.leftNote != null && rm.rightNote != null)
            {
                Debug.Log(rm.leftNote.localPosition.x);
                Debug.Log(rm.rightNote.localPosition.x);
                Destroy(rm.leftNote.gameObject);
                Destroy(rm.rightNote.gameObject);
            }
            dashDir = new Vector3(horMov, 0, verMov).normalized;
            canMove = false;
            Invoke("StopDash", dashLength);
            timer = 0f;
        }
        if (Input.GetMouseButtonDown(0) && timer > cd)
        {
            if (rm.leftNote != null && rm.rightNote != null)
            {
                Debug.Log(rm.leftNote.localPosition.x);
                Debug.Log(rm.rightNote.localPosition.x);
                Destroy(rm.leftNote.gameObject);
                Destroy(rm.rightNote.gameObject);
            }
            Instantiate(playerBullet, transform.position, transform.rotation);
            timer = 0f;
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

        timer += Time.deltaTime;
    }

    void StopDash()
    {
        canMove = true;
    }
}
