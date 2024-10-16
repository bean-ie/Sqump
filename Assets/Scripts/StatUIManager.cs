using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUIManager : MonoBehaviour
{
    [SerializeField] GameObject statMenuGameObject;
    [SerializeField] Transform gamemodesLayoutGroup;
    [SerializeField] GameObject gamemodeStatUIElementPrefab;
    [SerializeField] LevelPicker levelPicker;

    private void Start()
    {
        for (int i = 0; i < levelPicker.levels.Length;  i++)
        {
            GamemodeStatElement statElement = Instantiate(gamemodeStatUIElementPrefab, gamemodesLayoutGroup).GetComponent<GamemodeStatElement>();
            statElement.gamemodeName.text = levelPicker.levels[i].levelName;
            statElement.wins.text = PlayerPrefs.GetInt(levelPicker.levels[i].levelName + "wins").ToString();
            statElement.deaths.text = PlayerPrefs.GetInt(levelPicker.levels[i].levelName + "deaths").ToString();
        }

        statMenuGameObject.SetActive(false);
    }

    public void OpenCloseStatMenu() 
    {
        statMenuGameObject.SetActive(!statMenuGameObject.activeInHierarchy);
    }
}
