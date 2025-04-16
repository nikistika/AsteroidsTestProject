using System;

namespace Managers
{
    public class ScoreManager
    {
        public event Action<int> OnScoreChanged;

        public int CurrentScore { get; private set; }

        private bool _startFlag;
        
        public void StartWork()
        {
            if (_startFlag == false)
            {
                _startFlag = true;
                AddScore(0);
            }
        }

        public void AddScore(int score)
        {
            CurrentScore += score;
            OnScoreChanged?.Invoke(CurrentScore);
        }
    }
}