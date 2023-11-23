/*****************************************************************************
// File Name : PlayerController.cs
// Author : Isa Luluquisin
// Creation Date : November 10, 2023
//
// Brief Description : This has all the player functions like movement.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameManager gM;
    private AudioManager audioManager;

    [Tooltip("ActionMap being used")]
    [SerializeField] private PlayerInput playerInput;
    private InputAction move;
    private InputAction shoot;
    private InputAction restart;
    private InputAction quit;

    [Tooltip("References the sprite that the player is controlling")]
    [SerializeField] private Rigidbody2D tank;
    [SerializeField] private float speed;
    private float moveDirection;
    private bool tankIsMoving;

    public bool spaceWasPressed;
    public bool gameIsRunning;

    public Coroutine BulletRef;

    [Header("Variables relating to the bullets being spawned")]
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private float delay = .5f; //allows for delay
    [SerializeField] private bool spaceIsHeld;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        gameIsRunning = false;
        spaceWasPressed = false;
        spaceIsHeld = false;
        tank = GetComponent<Rigidbody2D>();
        EnableInput();
    }

    // Update is called once per frame
    void Update()
    {
        //reads the values of key inputs
        if (tankIsMoving)
        {
            moveDirection = move.ReadValue<float>();
        }
        if(gameIsRunning && spaceIsHeld)
        {
            if(BulletRef == null)
            {
                BulletRef = StartCoroutine(BulletSpawn());
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameIsRunning)
        {
            TankMovement();
        }
    }

    /// <summary>
    /// Moves the tank up and down depending on player input
    /// </summary>
    private void TankMovement()
    {
        if (tankIsMoving)
        {
            tank.velocity = new Vector2(0, speed * moveDirection);
        }
        else
        {
            tank.velocity = Vector2.zero;
        }
    }
    
    /// <summary>
    /// Rapid fires according to the delay
    /// </summary>
    /// <returns>time in between light bullet spawning</returns>
    public IEnumerator BulletSpawn()
    {
        yield return new WaitForSeconds(delay);
        LightBullet();
        BulletRef = null;
    }

    /// <summary>
    /// Instantiates a light bullet and called upon the behaviors and properties of a light bullet
    /// </summary>
    private void LightBullet()
    {
        audioManager.PlaySFX(GameObject.FindObjectOfType<AudioManager>().TankShoot);
        BulletController bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.ShootHold();
    }

    /// <summary>
    /// Instantiates a heavy bullet and calls upon the behaviors and properties of a heavy bullet
    /// </summary>
    private void HeavyBullet()
    {
        audioManager.PlaySFX(GameObject.FindObjectOfType<AudioManager>().TankShoot);
        BulletController bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.ShootOnce();
    }

    /// <summary>
    /// When space is pressed for the first time, the game starts.
    /// 
    /// When pressed afterwards, HeavyBullet() is called.
    /// When held, spaceIsheld turns LightBullet() is called
    /// </summary>
    /// <param name="ia"></param>
    private void OnShoot(InputValue ia)
    {
        //starts game if game hasn't started already
        if (!spaceWasPressed)
        {
            gM.StartScreen.SetActive(false);
            gM.InGameText.SetActive(true);
            spaceWasPressed = true;
            gameIsRunning = true;
        }
        else if (spaceWasPressed && gameIsRunning)
        {
            float val = ia.Get<float>();

            if (val >= InputSystem.settings.defaultHoldTime)
            {
                spaceIsHeld = true;
                LightBullet();
            }
            else
            {
                if (val <= InputSystem.settings.defaultButtonPressPoint)
                {
                    HeavyBullet();
                }
            }
        }

    }

    #region Input Actions
    /// <summary>
    /// This enables the current action map so that player input is taken in by the keyboard.
    /// </summary>
    private void EnableInput()
    {
        playerInput.currentActionMap.Enable();

        move = playerInput.currentActionMap.FindAction("Movement");
        shoot = playerInput.currentActionMap.FindAction("Shoot");
        restart = playerInput.currentActionMap.FindAction("Restart");
        quit = playerInput.currentActionMap.FindAction("Quit");

        move.started += Move_started;
        move.canceled += Move_canceled;
        shoot.canceled += Shoot_canceled;                                                           
        restart.started += Restart_started;
        quit.started += Quit_started;

    }
     private void Move_started(InputAction.CallbackContext obj)
    {
        tankIsMoving = true;   
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        tankIsMoving = false;
    }

    private void Shoot_canceled(InputAction.CallbackContext obj)
    {
        spaceIsHeld = false;
    }

    private void Restart_started(InputAction.CallbackContext obj)
    {
        Debug.Log("Restart button was called.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Quit_started(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene("MainMenu");
    }

    #endregion

    public void OnDestroy()
    {
        move.started -= Move_started;
        move.canceled -= Move_canceled;
        shoot.canceled -= Shoot_canceled;
        restart.started -= Restart_started;
        quit.started -= Quit_started;
    }
}
