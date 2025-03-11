using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{

    public int CurrentScore { get; private set; }
    
    [SerializeField] private TMP_Text _scoreText;

    private void Start()
    {
        AddScore(0);
    }

    public void AddScore(int score)
    {
        CurrentScore += score;
        _scoreText.text = $"Score: {CurrentScore}";
    }
    
}
