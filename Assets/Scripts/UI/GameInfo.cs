using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [SerializeField] private PlayerConfiguration playerConfig;
    [SerializeField] private Timer timer;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI roundTimeText;

    private void Update()
    {
        UpdateGameInfo();
    }

    private void UpdateGameInfo()
    {
        healthText.text = "Health " + playerConfig.currentHealth.ToString();
        ammoText.text = "Ammo " + playerConfig.currentBulletCount.ToString();
        roundTimeText.text = "Round Time " + timer.GetCurrentTimeAsString();
    }

}
