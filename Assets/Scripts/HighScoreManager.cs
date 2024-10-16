using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text normalHS, oldschoolHS, hardcoreHS;

    private void Start()
    {
        normalHS.text = "high score: " + PlayerPrefs.GetInt("normalhs", 0);
        oldschoolHS.text = "high score: " + PlayerPrefs.GetInt("oldschoolhs", 0);
        hardcoreHS.text = "high score: " + PlayerPrefs.GetInt("hardcorehs", 0);
    }
}
