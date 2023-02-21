using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private float timer = 0.0f, finalScore;
    [SerializeField] TextMeshProUGUI timerText, endScreenScore, endScreenWinLoss;

    [ContextMenu("Score")]
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - timer;

        string minutes = ((int)t / 59).ToString();
        string seconds = (t % 59).ToString("f0");

        timerText.text = minutes + ":" + seconds;

        if (Time.timeScale == 0f)
        {
            int scoreMinutes = Convert.ToInt32(minutes);
            int scoreSeconds = Convert.ToInt32(seconds);

            if (Convert.ToInt32(minutes) >= 1 && Convert.ToInt32(seconds) >= 0)
            {
                finalScore = (((float)scoreMinutes * 60 - scoreSeconds) * (float)scoreSeconds + 1) / 30 * 100;

                Math.Round((Decimal)finalScore, 0, MidpointRounding.AwayFromZero);
            }
            else
            {
                finalScore = ((float)scoreSeconds) / 30 * 8400;

                Math.Round((Decimal)finalScore, 0, MidpointRounding.AwayFromZero);
            }
            endScreenScore.text = finalScore.ToString("F0") + " points!";

            if (Convert.ToInt32(minutes) == 0 && Convert.ToInt32(seconds) <= 55)
            {
                endScreenWinLoss.text = "You Win!";
            }
            else
            {
                endScreenWinLoss.text = "You Lose!";
            }     
        }
    }   
}
