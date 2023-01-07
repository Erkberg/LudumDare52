using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public int currentLevel;
    public float levelTimePassed;

    private void Awake()
    {

    }

    private void Update()
    {
        CheckNextLevel();
    }

    private void CheckNextLevel()
    {
        levelTimePassed += Time.deltaTime;
        if(levelTimePassed > 60f)
        {
            StartNextLevel();
        }
    }

    private void StartNextLevel()
    {
        levelTimePassed = 0f;
        currentLevel++;
    }
}
