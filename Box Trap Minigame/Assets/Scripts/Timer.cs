using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float startTime;

    public float score;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f0");

        if((t % 60) < 9.5)
        {
            timerText.text = minutes + ":0" + seconds;
        }
        else
        {
            timerText.text = minutes + ":" + seconds;
        }
    }

    public void CheckScore()
    {
        score = Time.time - startTime;
    }
}
