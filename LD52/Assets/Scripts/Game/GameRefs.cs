using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameRefs : MonoBehaviour
{
    public PlayerController player;
    public World world;
    public Level level;
    public List<Soul> souls;
    public List<Devourer> devourers;

    public Transform GetClosestDevourerTarget(Vector3 devPosition)
    {
        List<Transform> targets = new List<Transform>();
        targets.Add(player.bodyCC.transform);
        foreach(Soul soul in souls)
        {
            targets.Add(soul.transform);
        }

        targets = targets.OrderBy(x => Vector3.Distance(devPosition, x.position)).ToList();
        return targets[0];
    }

    public Transform GetRandomDevourerTarget()
    {
        List<Transform> targets = new List<Transform>();
        targets.Add(player.bodyCC.transform);
        foreach (Soul soul in souls)
        {
            targets.Add(soul.transform);
        }
        
        return targets.GetRandomItem();
    }

    public Transform GetClosestSoulTarget(Vector3 soulPosition)
    {
        List<Transform> targets = new List<Transform>();
        targets.Add(player.bodyCC.transform);
        foreach (Devourer dev in devourers)
        {
            targets.Add(dev.transform);
        }

        targets = targets.OrderBy(x => Vector3.Distance(soulPosition, x.position)).ToList();
        Debug.Log("first " + Vector3.Distance(soulPosition, targets[0].position));
        Debug.Log("last " + Vector3.Distance(soulPosition, targets.LastOrDefault().position));
        return targets[0];
    }
}
