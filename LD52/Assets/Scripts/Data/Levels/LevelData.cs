using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public int id;
    public float duration;

    public List<LevelSoulData> soulData;
}
