using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocus : MonoBehaviour
{
    public Camera mainCam;
    public float fovFocussed;
    public float fovUnfocussed;
    public float focussedLookSpeedMultiplier = 0.33f;
    public float salvationRange = 16f;
    public float salvationIncreaseMultiplier = 0.33f;
    public float salvationDecreaseMultiplier = 2f;
    public float salvationValue;

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
        CheckSalvation();
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

    private void CheckSalvation()
    {
        Debug.DrawLine(mainCam.transform.position, mainCam.transform.position + mainCam.transform.forward * GetRange(), Color.yellow);
        bool hasHit = Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hit, GetRange());
        if(hasHit)
        {
            Soul soul = hit.collider.GetComponent<Soul>();
            if(soul)
            {
                salvationValue += Time.deltaTime * salvationIncreaseMultiplier;
                if(salvationValue >= 1f)
                {
                    soul.OnSalvation();
                    salvationValue = 0f;
                }
            }
            else
            {
                DecreaseSalvation();
            }
        }
        else
        {
            DecreaseSalvation();
        }

        Game.inst.ui.SetSalvation(salvationValue);
    }

    private float GetRange()
    {
        return IsFocussing() ? salvationRange * 2 : salvationRange;
    }

    private void DecreaseSalvation()
    {
        if(salvationValue > 0f)
        {
            salvationValue -= Time.deltaTime * salvationDecreaseMultiplier;
            if (salvationValue < 0f)
            {
                salvationValue = 0f;
            }
        }
    }

    public bool IsFocussing()
    {
        return isFocussing;
    }
}
