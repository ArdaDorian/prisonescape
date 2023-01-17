using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    int lives = 3;
    int score = 0;

    [SerializeField] TextMeshProUGUI lives_text, score_text;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        lives_text.text = lives.ToString();
        score_text.text = score.ToString();
    }

    public void IncreaseScore(int point)
    {
        score += point;
        score_text.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (lives > 1)
        {
            GoStartAtLevel();
        }

        else
        {
            
            ResetGameSession();
        }
    }

    private void GoStartAtLevel()
    {
        lives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        lives_text.text = lives.ToString();
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        FindObjectOfType<SceneSession>().ResetSceneSession();
    }
}
