using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action onGameStarted;
    private bool isGameStarted;
    private float currentTimeScale;
    private int score;
    private int money;
    [SerializeField] private Tiger tigerPrefab;
    [SerializeField] private Transform tigerSpawnPos;

    private void Awake()
    {
        instance = this;
        currentTimeScale = Time.timeScale;
        if (PlayerPrefs.HasKey("Money"))
        {
            money = PlayerPrefs.GetInt("Money");
        }
        else
        {
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.Save();
        }
        Tiger.onNewPlatformReached += IncreaseScore;
    }
    private void OnDisable()
    {
        Tiger.onNewPlatformReached -= IncreaseScore;
    }
    private void Start()
    {
        UIManager.instance.ShowMoney(money.ToString());
    }
    private void Update()
    {
        if (isGameStarted)
        {
            
        }
    }
    public void IncreaseScore()
    {
        score++;
        UIManager.instance.ShowScore(score.ToString());
    }
    public void StartGame()
    {
        isGameStarted = true;
        onGameStarted?.Invoke();
        Time.timeScale = 1f;
        Tiger newTiger = Instantiate(tigerPrefab);
        CameraController.instance.SetTarget(newTiger.transform);
        newTiger.gameObject.transform.position = tigerSpawnPos.position;
        UIManager.instance.ShowScore(score.ToString());
    }
    public void AddMoney()
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 1);
        PlayerPrefs.Save();
    }
    public void PauseGame()
    {
        isGameStarted = false;
        Time.timeScale = 0f;
    }
    public void UnPauseGame()
    {
        isGameStarted = true;
        Time.timeScale = currentTimeScale;
    }
    public void EndGame()
    {
        isGameStarted = false;
        CheckBestScore();
        UIManager.instance.ShowLosePanel();
    }
    private void CheckBestScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            int tempBestScore = PlayerPrefs.GetInt("BestScore");
            if (tempBestScore > score)
            {
                UIManager.instance.ShowBestScore(tempBestScore.ToString());
            }
            else
            {
                UIManager.instance.ShowBestScore(score.ToString());
                PlayerPrefs.SetInt("BestScore", score);
                PlayerPrefs.Save();
            }
        }
        else
        {
            UIManager.instance.ShowBestScore(score.ToString());
            PlayerPrefs.SetInt("BestScore", score);
            PlayerPrefs.Save();
        }
    }
    public bool IsGameStarted()
    {
        return isGameStarted;
    }
}
