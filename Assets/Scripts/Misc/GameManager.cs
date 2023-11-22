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
    [Tooltip("How many lives player has left")]
    [SerializeField] private TMP_Text livesText;
    [Tooltip("Game object that has children that should be active during gameplay")]
    public GameObject InGameText;

    //number of times player can be hit before game over
    private int lives = 3;
    //score of the player
    private int score = 0;
    //highest score in-game
    private int highScore;


    // Start is called before the first frame update
    void Start()
    {
        StartScreen.SetActive(true);
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
       // highScoreText.text = "High Score: " + highScore;
    }

    public void PlayerDied()
    {
        lives--;
        livesText.text = "Lives: " + lives;

        if(lives == 0)
        {
            endScoreText.text = "Score: " + score + "/n New High Score: " + highScore;
            loseScreen.SetActive(true);
            playerInstance.gameIsRunning = false;
        }
    }

    public void UpdateScore()
    {
        score += 100;

        scoreText.text = "Score: " + score;
    }
}
