using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    
    public Text healthText;
    public int score;
    public Text scoreText;
    public Text treasureText;
    public int chests;

    public int totalScore;

    public static GameController instance;

    private bool isPaused;

    public GameObject gameoverObj;
    public GameObject pauseObj;
    public GameObject finishObj;
    // Start is called before the first frame update
    
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        totalScore = PlayerPrefs.GetInt("score");
        Debug.Log(PlayerPrefs.GetInt("score"));
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
        Congratulations();
    }
    
    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
        
        PlayerPrefs.SetInt("score",score + totalScore);
    }
    
    public void UpdateChests(int value)
    {
        chests += value;
        treasureText.text = chests.ToString() + "/4";
    }
    
    public void UpdateLives(int value)
    {
        healthText.text = "x " + value.ToString();
        
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            pauseObj.SetActive(true);
        }

        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
            pauseObj.SetActive(false);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameoverObj.SetActive(true);
    }
    
    public void Congratulations()
    {
        if (chests == 4)
        {
            StartCoroutine("Finish");
        }
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(1.5f);
        finishObj.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void ReturntoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
