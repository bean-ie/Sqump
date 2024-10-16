using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] float startSpacing;
    [SerializeField] Vector2 minmaxXSpacing, minmaxYSpacing, minmaxLevelSize;
    [SerializeField] GameObject[] allPlatforms;
    [SerializeField] TImerController timerController;
    public Level currentLevel;
    [SerializeField] GameObject lava, finishPlatform;
    float lowestPlatform;
    public int levelSize;
    GameObject[] spawnedPlatforms;  

    void Start()
    {
        currentLevel = LevelInfoHolder.instance.chosenLevel;
        GenerateLevel();
    }

    void GenerateLevel()
    {
        lowestPlatform = -2;
        int direction = (int)Mathf.Sign(Random.value - 0.5f);
        float prevX = startSpacing * direction;
        float prevY = -2;
        Vector2 spawnPosition;
        levelSize = Random.Range((int)currentLevel.minmaxLevelSize.x, (int)currentLevel.minmaxLevelSize.y + 1);
        if (currentLevel.enableTimer) timerController.ResetTimer(levelSize);
        spawnedPlatforms = new GameObject[levelSize];
        for (int i = 0; i < levelSize; i++)
        {
            spawnPosition.x = prevX + Random.Range(currentLevel.minmaxXSpacing.x, currentLevel.minmaxXSpacing.y) * direction * (currentLevel.randomizeDirectionOnEachPlatform ? Mathf.Sign(Random.value - 0.5f) : 1);
            spawnPosition.y = prevY + Random.Range(currentLevel.minmaxYSpacing.x, currentLevel.minmaxYSpacing.y);
            if (spawnPosition.y < lowestPlatform) lowestPlatform = spawnPosition.y;
            prevX = spawnPosition.x;
            prevY = spawnPosition.y;
            GameObject platformToSpawn;
            if (i == levelSize - 1)
            {
                platformToSpawn = finishPlatform;
            }
            else
            {
                platformToSpawn = currentLevel.availablePlatforms[Random.Range(0, currentLevel.availablePlatforms.Length)];
                if (i != 0)
                {
                    while ((spawnedPlatforms[i - 1].TryGetComponent<SwitchPlatform>(out SwitchPlatform sp) || spawnedPlatforms[i - 1].TryGetComponent<DisappearingPlatform>(out DisappearingPlatform dp)) && platformToSpawn == allPlatforms[2])
                    {
                        platformToSpawn = currentLevel.availablePlatforms[Random.Range(0, currentLevel.availablePlatforms.Length)];
                    }
                }
            }
            spawnedPlatforms[i] = Instantiate(platformToSpawn, spawnPosition, Quaternion.identity);
        }
        lava.transform.position = Vector2.up * (lowestPlatform - 33);
    }
    public void RegenerateLevel()
    {
        foreach(GameObject platform in spawnedPlatforms)
        {
            Destroy(platform);
        }
        GenerateLevel();
    }
}
