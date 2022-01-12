using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject controlsPanel;
    public Animator mainMenuPanel;
    public Animator transition;

    public Text highScoreText;
    public Text highScore2Text;
    public Text highScore3Text;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("menu");
        transition.SetTrigger("Close");

        if(PlayerPrefs.GetFloat("Fhighscore") > 0)
        {
            highScoreText.text = "Highscore: " + PlayerPrefs.GetString("HighScore");
        }
        else
        {
            highScoreText.text = "Highscore: 0:00";
        }

        if (PlayerPrefs.GetFloat("Fhighscore2") > 0)
        {
            highScore2Text.text = "Highscore: " + PlayerPrefs.GetString("HighScore2");
        }
        else
        {
            highScore2Text.text = "Highscore: 0:00";
        }

        if (PlayerPrefs.GetFloat("Fhighscore3") > 0)
        {
            highScore3Text.text = "Highscore: " + PlayerPrefs.GetString("HighScore3");
        }
        else
        {
            highScore3Text.text = "Highscore: 0:00";
        }

    }

    public void StageOneButton()
    {
        FindObjectOfType<AudioManager>().Play("click");
        transition.SetTrigger("Open");

        Invoke("StageOne", 1f);
    }

    void StageOne()
    {
        SceneManager.LoadScene("StageOne");
    }

    public void StageTwoButton()
    {
        FindObjectOfType<AudioManager>().Play("click");
        transition.SetTrigger("Open");

        Invoke("StageTwo", 1f);
    }

    void StageTwo()
    {
        SceneManager.LoadScene("StageTwo");
    }

    public void StageThreeButton()
    {
        FindObjectOfType<AudioManager>().Play("click");
        transition.SetTrigger("Open");

        Invoke("StageThree", 1f);
    }

    void StageThree()
    {
        SceneManager.LoadScene("StageThree");
    }


    public void Back()
    {
        FindObjectOfType<AudioManager>().Play("click");
        mainMenuPanel.SetTrigger("Open");
    }

    public void PlayButton()
    {
        FindObjectOfType<AudioManager>().Play("click");
        mainMenuPanel.SetTrigger("Close");
    }

    public void OpenControls()
    {
        FindObjectOfType<AudioManager>().Play("click");
        controlsPanel.SetActive(true);
        controlsPanel.GetComponent<Animator>().SetTrigger("Open");
    }

    public void CloseControls()
    {
        FindObjectOfType<AudioManager>().Play("click");
        controlsPanel.GetComponent<Animator>().SetTrigger("Close");
    }

    public void ResetHighScores()
    {
        FindObjectOfType<AudioManager>().Play("click");
        PlayerPrefs.DeleteAll();
        highScoreText.text = "Highscore: 0:00";
        highScore2Text.text = "Highscore: 0:00";
        highScore3Text.text = "Highscore: 0:00";
    }

    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Application.Quit();
    }
}
