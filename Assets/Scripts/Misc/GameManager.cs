/*****************************************************************************
// File Name : GameManager.cs
// Author : Isa Luluquisin
// Creation Date : November 20, 2023
//
// Brief Description : This is a file that handles panels and text throughout the game.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("Object references for scripts")]
    [SerializeField] private PlayerController playerInstance;

    [Header("Screens and Texts")]
    [Tooltip("Game instructions that appear before game starts")]
    public GameObject StartScreen;
    [Tooltip("Endscore and instructions to restart the game appear")]
    [SerializeField] private GameObject loseScreen;
    [Tooltip("Player's score by the end of the game")]
    [SerializeField] private TMP_Text endScoreText;
    [Tooltip("Player's current score")]
    [SerializeField] private TMP_Text scoreText;
    [Tooltip("Highest score on device")]
    [SerializeField] private TMP_Text highScoreText;
    [Tooltip("How many lives player has left")]
    [SerializeField] private TMP_Text livesText;
    [Tooltip("Game object that has children that should be active during gameplay")]
    public GameObject InGameText;

    //number of times player can be hit before game over
    public int lives = 3;
    //score of the player
    public int score = 0;
    //highest score in-game
    private int highScore;

    [SerializeField] private HighScoreHandler highScoreHandler;

    private AudioManager audioManager;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        StartScreen.SetActive(true);
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }
    private void Update()
    {
        //updates the high score if there is a highscore in the list
        if (highScoreHandler.HighScoreList.Count > 0)
        {
            highScore = highScoreHandler.HighScoreList[0];
        }
        else if (highScoreHandler.HighScoreList.Count == 0)
        {
            highScore = 0;
        }
        highScoreText.text = "High Score: " + highScore;
    }

    /// <summary>
    /// Handles what happens if a player loses a life. This is updated on-screen.
    /// When player has used up all their lives, it is game over.
    /// </summary>
    public void PlayerDied()
    {
        audioManager.PlaySFX(GameObject.FindObjectOfType<AudioManager>().LifeLost);
        lives--;
        livesText.text = "Lives: " + lives;

        if(lives == 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// Updates the current score in the current game. Each enemy death is worth 100 points
    /// </summary>
    public void UpdateScore()
    {
        audioManager.PlaySFX(GameObject.FindObjectOfType<AudioManager>().EnemyDeath);
        score += 100;

        scoreText.text = "Score: " + score;
    }

    /// <summary>
    /// Handles what occurs when the game ends. The high score is saved if it is in the
    /// top 5 scores. Both the currrent and high scores are listed and instructions to restart
    /// the game or go back to main menu are listed. 
    /// </summary>
    private void GameOver()
    {
        highScoreHandler.AddHighScoreIfPossible(score);
        highScore = highScoreHandler.HighScoreList[0];
        endScoreText.text = "Current Score: " + score + "\n High Score: " + highScore;
        loseScreen.SetActive(true);
        playerInstance.gameIsRunning = false;
    }
}
