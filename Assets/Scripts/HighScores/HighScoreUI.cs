/*****************************************************************************
// File Name : HighScoreUI.cs
// Author : Isa Luluquisin
// Creation Date : November 21, 2023
//
// Brief Description : This is a file that handles updates of the high scores
                        on the UI highscore panel in main menu.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour
{
    [Tooltip("One of the listed elements in highscores")]
    [SerializeField] private TMP_Text[] uiElements;
    [Tooltip("Refers to script handling highscores")]
    [SerializeField] private HighScoreHandler highScoreHandler;

    private void OnEnable()
    {
        HighScoreHandler.onHighScoreListChanged += UpdateUI;
    }

    private void OnDisable()
    {
        HighScoreHandler.onHighScoreListChanged -= UpdateUI;
    }

    /// <summary>
    /// Updates the UI by changes the text on the UI elements to match
    /// the top 5 scores.
    /// </summary>
    /// <param name="list"> list of the high scores in json file </param>
    private void UpdateUI (List<int> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            int points = list[i];
            print(points);

            //overwrite points
            uiElements[i].text = points.ToString();
     
        }
    }
}
