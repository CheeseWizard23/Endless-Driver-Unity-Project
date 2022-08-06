using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI totalCoinsText;
    public static int totalNumOfCoins;

    public TextMeshProUGUI highScoreText;
    public static int highScore;

    public void Start()
    {
        totalNumOfCoins = PlayerPrefs.GetInt("TotalNumberOfCoins", 0);
        totalCoinsText.text = "Total Coins: " + totalNumOfCoins;

        highScore = PlayerPrefs.GetInt("High Score", 0);
        highScoreText.text = "High Score: " + highScore;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
