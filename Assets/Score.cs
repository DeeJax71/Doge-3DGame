using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private float timer = 0.0f;
    [SerializeField] TextMeshProUGUI timerText, endScreenText;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t  = Time.time - timer;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f0");

        timerText.text = minutes + ":" + seconds;

        if (Time.timeScale == 0f)
        {
            int scoreMinutes = Convert.ToInt32(minutes) + 2;
            int scoreSeconds = Convert.ToInt32(seconds);
            int finalScore = (scoreMinutes * scoreSeconds) / 60* 500;

            Debug.Log(minutes);
                Debug.Log(seconds);
            Debug.Log(scoreMinutes);
            Debug.Log(scoreSeconds);
            Debug.Log(finalScore);
            endScreenText.text = finalScore.ToString();
        }
    }
}
