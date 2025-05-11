using System;
using GameLogic.SaveLogic.SaveData;
using Managers;

namespace GameLogic
{
    public class GameOver
    {
        private readonly SaveController _saveController;
        private readonly ScoreManager _scoreManager;

        public event Action OnGameOver;

        public GameOver(SaveController saveController, ScoreManager scoreManager)
        {
            _scoreManager = scoreManager;
            _saveController = saveController;
        }

        public void EndGame()
        {
            SaveData();
            OnGameOver?.Invoke();
        }

        private void SaveData()
        {
            SaveData data = _saveController.GetRecord();
            if(data.ScoreRecord < _scoreManager.CurrentScore)
            {
                data.ScoreRecord = _scoreManager.CurrentScore;
                _saveController.SaveRecord(data);
            }
        }
    }
}