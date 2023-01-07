using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ToolData : ScriptableObject
{
    public Tool.Id id;
    public List<ToolLevelData> levelData;
}
