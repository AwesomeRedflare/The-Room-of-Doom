using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    public Text timeLastedText;
    public Text diagnosisText;
    public Text highScoreText;
    public GameObject readyText;

    public Timer timer;
    public HighScoreManager highScoreManager;
    public GoodPlatformerController player;

    public Animator transition;

    private void Start()
    {
        transition.SetTrigger("Close");
        StartCoroutine("ReadySurvive");
    }

    public void MainMenuButton()
    {
        FindObjectOfType<AudioManager>().Play("click");
        transition.SetTrigger("Open");

        Invoke("GoToMainMenu", 1f);
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void TryAgainButton()
    {
        FindObjectOfType<AudioManager>().Play("click");
        transition.SetTrigger("Open");

        Invoke("Reload", 1f);
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver(string d)
    {
        timer.CheckScore();
        highScoreManager.CheckHighScore();
        if (highScoreManager.stage == 1)
        {
            highScoreText.text = "Your highscore is " + PlayerPrefs.GetString("HighScore");
        }
        if (highScoreManager.stage == 2)
        {
            highScoreText.text = "Your highscore is " + PlayerPrefs.GetString("HighScore2");
        }
        if (highScoreManager.stage == 3)
        {
            highScoreText.text = "Your highscore is " + PlayerPrefs.GetString("HighScore3");
        }

        diagnosisText.text = d;
        timeLastedText.text = "You survived for " + timer.timerText.text;

        GameOverPanel.SetActive(true);
        GetComponent<AudioSource>().volume = 0;

        TurnOffObstacles();
    }

    IEnumerator ReadySurvive()
    {
        timer.enabled = false;
        player.enabled = false;

        yield return new WaitForSeconds(2);

        FindObjectOfType<AudioManager>().Play("click");
        readyText.GetComponent<Text>().text = "Survive!";

        yield return new WaitForSeconds(1);

        timer.enabled = true;
        player.enabled = true;
        readyText.SetActive(false);
        TurnOnObstacles();
        GetComponent<AudioSource>().Play();
        //FindObjectOfType<AudioManager>().Play("music");
    }

    void TurnOnObstacles()
    {
        foreach (ObstacleSpawner o in FindObjectsOfType<ObstacleSpawner>())
        {
            o.Invoke("SawBladeSpawner", 0f);
        }

        foreach (BoulderSpawner b in FindObjectsOfType<BoulderSpawner>())
        {
            b.StartCoroutine("SpawnBoulder");
        }
    }

    void TurnOffObstacles()
    {
        foreach (ObstacleSpawner o in FindObjectsOfType<ObstacleSpawner>())
        {
            Destroy(o.gameObject);
        }

        foreach (BoulderSpawner b in FindObjectsOfType<BoulderSpawner>())
        {
            Destroy(b.gameObject);
        }
    }
}
