using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public State state;
    public int level;
    public float timeInLevel;
    public int scorePlayer;
    public int scoreDevourer;

    public enum State
    {
        Title,
        Ingame,
        End
    }

    private void Update()
    {
        timeInLevel += Time.deltaTime;

        if(timeInLevel > 30f)
        {
            timeInLevel = 0f;
            level++;
            Game.inst.ui.ShowState();
            foreach(Devourer dev in Game.inst.refs.devourers)
            {
                dev.huntingRange += 1f;
            }
        }
    }
}
