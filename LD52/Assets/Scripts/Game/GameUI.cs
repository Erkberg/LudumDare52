using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image levelProgressFillImage;
    public Image enduranceImage;
    public Image salvationImage;
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityValue;

    public GameObject titleScreen;
    public GameObject endScreen;
    public TextMeshProUGUI scorePlayerText;
    public TextMeshProUGUI scoreDevourerText;

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

    public void OnGameStart()
    {
        titleScreen.SetActive(false);
    }

    public void OnStartButtonClicked()
    {
        OnGameStart();
        Game.inst.StartGame();
    }

    public void OnGameEnd()
    {
        scorePlayerText.text = Game.inst.state.scorePlayer.ToString();
        scoreDevourerText.text = Game.inst.state.scoreDevourer.ToString();
        endScreen.SetActive(true);
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnSensValueChanged(float value)
    {
        sensitivityValue.text = value.ToString("0.00");
    }

    public float GetSensValue()
    {
        return sensitivitySlider.value;
    }
}
