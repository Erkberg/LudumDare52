using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyCC : MonoBehaviour
{
    public PlayerController pc;

    public void OnDevour(Transform dev)
    {
        pc.movement.LookAtTransform(dev);
        Game.inst.EndGame();
    }
}
