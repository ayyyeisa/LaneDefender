/*****************************************************************************
// File Name : HighScoreHandler.cs
// Author : Isa Luluquisin
// Creation Date : November 21, 2023
//
// Brief Description : This is a file that controls what happens to high scores.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HighScoreHandler : MonoBehaviour
{
    [Tooltip("List of the highscores from JSON file")]
    public List<int> HighScoreList = new List<int>();
    [Tooltip("Name of the json file that will be read/written to")]
    [SerializeField] private string filename;

    //number of items in the list
    private int maxCount = 5;

    //subscription to events
    public delegate void OnHighScoreListChanged(List<int> list);
    public static event OnHighScoreListChanged onHighScoreListChanged;

    private void Start()
    {
        LoadHighScores();
    }

    /// <summary>
    /// When the game is opened, the json file containing a list of highscores are read.
    /// If there are more than five elements, it is shrunk back down to 5 by deleting the last element.
    /// </summary>
    private void LoadHighScores()
    {
        HighScoreList = FileHandler.ReadListFromJSON<int> (filename);

        while(HighScoreList.Count > maxCount)
        {
            HighScoreList.RemoveAt(maxCount);
        }

        if(onHighScoreListChanged != null)
        {
            onHighScoreListChanged.Invoke(HighScoreList);
        }
    }

    /// <summary>
    /// The highscore is savedto the json file
    /// </summary>
    private void SaveHighScore()
    {
        FileHandler.SaveToJSON<int>(HighScoreList, filename);
    }

    /// <summary>
    /// If the highscore is within the top 5 scores in the json file, it is inserted.
    /// The last element is then deleted.
    /// </summary>
    /// <param name="points">the number of points player earned that game</param>
    public void AddHighScoreIfPossible(int points)
    {
        for(int i = 0; i < maxCount; i++)
        {
            //adds high score if there's space in the list or if the score is higher than one of the scores listed
            if(i >= HighScoreList.Count || points > HighScoreList[i])
            {
                HighScoreList.Insert(i, points);
               // listChanged = true;

                while (HighScoreList.Count > maxCount)
                {
                    HighScoreList.RemoveAt(maxCount);
                }

                SaveHighScore();

                if (onHighScoreListChanged != null)
                {
                    onHighScoreListChanged.Invoke(HighScoreList);
                }

                break;
            }
        }
    }

    
}
