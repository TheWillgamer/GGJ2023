using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    public float dashLength;
    public float cd;

    public int meterGainPerfect;
    public int meterGainGreat;
    public int meterLossMiss;
    public TMP_Text timingText;

    private int combo;
    public TMP_Text comboText;

    public Transform playerBullet;
    public Transform bulletSpawn;

    private float timer;
    private bool canMove;
    private Vector3 dashDir;
    private RhythmManager rm;
    private GameplayManager gm;

    private int p = 0;
    private int g = 0;

    void Start()
    {
        combo = 0;
        canMove = true;
        rm = GameObject.FindWithTag("UI").GetComponent<RhythmManager>();
        gm = GameObject.FindWithTag("EventSystem").GetComponent<GameplayManager>();
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

        if (Input.GetKeyDown(KeyCode.Space) && timer > cd)
        {
            if(rm.leftNote != null && rm.rightNote != null)
            {
                gm.meterGain(meterGainGreat);
                if (rm.rightNote.localPosition.x < 15f)
                {
                    gm.meterGain(meterGainPerfect);
                    timingText.text = "Perfect";
                }
                else
                {
                    gm.meterGain(meterGainGreat);
                    timingText.text = "Great";
                }
                Destroy(rm.leftNote.gameObject);
                Destroy(rm.rightNote.gameObject);
            }
            else  // Miss
            {
                gm.meterLoss(meterLossMiss);
                timingText.text = "Miss";

                combo = 0;
                UpdateCombo();
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
                gm.meterGain(meterGainGreat);
                if (rm.rightNote.localPosition.x < 15f)
                {
                    gm.meterGain(meterGainPerfect);
                    timingText.text = "Perfect";
                    p++;
                }
                else
                {
                    gm.meterGain(meterGainGreat);
                    timingText.text = "Great";
                    g++;
                }
                Destroy(rm.leftNote.gameObject);
                Destroy(rm.rightNote.gameObject);
                Debug.Log("Perfect:" + p);
                Debug.Log("Great:" + g);
            }
            else  // Miss
            {
                gm.meterLoss(meterLossMiss);
                timingText.text = "Miss";

                combo = 0;
                UpdateCombo();
            }

            Instantiate(playerBullet, bulletSpawn.position, bulletSpawn.rotation);
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

    void UpdateCombo()
    {
        if (combo > 0)
            comboText.text = combo.ToString();
        else
            comboText.text = "";
    }
}
