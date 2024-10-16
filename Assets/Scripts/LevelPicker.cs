using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class LevelPicker : MonoBehaviour
{

    public Level[] levels;
    [SerializeField] GameObject levelPrefab;
    [SerializeField] Transform grid;

    private void Start()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            GameObject createdLevel = Instantiate(levelPrefab, grid);
            LevelUI ui = createdLevel.GetComponent<LevelUI>();
            ui.gamemodeName.text = levels[i].levelName;
            ui.gamemodeDescription.text = levels[i].levelDescription;
            ui.highScoreText.text = "high score: " + PlayerPrefs.GetInt(levels[i].levelName + "hs", 0);
            Color backgroundColor = levels[i].levelColor;
            backgroundColor.a = 0.25f;
            ui.gamemodeName.color = levels[i].levelColor;
            ui.backgroundImage.color = backgroundColor;
            ui.playButton.image.color = levels[i].levelColor;
            int localI = i;
            ui.playButton.onClick.AddListener(delegate { PickLevel(levels[localI]); });
        }
    }

    public void PickLevel(Level level)
    {
        LevelInfoHolder.instance.chosenLevel = level;
        SceneManager.LoadScene(1);
    }
}
