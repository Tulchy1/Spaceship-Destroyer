using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreBoard : MonoBehaviour
{   
    TMP_Text scoreText;
    int score;
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "0";
    }
    public void IncreaseScore(int increaseAmount)
    {
        score += increaseAmount;
        scoreText.text = score.ToString();
    }
}
