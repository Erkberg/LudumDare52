using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement;
    public PlayerTools tools;
    public PlayerStats stats;
    public PlayerAbilities abilities;

    public bool IsControllable()
    {
        return Time.timeScale != 0f;
    }
}
