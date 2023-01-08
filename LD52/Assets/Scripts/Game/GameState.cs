using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public State state;
    public int scorePlayer;
    public int scoreDevourer;

    public enum State
    {
        Title,
        Ingame,
        End
    }
}
