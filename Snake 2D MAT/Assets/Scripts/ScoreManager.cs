using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI ScoreText;
    private int score;
    private int Multiplier;
    private float MultiplierTime;
    private bool Boosting;

    private LevelGrid levelgrid;

    public void Setup(LevelGrid levelGrid)
    {
        this.levelgrid = levelGrid;
    }

    private void Awake()
    {
        ScoreText = GetComponent<TextMeshProUGUI>();
        Multiplier = 1;
        MultiplierTime = 0f;
        Boosting = false;
    }
    private void Update()
    {
        if (Boosting)
        {
            MultiplierTime += Time.deltaTime;
            if(MultiplierTime >= 15f)
            {
                Multiplier = 1;
                MultiplierTime = 0f;
                Boosting = false;
            }
        }
    }
    public void ScoreBoostActive()
    {
        Multiplier = 2;
        Boosting = true;

    }
    public void IncreaseScore(int increment)
    {
        score += increment * Multiplier;
        RefreshUI();       
    }

    private void RefreshUI()
    {
        ScoreText.text = "Score : " + score;
    }
}