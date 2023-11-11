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

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        tank = GetComponent<Rigidbody2D>();
        EnableInput();  
    }

    // Update is called once per frame
    void Update()
    {
        if (tankIsMoving)
        {
            moveDirection = move.ReadValue<float>();
        }
    }

    private void FixedUpdate()
    {
        TankMovement();
    }

    private void TankMovement()
    {
        if(tankIsMoving)
        {
            Debug.Log("Tank should be moving");
            tank.velocity = new Vector2(0, speed * moveDirection);
        }
        else
        {
            Debug.Log("Tank should not be moving");
            tank.velocity = Vector2.zero;
        }
    }
    private void Shoot()
    {

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

        move.started += Move_Started;
        move.canceled += Move_Canceled;
        shoot.started += Shoot_Started;
        restart.started += Restart_Started;
        quit.started += Quit_Started;

    }
     private void Move_Started(InputAction.CallbackContext obj)
    {
        tankIsMoving = true;
    }

    private void Move_Canceled(InputAction.CallbackContext obj)
    {
        tankIsMoving = false;
    }

    private void Shoot_Started(InputAction.CallbackContext obj)
    {
        Debug.Log("Bullet was shot");
        Shoot();
    }

    private void Restart_Started(InputAction.CallbackContext obj)
    {
        Debug.Log("Restart button was called.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Quit_Started(InputAction.CallbackContext obj)
    {
        Debug.Log("Player quit game");
        Application.Quit();
    }

    #endregion

    public void OnDestroy()
    {
        move.started -= Move_Started;
        move.canceled -= Move_Canceled;
        shoot.started -= Shoot_Started;
        restart.started -= Restart_Started;
        quit.started -= Quit_Started;
    }
}
