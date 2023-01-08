using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatButtonTimescale : MonoBehaviour
{
    public float timeScale = 8f;

    private GameInput input;

    private void Awake()
    {
        input = Game.inst.input;
    }

    private void Update()
    {
        if (input.GetCheatButtonDown())
        {
            Time.timeScale = timeScale;
        }
        else if (input.GetCheatButtonUp())
        {
            Time.timeScale = 1f;
        }
    }
}
