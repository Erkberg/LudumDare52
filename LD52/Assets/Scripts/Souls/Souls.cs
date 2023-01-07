using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souls : MonoBehaviour
{
    public List<Soul> soulPrefabs;
    public float spawnDistance;

    private Dictionary<Soul.Id, float> soulSpawnCooldownsPassed;

    private void Awake()
    {
        CreateDicts();
    }

    private void CreateDicts()
    {
        soulSpawnCooldownsPassed = new Dictionary<Soul.Id, float>
        {
            { Soul.Id.Hovering, 0f },
            { Soul.Id.Charging, 0f },
            { Soul.Id.Circling, 0f },
            { Soul.Id.Flying, 0f },
            { Soul.Id.Jumping, 0f }
        };
    }

    private void Update()
    {
        CheckSpawn();
    }

    private void CheckSpawn()
    {
        foreach(LevelSoulData levelSoulData in Game.inst.data.GetCurrentLevelData().soulData)
        {
            soulSpawnCooldownsPassed[levelSoulData.id] += Time.deltaTime;
            if(soulSpawnCooldownsPassed[levelSoulData.id] > levelSoulData.cooldown)
            {
                soulSpawnCooldownsPassed[levelSoulData.id] = 0f;
                SpawnSouls(levelSoulData);
            }
        }
    }

    private void SpawnSouls(LevelSoulData levelSoulData)
    {
        for (int i = 0; i < levelSoulData.amount; i++)
        {
            SpawnSoul(levelSoulData.id);
        }
    }

    private void SpawnSoul(Soul.Id id)
    {
        Soul soulPrefab = GetSoulPrefab(id);
        Soul soul = Instantiate(soulPrefab, GetRandomSpawnPosition(soulPrefab.spawnY), Quaternion.identity, transform);
    }

    private Soul GetSoulPrefab(Soul.Id id)
    {
        return soulPrefabs.Find(x => x.id == id);
    }

    private Vector3 GetRandomSpawnPosition(float spawnY)
    {
        Vector3 playerPos = Game.inst.refs.player.body.transform.position;
        Vector3 spawnPos = playerPos + Quaternion.Euler(0f, Random.Range(0f, 360f), 0f) * Vector3.forward * spawnDistance;
        spawnPos.y = spawnY;
        return spawnPos;
    }
}
