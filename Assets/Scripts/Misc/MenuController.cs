/*****************************************************************************
// File Name : MenuController.cs
// Author : Isa Luluquisin
// Creation Date : November 21, 2023
//
// Brief Description : This is a file that controls the main menu behavior.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    /// <summary>
    /// loads the game scene when the "new game" button is pressed
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("LaneDefender");
    }

    /// <summary>
    /// quits out of unity when the "quit game" button is pressed
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
