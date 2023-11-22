using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HighScoreHandler : MonoBehaviour
{
    public List<int> HighScoreList = new List<int>();
    private int maxCount = 5;
    [SerializeField] private string filename;

    public delegate void OnHighScoreListChanged(List<int> list);
    public static event OnHighScoreListChanged onHighScoreListChanged;

  //  public bool listChanged;

    private void Start()
    {
        LoadHighScores();
        //listChanged = false;
    }

    private void LoadHighScores()
    {
        HighScoreList = FileHandler.ReadListFromJSON<int> (filename);

        while(HighScoreList.Count > maxCount)
        {
            HighScoreList.RemoveAt(maxCount);
           // listChanged = true;
        }

        if(onHighScoreListChanged != null)
        {
            onHighScoreListChanged.Invoke(HighScoreList);
        }
    }

    private void SaveHighScore()
    {
        FileHandler.SaveToJSON<int>(HighScoreList, filename);
    }

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
