using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TImerController : MonoBehaviour
{
    float timeLeft;
    [SerializeField] TMP_Text timerText;
    [SerializeField] LevelGenerator levelGenerator;
    [SerializeField] PlayerManager playerManager;

    private void Start()
    {
        if (!LevelInfoHolder.instance.chosenLevel.enableTimer)
        {
                Destroy(timerText);
                Destroy(gameObject);
        }
    }

    public void ResetTimer(float size)
    {
        timeLeft = size * LevelInfoHolder.instance.chosenLevel.timePerPlatform;
    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            if (!playerManager.immune) timeLeft -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timeLeft).ToString();
        } else if (timeLeft < 0 && !playerManager.immune)
        {
            playerManager.Die();
            timeLeft = 0;
            timerText.text = "0";
        }
    }
}
