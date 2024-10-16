using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreText;
    [SerializeField] PostProcessingController ppController;
    int score = 0;
    private void Start()
    {
        scoreText.text = score.ToString();
    }
    public void AddScore(int add)
    {
        score += add;
        StartCoroutine("UpdateScore");
    }

    public void ResetScore()
    {
        UpdateHighScore();
        score = 0;
        StartCoroutine("UpdateScore");
    }

    IEnumerator UpdateScore()
    {
        yield return new WaitForSeconds(1f);

        scoreText.text = score.ToString();
        ppController.SetScoreGlitches(score);
    }

    public void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt(LevelInfoHolder.instance.chosenLevel.levelName + "hs"))
        {
            PlayerPrefs.SetInt(LevelInfoHolder.instance.chosenLevel.levelName + "hs", score);
        }
    }
}
