using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] private GameObject highScoreUIElementPrefab;
    [SerializeField] private Transform elementWrapper;

    List<GameObject> uiElements = new List<GameObject>();

    private void UpdateUI (List<HighScoreElement> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            HighScoreElement element = list[i];

            if(element.Points > 0)
            {
                if(i >= uiElements.Count)
                {
                    var inst = Instantiate(highScoreUIElementPrefab, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(elementWrapper, false);

                        uiElements.Add(inst);
                }

                //write or overwrite points
                var texts = uiElements[i].GetComponentsInChildren<Text>();
                texts[0].text = element.Points.ToString();
            }
        }
    }
}
