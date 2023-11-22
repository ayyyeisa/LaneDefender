using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] uiElements;

    [SerializeField] private HighScoreHandler highScoreHandler;

    private void OnEnable()
    {
        HighScoreHandler.onHighScoreListChanged += UpdateUI;
    }

    private void OnDisable()
    {
        HighScoreHandler.onHighScoreListChanged -= UpdateUI;
    }
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
