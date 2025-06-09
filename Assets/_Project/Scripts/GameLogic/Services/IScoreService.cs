using System;

namespace _Project.Scripts.GameLogic.Services
{
    public interface IScoreService
    {
        public event Action<int> OnScoreChanged;

        public void AddScore(int score);

    }
}