using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game inst;

    public GameInput input;
    public GameUI ui;
    public GameRefs refs;
    public GameState state;
    public GameData data;
    public GameProgress progress;
    public new GameAudio audio;

    private void Awake()
    {
        inst = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
