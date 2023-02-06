using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int meterGainOnDeath;
    public int pointGainOnDeath;

    private GameplayManager gm;

    void Start()
    {
        gm = GameObject.FindWithTag("EventSystem").GetComponent<GameplayManager>();
    }

    public void TakeDamage(int amt)
    {
        health -= amt;
        if (health < 1)
        {
            gm.meterGain(meterGainOnDeath);
            gm.addScore(pointGainOnDeath);
            Destroy(gameObject);
        }
    }
}
