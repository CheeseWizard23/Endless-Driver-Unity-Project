using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;

    public static int numOfCoins;
    public static int totalNumOfCoins;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI totalCoinsText;

    public static int score;
    public static int highScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        gameOver = false; 
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        isGameStarted = false;
        numOfCoins = 0;
        score = 0;
        totalNumOfCoins = PlayerPrefs.GetInt("TotalNumberOfCoins", 0);
    }

    void Update()
    {
        coinsText.text = "Coins: " + numOfCoins;
        totalCoinsText.text = "Total Coins: " + totalNumOfCoins;
        scoreText.text = "Score: " + score;

        if (gameOver)
        {
            /* Time.timeScale = 0; */
            highScore = PlayerPrefs.GetInt("High Score", 0);
            if (score > highScore)
            {
                PlayerPrefs.SetInt("High Score", score);
            }
            highScore = PlayerPrefs.GetInt("High Score", 0);
            highScoreText.text = "High Score: " + highScore;
            gameOverPanel.SetActive(true);
        }

        if (Input.GetKeyDown("up"))
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }
}
