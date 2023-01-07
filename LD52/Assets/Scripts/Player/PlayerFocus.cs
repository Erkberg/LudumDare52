using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocus : MonoBehaviour
{
    public Camera mainCam;
    public float fovFocussed;
    public float fovUnfocussed;

    private bool isFocussing;
    private float targetFov;

    private void Awake()
    {
        targetFov = fovUnfocussed;
    }

    private void Update()
    {
        CheckFocus();
        AdjustFov();
    }

    private void CheckFocus()
    {
        if (Game.inst.input.GetFocusDown())
        {
            isFocussing = true;
            targetFov = fovFocussed;
        }

        if (Game.inst.input.GetFocusUp())
        {
            isFocussing = false;
            targetFov = fovUnfocussed;
        }
    }

    private void AdjustFov()
    {
        mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, targetFov, 0.1f);
    }

    public bool IsFocussing()
    {
        return isFocussing;
    }
}
