using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float currentTime;
    private StringBuilder timerStringBuilder;

    private void OnEnable()
    {
        currentTime = 0f;
        timerStringBuilder = new StringBuilder();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        DisplayTime(currentTime);
    }

    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerStringBuilder.Clear();
        timerStringBuilder.AppendFormat("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timerStringBuilder.ToString();
    }

    public string GetCurrentTimeAsString()
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
