using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public List<ToolData> toolData;

    public ToolData GetToolData(Tool.Id id)
    {
        return toolData.Find(x => x.id == id);
    }

    public ToolLevelData GetToolCurrentLevelData(Tool.Id id)
    {
        int level = Game.inst.progress.GetToolLevel(id);
        return GetToolData(id).levelData.Find(x => x.level == level);
    }
}
