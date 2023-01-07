using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Enable();
    }

    public Vector2 GetMove()
    {
        return controls.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetLook()
    {
        return controls.Player.Look.ReadValue<Vector2>();
    }

    public bool GetDash()
    {
        return controls.Player.Dash.WasPerformedThisFrame();
    }

    public bool GetDashDown()
    {
        return controls.Player.Dash.WasPressedThisFrame();
    }

    public bool GetDashUp()
    {
        return controls.Player.Dash.WasReleasedThisFrame();
    }
}
