using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] private Image firstPlayerHealthBar;
    [SerializeField] private Image secondPlayerHealthBar;

    [Header("Mana Bar")]
    [SerializeField] private Image firstPlayerManaBar;
    [SerializeField] private Image secondPlayerManaBar;

    [Header("Countdown Timer")]
    [SerializeField] private TextMeshProUGUI timerText;
    private float countdownTime = 120f;

    void Update()
    {
        UpdateCountdownTimer();
    }

    private void UpdateCountdownTimer()
    {
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
            int seconds = Mathf.CeilToInt(countdownTime);

            if (seconds <= 15)
            {
                timerText.color = Color.red;
            }

            timerText.text = seconds.ToString();
        }
        else
        {
            countdownTime = 0;
            timerText.text = "0";
            Time.timeScale = 0;
            SceneManager.LoadSceneAsync(3);
        }
    }

    public void UpdateFirstPlayerHealthBar(float curentHP, float maxHP)
    {
        firstPlayerHealthBar.fillAmount = curentHP / maxHP;
    }

    public void UpdateSecondPlayerHealthBar(float curentHP, float maxHP)
    {
        secondPlayerHealthBar.fillAmount = curentHP / maxHP;
    }

    public void UpdateFirstPlayerManaBar(float currentMana, float maxMana)
    {
        firstPlayerManaBar.fillAmount = currentMana / maxMana;
    }

    public void UpdateSecondPlayerManaBar(float currentMana, float maxMana)
    {
        secondPlayerManaBar.fillAmount = currentMana / maxMana;
    }
}
