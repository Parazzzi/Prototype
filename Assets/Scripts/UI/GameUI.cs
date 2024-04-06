using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI playerAmmoText;
    [SerializeField] private TextMeshProUGUI totalRoundScore;

    [SerializeField] private PlayerConfiguration playerConfig;
    [SerializeField] private Score score;

    [Header("Canvas")]
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private Canvas LozeCanvas;
    [SerializeField] private Canvas PauseCanvas;

    [Header("Button in Pause Canvas")]
    [SerializeField] private Button pauseOnButton;
    [SerializeField] private Button pauseOffButton;
    [SerializeField] private Button mainMenuPauseButton;

    [Header("Button in Loze Canvas")]
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuLoseButton;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDied += ActiveLozeCanvas;
        score.OnUpdateScore += UpdateScoreText;
        SoundManager.instance.PlaySound(SoundNames.OKEY_LETS_GO);
        score.currentRoundScore = 0;
    }

    private void Start()
    {
        ActiveCanvasOnStart();
        SetupPauseButtonsListeners();
        UpdateScoreText();
    }

    private void ActiveCanvasOnStart()
    {
        RoundTimeSckale(1);
        gameCanvas.gameObject.SetActive(true);
        LozeCanvas.gameObject.SetActive(false);
        PauseCanvas.gameObject.SetActive(false);
    }
    private void SetupPauseButtonsListeners()
    {
        pauseOnButton.onClick.AddListener(PauseOn);
        pauseOffButton.onClick.AddListener(PauseOff);
        mainMenuPauseButton.onClick.AddListener(LoadMainMenu);
        playAgainButton.onClick.AddListener(LoadLvl1);
        mainMenuLoseButton.onClick.AddListener(LoadMainMenu);
    }

    public void UpdateAmmoText()
    {
        playerAmmoText.text = playerConfig.currentBulletCount.ToString();
    }

    public void UpdateHealthText()
    {
        playerHealthText.text = "Health " + playerConfig.currentHealth.ToString();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score " + score.scoreData.score.ToString();
    }

    private void ActiveLozeCanvas()
    {
        LozeCanvas.gameObject.SetActive(true);
        totalRoundScore.text = "Score Earned - " + score.currentRoundScore.ToString();
        RoundTimeSckale(0);
    }

    public void LoadMainMenu()
    {
        SoundManager.instance.PlaySound(SoundNames.OPEN_CLOSED);
        SceneManager.LoadScene(0);
        RoundTimeSckale(1);
    }

    public void LoadLvl1()
    {
        SoundManager.instance.PlaySound(SoundNames.OPEN_CLOSED);
        SceneManager.LoadScene(1);
    }

    public void PauseOn()
    {
        SoundManager.instance.PlaySound(SoundNames.OPEN_CLOSED);
        SoundManager.instance.PlaySound(SoundNames.YEMETE_KUDASSAI);
        PauseCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
        RoundTimeSckale(0);
    }

    public void PauseOff()
    {
        SoundManager.instance.PlaySound(SoundNames.OPEN_CLOSED);
        PauseCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);
        RoundTimeSckale(1);
    }

    private void RoundTimeSckale(float time)
    {
        Time.timeScale = time;
    }


    private void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= ActiveLozeCanvas;
        score.OnUpdateScore -= UpdateScoreText;
    }

}


