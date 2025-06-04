using System;

namespace Service
{
    public interface IScoreService
    {
        public event Action<int> OnScoreChanged;

        public void AddScore(int score);

    }
}