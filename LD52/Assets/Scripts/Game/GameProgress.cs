using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public Dictionary<Tool.Id, int> toolLevels;

    public int currentLevel;
    public float levelTimePassed;
    public float currentPlayerLevel;
    public float playerExp;

    private void Awake()
    {
        InitDicts();
    }

    public void InitDicts()
    {
        toolLevels = new Dictionary<Tool.Id, int>
        {
            { Tool.Id.Scythe, 0 },
            { Tool.Id.Missile, 0 },
            { Tool.Id.Area, 0 },
            { Tool.Id.Scatter, 0 },
            { Tool.Id.Easer, 0 },
            { Tool.Id.Laser, 0 }
        };
    }

    private void Update()
    {
        CheckNextLevel();
    }

    private void CheckNextLevel()
    {
        levelTimePassed += Time.deltaTime;
        if(levelTimePassed > Game.inst.data.GetCurrentLevelData().duration)
        {
            StartNextLevel();
        }
    }

    private void StartNextLevel()
    {
        levelTimePassed = 0f;
        currentLevel++;
    }

    public int GetToolLevel(Tool.Id id)
    {
        return toolLevels[id];
    }

    public void IncreaseToolLevel(Tool.Id id)
    {
        toolLevels[id]++;
    }

    public void OnExpPickup(float value)
    {
        playerExp += value;
        float expNeeded = GetNextLevelExpNeeded();
        if (playerExp >= expNeeded)
        {
            playerExp -= expNeeded;
            currentPlayerLevel++;
            expNeeded = GetNextLevelExpNeeded();
            Game.inst.ui.OnLevelUp();
        }

        Game.inst.ui.SetLevelProgress(playerExp / expNeeded);
    }

    public float GetNextLevelExpNeeded()
    {
        return 2 + Mathf.Pow(2, currentPlayerLevel + 1);
    }
}
