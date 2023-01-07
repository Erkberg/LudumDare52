using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public Dictionary<Tool.Id, int> toolLevels;

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

    public int GetToolLevel(Tool.Id id)
    {
        return toolLevels[id];
    }


}
