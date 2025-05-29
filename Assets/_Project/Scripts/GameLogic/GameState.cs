using System;
using Cysharp.Threading.Tasks;
using GameLogic.SaveLogic.SaveData;
using Managers;

namespace GameLogic
{
    public class GameState
    {
        private readonly SaveController _saveController;
        private readonly ScoreService _scoreService;

        public event Action OnGameOver;
        public event Action OnGameExit;
        public event Func<UniTask> OnGameContinue;

        public void EndGame()
        {
            OnGameOver?.Invoke();
        }

        public void ContinueGame()
        {
            OnGameContinue?.Invoke();
        }

        public void GameExit()
        {
            OnGameExit?.Invoke();
        }
    }
}