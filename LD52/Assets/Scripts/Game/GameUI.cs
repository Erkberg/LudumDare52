using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image levelProgressFillImage;

    public void SetLevelProgress(float value)
    {
        levelProgressFillImage.fillAmount = value;
    }
}
