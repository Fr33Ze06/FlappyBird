using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField]
    public Canvas canvasGameOver;
    [SerializeField]
    public Canvas canvasStart;

    private bool isGameActive = true;
    private bool isGamePaused = true;

    [Header("Score")]
    public TextMeshProUGUI ScoreGameoverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    private int score = 0;
    private int bestScore;

    [Header("Sounds")]
    [SerializeField]
    private AudioSource dieSound;

    [SerializeField]
    private AudioSource coinSound;

    void Start()
    {
        PauseGame();
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        highscoreText.text = "Best : " + bestScore;
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    private void Update()
    {   
        if (isGamePaused && Input.GetKeyDown(KeyCode.Space))
        {
            ResumeGame();
            canvasStart.gameObject.SetActive(false);
        }
        if (!isGameActive && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    public void GameOver()
    {
        dieSound.Play();
        isGameActive = false;
        Debug.Log("Game Over");
        PauseGame();
        canvasGameOver.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        isGameActive = true;
        canvasGameOver.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void ResumeGame(){
        Time.timeScale = 1f;
    }

    private void PauseGame(){
        Time.timeScale = 0f;
    }

    public void IncreaseScore()
    {
        coinSound.Play();
        score++;
        if (score > bestScore)
        {
            bestScore = score;

            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
        ScoreGameoverText.text = "" + score;
    }

}
