using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;


public class Score : MonoBehaviour
{
    public event Action OnUpdateScore;

    public ScoreData scoreData;
    private string savePath;

    public int currentRoundScore;

    private void OnEnable()
    {
        Meteor.OnMeteorTakeDamage += AddScore;
        SpaceShip.OnSpaceShipTakeDamage += AddScore;
    }

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "score.json");
        scoreData = new ScoreData();
    }

    void Start()
    {
        LoadScore();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            AddScore(100);
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            DeductScore(100);
        }
    }

    public ScoreData GetScoreData()
    {
        return scoreData;
    }

    public void DeductScore(int points)
    {
        if (points <= scoreData.score)
        {
            scoreData.score -= points;
            SaveScore();
        }
        else
        {
            Debug.Log("NO Money");
        }
    }

    public void AddScore(int points)
    {
        scoreData.score += points;
        currentRoundScore += points;
        SaveScore();
    }

    private void SaveScore()
    {
        string json = JsonUtility.ToJson(scoreData);
        File.WriteAllText(savePath, json);
        OnUpdateScore?.Invoke();
    }

    private void LoadScore()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            scoreData = JsonUtility.FromJson<ScoreData>(json);
        }
    }

    private void OnDisable()
    {
        Meteor.OnMeteorTakeDamage -= AddScore;
        SpaceShip.OnSpaceShipTakeDamage -= AddScore;
    }
}
