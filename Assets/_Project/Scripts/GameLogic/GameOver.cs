using System;
using GameLogic.SaveLogic.SaveData;
using Managers;

namespace GameLogic
{
    public class GameOver
    {
        private readonly SaveController _saveController;
        private readonly ScoreService _scoreService;

        public event Action OnGameOver;

        public GameOver(SaveController saveController, ScoreService scoreService)
        {
            _scoreService = scoreService;
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
            if(data.ScoreRecord < _scoreService.CurrentScore)
            {
                data.ScoreRecord = _scoreService.CurrentScore;
                _saveController.SaveRecord(data);
            }
        }
    }
}