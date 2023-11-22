using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HighScoreHandler : MonoBehaviour
{
    public List<HighScoreElement> HighScoreList = new List<HighScoreElement>();
    private int maxCount = 5;
    [SerializeField] private string filename;

    private void Start()
    {
        LoadHighScores();
    }

    private void LoadHighScores()
    {
        HighScoreList = FileHandler.ReadListFromJSON<HighScoreElement> (filename);

        while(HighScoreList.Count > maxCount)
        {
            HighScoreList.RemoveAt(maxCount);
        }
    }

    private void SaveHighScore()
    {
        FileHandler.SaveToJSON<HighScoreElement>(HighScoreList, filename);
    }

    public void AddHighScoreIfPossible(HighScoreElement element)
    {
        for(int i = 0; i < maxCount; i++)
        {
            //adds high score if there's space in the list or if the score is higher than one of the scores listed
            if(i >= HighScoreList.Count || element.Points > HighScoreList[i].Points)
            {
                HighScoreList.Insert(i, element);

                while (HighScoreList.Count > maxCount)
                {
                    HighScoreList.RemoveAt(maxCount);
                }

                SaveHighScore();

                break;
            }
        }
    }

    
}
