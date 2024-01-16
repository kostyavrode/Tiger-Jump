using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TMP_Text scoreBar;
    [SerializeField] private TMP_Text bestScoreBar;
    [SerializeField] private TMP_Text moneyBar;
    [SerializeField] private GameObject soundButtonOn;
    [SerializeField] private GameObject soundButtonOff;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Image sprite;
    private void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetString("Sound", "true");
            PlayerPrefs.Save();
        }
        CheckSound();
    }
    public void StartGame()
    {
        GameManager.instance.StartGame();
    }
    public void ShowForce(float t)
    {
        sprite.fillAmount = t;
    }
    public void CheckSound()
    {
        if (PlayerPrefs.GetString("Sound")=="true")
        {
            audioSource.Play();
            soundButtonOff.SetActive(true);
            soundButtonOn.SetActive(false);
        }
        else
        {
            audioSource.Pause();
            soundButtonOff.SetActive(false);
            soundButtonOn.SetActive(true);
        }
    }
    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
        inGamePanel.SetActive(false);
    }
    public void SoundOff()
    {
        PlayerPrefs.SetString("Sound", "false");
        PlayerPrefs.Save();
    }
    public void SoundOn()
    {
        PlayerPrefs.SetString("Sound", "true");
        PlayerPrefs.Save();
    }
    public void ShowMoney(string money)
    {
        moneyBar.text = money;
    }
    public void PauseGame()
    {
        GameManager.instance.PauseGame();
    }
    public void UnPauseGame()
    {
        GameManager.instance.UnPauseGame();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowScore(string score)
    {
        scoreBar.text = score;
    }
    public void ShowBestScore(string bestScore)
    {
        bestScoreBar.text = bestScore;
    }
    public void ShowPrivacy(string url)
    {

    }
}
