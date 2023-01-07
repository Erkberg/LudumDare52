using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public Id id;
    public PlayerTools tools;
    public ToolArea area;
    public ToolProjectile projectilePrefab;

    protected float cooldownPassed;
    protected GameData data;

    public enum Id
    {
        None,
        Scythe,
        Missile,
        Area,
        Scatter,
        Easer,
        Laser
    }

    protected virtual void Awake()
    {
        data = Game.inst.data;
    }

    protected virtual void Update()
    {
        CheckCooldown();
    }

    protected virtual void CheckCooldown()
    {
        Timing.AddTimeAndCheckMax(ref cooldownPassed, GetData().cooldown, Time.deltaTime, OnCooldown);
    }

    protected virtual void OnCooldown() { }

    public virtual float GetDamage()
    {
        return GetData().damage;
    }

    protected ToolLevelData GetData()
    {
        return data.GetToolCurrentLevelData(id);
    }
}
