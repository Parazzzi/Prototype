using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Score score;

    [Header("Button")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button openShopButton;
    [SerializeField] private Button closeShopButton;


    private void OnEnable()
    {
        score.OnUpdateScore += UpdateScoreText;
    }

    private void Start()
    {
        ActiveCanvasOnStart();
        SetupButtonsListeners();
        UpdateScoreText();
    }

    private void ActiveCanvasOnStart()
    {
        mainMenuCanvas.SetActive(true);
        shopCanvas.SetActive(false);
    }

    private void SetupButtonsListeners()
    {
        playButton.onClick.AddListener(PlayButton);
        openShopButton.onClick.AddListener(OpenShopButton);
        closeShopButton.onClick.AddListener(CloseShopButton);
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score " + score.scoreData.score.ToString();
    }

    public void PlayButton()
    {
        SoundManager.instance.PlaySound(SoundNames.OPEN_CLOSED);
        SceneManager.LoadScene(1);
    }

    public void OpenShopButton()
    {
        SoundManager.instance.PlaySound(SoundNames.OPEN_CLOSED);
        shopCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void CloseShopButton()
    {
        SoundManager.instance.PlaySound(SoundNames.OPEN_CLOSED);
        shopCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    private void OnDisable()
    {
        score.OnUpdateScore -= UpdateScoreText;
    }

}
