using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyCC : MonoBehaviour
{
    public PlayerController pc;

    public void OnDevour(Transform dev)
    {
        Game.inst.EndGame();
    }
}
