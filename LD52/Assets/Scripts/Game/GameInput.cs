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

    public bool GetJump()
    {
        return controls.Player.Jump.WasPerformedThisFrame();
    }

    public bool GetJumpDown()
    {
        return controls.Player.Jump.WasPressedThisFrame();
    }

    public bool GetJumpUp()
    {
        return controls.Player.Jump.WasReleasedThisFrame();
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

    public bool GetFocus()
    {
        return controls.Player.Focus.WasPerformedThisFrame();
    }

    public bool GetFocusDown()
    {
        return controls.Player.Focus.WasPressedThisFrame();
    }

    public bool GetFocusUp()
    {
        return controls.Player.Focus.WasReleasedThisFrame();
    }
}
