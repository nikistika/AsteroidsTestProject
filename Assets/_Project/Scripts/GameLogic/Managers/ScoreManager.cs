using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event Action<int> OnScoreChanged;

    public int CurrentScore { get; private set; }

    private void Start()
    {
        AddScore(0);
    }
    
    public void AddScore(int score)
    {
        CurrentScore += score;
        OnScoreChanged?.Invoke(CurrentScore);
    }
}
