using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject[] hearts;
    private int life; //3
    private bool dead;
    public float meterLossOnHit;
    private GameplayManager gm;

    private void Start()
    {
        life = hearts.Length;
        gm = GameObject.FindWithTag("EventSystem").GetComponent<GameplayManager>();
    }

    public void TakeDamage(int d)
    {
        if (life >= 1)
        {
            gm.meterLoss(meterLossOnHit);
            life -= d;
            UpdateHearts();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void UpdateHearts()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < life)
                hearts[i].SetActive(true);
            else
                hearts[i].SetActive(false);
        }
    }
}
