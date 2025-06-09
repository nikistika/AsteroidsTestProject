using System;
using Cysharp.Threading.Tasks;

namespace GameLogic
{
    public class GameState
    {
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