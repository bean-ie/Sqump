using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfoHolder : MonoBehaviour
{
    public Level chosenLevel;

    public static LevelInfoHolder instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetChosenLevel(Level levelToChoose)
    {
        chosenLevel = levelToChoose;
    }
}
