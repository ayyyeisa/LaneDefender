using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        StartScreen.SetActive(true);
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }
    private void Update()
    {
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

    public void PlayerDied()
    {
        lives--;
        livesText.text = "Lives: " + lives;

        if(lives == 0)
        {
            GameOver();
        }
    }

    public void UpdateScore()
    {
        score += 100;

        scoreText.text = "Score: " + score;
    }

    private void GameOver()
    {
        highScoreHandler.AddHighScoreIfPossible(score);
        highScore = highScoreHandler.HighScoreList[0];
        endScoreText.text = "Current Score: " + score + "\n High Score: " + highScore;
        loseScreen.SetActive(true);
        playerInstance.gameIsRunning = false;
    }
}
