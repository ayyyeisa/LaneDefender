using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] private GameObject highScoreUIElementPrefab;
    [SerializeField] private Transform elementWrapper;

    List<GameObject> uiElements = new List<GameObject>();
}
