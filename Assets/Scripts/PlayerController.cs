/*****************************************************************************
// File Name : PlayerController.cs
// Author : Isa Luluquisin
// Creation Date : November 10, 2023
//
// Brief Description : This has all the player functions like movement.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction move;
    [SerializeField] private InputAction shoot;
    [SerializeField] private InputAction restart;
    [SerializeField] private InputAction quit;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        EnableInput();  
    }

    // Update is called once per frame
    void Update()
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
        Debug.Log("Player is moving");
    }

    private void Move_Canceled(InputAction.CallbackContext obj)
    {
        Debug.Log("Player move is canceled");
    }

    private void Shoot_Started(InputAction.CallbackContext obj)
    {
        Debug.Log("Bullet was shot");
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
