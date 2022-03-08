using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score scoreTxt;

    public Text scoreText;
    private int playerScore = 0;
    private int enemyScore = 0;

    private void Awake()
    {
        scoreTxt = this;
    }

    void Update()
    {
        scoreText.text = "Player: " + playerScore + "\r\n" + "Enemy: " + enemyScore;
    }

    public void AddPlayerPoint()
    {
        playerScore++;
    }

    public void AddEnemyPoint()
    {
        enemyScore++;
    }
}
