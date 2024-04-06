using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Score totalScore;
    [SerializeField] private ShopConfig shopConfig;
    [SerializeField] private PlayerConfiguration playerConfig;
    [SerializeField] private TextMeshProUGUI scoreShopText;

    [Header("MoveSpeed")]
    [SerializeField] private TextMeshProUGUI countMoveSpeedText;
    [SerializeField] private Button buyMoveSpeedButton;
    [SerializeField] private Button addMoveSpeedButton;
    [SerializeField] private Button minusMoveSpeedButton;

    [Header("Health")]
    [SerializeField] private TextMeshProUGUI countHealthText;
    [SerializeField] private Button buyHealthButton;
    [SerializeField] private Button addHealthButton;
    [SerializeField] private Button minusHealthButton;

    private float delayToPlaySound = 0.5f;

    private void OnEnable()
    {
        totalScore.OnUpdateScore += UpdateScoreCount;
    }

    private void Start()
    {
        UpdateMoveSpeedCount();
        UpdateHealthCount();
        UpdateScoreCount();
    }

    public void UpdateScoreCount()
    {
        scoreShopText.text = totalScore.scoreData.score.ToString();
    }

    private void UpdateMoveSpeedCount()
    {
        countMoveSpeedText.text = playerConfig.currentMoveSpeed.ToString();

        if (playerConfig.currentMoveSpeed == playerConfig.maxMoveSpeed)
        {
            addMoveSpeedButton.gameObject.SetActive(false);
            buyMoveSpeedButton.gameObject.SetActive(true);
        }
        else
        {
            addMoveSpeedButton.gameObject.SetActive(true);
            buyMoveSpeedButton.gameObject.SetActive(false);
        }

        if (playerConfig.currentMoveSpeed == 1)
        {
            minusMoveSpeedButton.gameObject.SetActive(false);
        }
        else
        {
            minusMoveSpeedButton.gameObject.SetActive(true);
        }
    }

    private void UpdateHealthCount()
    {
        countHealthText.text = playerConfig.currentHealthInStart.ToString();

        if (playerConfig.currentHealthInStart == playerConfig.maxHealth)
        {
            addHealthButton.gameObject.SetActive(false);
            buyHealthButton.gameObject.SetActive(true);
        }
        else
        {
            addHealthButton.gameObject.SetActive(true);
            buyHealthButton.gameObject.SetActive(false);
        }

        if (playerConfig.currentHealthInStart == 1)
        {
            minusHealthButton.gameObject.SetActive(false);
        }
        else
        {
            minusHealthButton.gameObject.SetActive(true);
        }
    }

    public void AddMoveSpeedCount()
    {
        playerConfig.currentMoveSpeed++;
        UpdateMoveSpeedCount();
    }

    public void MinusMoveSpeedCount()
    {
        playerConfig.currentMoveSpeed--;
        UpdateMoveSpeedCount();
    }

    public void AddHealthCount()
    {
        playerConfig.currentHealthInStart++;
        UpdateHealthCount();
    }

    public void MinusHealthCount()
    {
        playerConfig.currentHealthInStart--;
        UpdateHealthCount();

    }

    public void BuyHealth()
    {
        int pointsToDeduct = shopConfig.addHealth;
        ScoreData scoreData = totalScore.GetScoreData();

        if (pointsToDeduct <= scoreData.score)
        {
            SoundManager.instance.PlaySound(SoundNames.BUY);
            playerConfig.maxHealth += 1;
            playerConfig.currentHealthInStart = playerConfig.maxHealth;
            countMoveSpeedText.text = playerConfig.currentMoveSpeed.ToString();
            totalScore.DeductScore(pointsToDeduct);
            StartCoroutine(WaitForTimeAndDo(delayToPlaySound));
        }
        else
        {
            ErrorController.Instance.DisplayError(ErrorNames.NOT_MONEY_ERROR);
            Debug.Log("Not enough points to buy health!");
        }
    }

    public void BuyMoveSpeed()
    {
        int pointsToDeduct = shopConfig.moveSpeed;
        ScoreData scoreData = totalScore.GetScoreData();

        if (pointsToDeduct <= scoreData.score)
        {
            SoundManager.instance.PlaySound(SoundNames.BUY);
            playerConfig.maxMoveSpeed += 1f;
            playerConfig.currentMoveSpeed = playerConfig.maxMoveSpeed;
            countMoveSpeedText.text = playerConfig.currentMoveSpeed.ToString();
            totalScore.DeductScore(pointsToDeduct);
            StartCoroutine(WaitForTimeAndDo(delayToPlaySound));
        }
        else
        {
            ErrorController.Instance.DisplayError(ErrorNames.NOT_MONEY_ERROR);
            Debug.Log("Not enough points to buy move speed!");
        }
    }

    private IEnumerator WaitForTimeAndDo(float time)
    {
        yield return new WaitForSeconds(time);
        SoundManager.instance.PlaySound(SoundNames.AFTERBUY);
    }

    private void OnDisable()
    {
        totalScore.OnUpdateScore -= UpdateScoreCount;
    }
}
