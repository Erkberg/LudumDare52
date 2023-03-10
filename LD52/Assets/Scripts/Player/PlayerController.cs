using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement;
    public PlayerBody body;
    public PlayerBodyCC bodyCC;
    public PlayerFocus focus;

    public bool IsControllable()
    {
        return Time.timeScale != 0f;
    }
}
