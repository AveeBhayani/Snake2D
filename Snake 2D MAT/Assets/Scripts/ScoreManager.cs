using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI ScoreText;
    private int score;
    public FoodSpawner foodspawner;
    private void Awake()
    {
        ScoreText = GetComponent<TextMeshProUGUI>();
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
        RefreshUI();
        foodspawner.SpawnObject();
        
    }

    private void RefreshUI()
    {
        ScoreText.text = "Score : " + score;
    }
}