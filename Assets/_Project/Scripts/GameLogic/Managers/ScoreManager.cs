using System;
using ModestTree;
using Zenject;

namespace Managers
{
    public class ScoreManager : IInitializable
    {
        public event Action<int> OnScoreChanged;

        public int CurrentScore { get; private set; }

        private bool _startFlag;

        public void AddScore(int score)
        {
            CurrentScore += score;
            OnScoreChanged?.Invoke(CurrentScore);
        }

        [Inject]
        public void Initialize()
        {
            if (_startFlag == false)
            {
                _startFlag = true;
                AddScore(0);
            }
        }
    }
}