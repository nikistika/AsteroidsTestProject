using System;
using UI;

namespace GameLogic
{
    public class GameOver
    {
        public event Action OnGameOver;

        public void EndGame()
        {
            OnGameOver?.Invoke();
        }
    }
}