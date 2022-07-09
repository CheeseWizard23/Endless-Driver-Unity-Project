using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;

    void Start()
    {
        gameOver = false; 
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        isGameStarted = false;
    }

    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        if (Input.GetKeyDown("up"))
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }
}
