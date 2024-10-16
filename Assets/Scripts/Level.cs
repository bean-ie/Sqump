using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level")]
public class Level : ScriptableObject
{
    public string levelName;
    public string levelDescription;
    public Color levelColor = Color.white;

    public GameObject[] availablePlatforms;
    public bool dontRandomizePostProcessing;
    public bool enableTimer;
    public float timePerPlatform;

    public Vector2 minmaxXSpacing = new Vector2(10, 13), minmaxYSpacing = new Vector2(-2, 2), minmaxLevelSize = new Vector2(5, 16);
    public bool randomizeDirectionOnEachPlatform;

    public bool allowDash;
    public float dashCooldown;
    public int doubleJumpAmount = 0;
}
