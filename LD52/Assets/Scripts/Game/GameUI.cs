using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image levelProgressFillImage;
    public Image enduranceImage;
    public Image salvationImage;

    public void SetLevelProgress(float value)
    {
        levelProgressFillImage.fillAmount = value;
    }

    public void SetEndurance(float value)
    {
        enduranceImage.CrossFadeAlpha(value, 0f, false);
    }

    public void SetSalvation(float value)
    {
        //salvationImage.CrossFadeAlpha(value, 0f, false);
        salvationImage.fillAmount = value;
    }
}
