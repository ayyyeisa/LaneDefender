/*****************************************************************************
// File Name : EnemyController.cs
// Author : Isa Luluquisin
// Creation Date : November 21, 2023
//
// Brief Description : This controls the behavior of enemies.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour
{
    [Header("------------------Enemy variables-------------------")]
    [SerializeField] private Rigidbody2D enemy;
    [Tooltip("Speed at which the enemy is moving at")]
    public float speed;
    [Tooltip("How many times an enemy must be hit before dying")]
    [SerializeField] private int lives;

    [Header("References to game objects with scripts")]
    [SerializeField] private GameManager gM;
    [SerializeField] private PlayerController playerInstance;

    private AudioManager audioManager;

   // public Coroutine PauseRef;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        gM = FindObjectOfType<GameManager>();
        playerInstance = FindObjectOfType<PlayerController>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    void Update()
    {
        //ensures enemy is consistently moving to the left. they will only stop if game is over
        enemy.velocity = Vector2.left * speed;
        if(!playerInstance.gameIsRunning && playerInstance.spaceWasPressed)
        {
            enemy.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Handles collisions of enemies to other game objects. They will be destroyed upon colliding with 
    /// the player, the left side of the screen, and any bullets. The type of bullet affects how many lives
    /// they have lost. The sound effect "enemyhit" should also be played.
    /// 
    /// The collision of the enemy with the player or left side should also prompt the loss of player life.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Destroy(enemy.gameObject);
            gM.PlayerDied();
        }
        else if (collision.transform.tag == "LeftBox")
        {
            Destroy(gameObject);
            gM.PlayerDied();
        }

        else if (collision.transform.tag == "Bullet")
        {
            //plays corresponding SFX
            audioManager.PlaySFX(GameObject.FindObjectOfType<AudioManager>().EnemyHit);
            /*
            if(PauseRef == null)
            {
                StartCoroutine(Pause());
            }
            */
            lives--;
            if (lives <= 0)
            {
                Destroy(enemy.gameObject);
                gM.UpdateScore();
            }
        }
        else if(collision.transform.tag == "HeavyBullet")
        {
            //plays corresponding SFX
            audioManager.PlaySFX(GameObject.FindObjectOfType<AudioManager>().EnemyHit);
            /*
            if (PauseRef == null)
            {
                StartCoroutine(Pause());
            }
            StopCoroutine(Pause());
            */
            lives -= 3;
            if (lives <= 0)
            {
                Destroy(enemy.gameObject);
                gM.UpdateScore();
            }
        }
    }

    /*
    public IEnumerator Pause()
    {
        enemy.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        enemy.velocity = Vector2.left * speed;
        PauseRef = null;
    }
    */
}
