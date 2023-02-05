using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject[] hearts; //[0] [1] [2]
    private int life; //3
    private bool dead;

    private void Start()
    {
        life = hearts.Length;
    }
    void Update()
    {
        if (dead == true)
        {

            Debug.Log("YOU ARE DEAD!");
        }
        // SET DEAD CODE
    }

    public void TakeDamage(int d)
    {

        if (life >= 1)
        {
            life -= d; //1- 1 - 0
            Destroy(hearts[life].gameObject); //[1]
            if (life < 1)
            {
                dead = true;
            }
        }
    }
}
