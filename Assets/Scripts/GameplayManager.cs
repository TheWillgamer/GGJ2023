using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public TMP_Text multiplierText;
    public Image meterBar;
    private float timer;
    private bool timerActive;

    private float meter;         // Combo meter
    public float meterLossRate;
    private int maxMeter = 400;

    private int score;

    private RhythmManager rm;

    //Music player
    public AudioClip[] clips;
    public float[] delay;
    public float[] bpm;
    public float[] length;
    public AudioSource aSource;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        timer = 0f;
        timerActive = true;
        rm = GameObject.FindWithTag("UI").GetComponent<RhythmManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
            timer += Time.deltaTime;
        DisplayTime();

        meterLoss(meterLossRate * Time.deltaTime);
        UpdateMeter();

        UpdateMusic();
    }

    void DisplayTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void UpdateMeter()
    {
        float meterDisplay = meter;
        if (meter < 100)
        {
            multiplierText.text = "";
        }
        else if (meter < 200)
        {
            multiplierText.text = "2x";
            meterDisplay -= 100;
        }
        else if (meter < 300)
        {
            multiplierText.text = "5x";
            meterDisplay -= 200;
        }
        else
        {
            multiplierText.text = "10x";
            meterDisplay -= 300;
        }
        meterBar.fillAmount = meterDisplay / 100;
    }

    public void meterGain(int amt)
    {
        meter += amt;
        if(meter > maxMeter)
        {
            meter = maxMeter;
        }
    }

    public void meterLoss(float amt)
    {
        meter -= amt;
        if (meter < 0f)
        {
            meter = 0f;
        }
    }

    void UpdateMusic()
    {
        if (Input.GetKey(KeyCode.J))
        {
            aSource.clip = clips[0];
            rm.activated = true;
            rm.spawnRate = 60f / bpm[0];
            rm.timer = 0f - delay[0];
            aSource.Play();
            Invoke("DisableRM", length[0]);
        }
    }

    void DisableRM()
    {
        rm.activated = false;
    }

    public void addScore(int amt)
    {
        if (meter < 100)
        {
            score += amt;
        }
        else if (meter < 200)
        {
            score += amt * 2;
        }
        else if (meter < 300)
        {
            score += amt * 5;
        }
        else
        {
            score += amt * 10;
        }
        scoreText.text = score.ToString();
    }
}
