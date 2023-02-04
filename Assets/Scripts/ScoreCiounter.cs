using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public Text counterText;
    int points;
    void Update()
    {
        ShowScore();
    }

    private void ShowScore()
    {
        counterText.text = points.ToString();
    }
    public void KeepScore()
    { 
           
    }
}
