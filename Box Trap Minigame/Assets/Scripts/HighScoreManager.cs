using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public int stage;

    public Timer timer;

    public void CheckHighScore()
    {

        if(stage == 1)
        {
            if (timer.score > PlayerPrefs.GetFloat("Fhighscore"))
            {
                PlayerPrefs.SetFloat("Fhighscore", timer.score);
                PlayerPrefs.SetString("HighScore", timer.timerText.text);
            }
        }

        if(stage == 2)
        {
            if (timer.score > PlayerPrefs.GetFloat("Fhighscore2"))
            {
                PlayerPrefs.SetFloat("Fhighscore2", timer.score);
                PlayerPrefs.SetString("HighScore2", timer.timerText.text);
            }
        }

        if (stage == 3)
        {
            if (timer.score > PlayerPrefs.GetFloat("Fhighscore3"))
            {
                PlayerPrefs.SetFloat("Fhighscore3", timer.score);
                PlayerPrefs.SetString("HighScore3", timer.timerText.text);
            }
        }
    }
}
