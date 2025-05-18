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
        public event Action OnGameExit;
        public event Action OnContinueGame;

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
        
        public void ContinueGame()
        {
            OnContinueGame?.Invoke();
        }
        
        public void OnOnGameExit()
        {
            OnGameExit?.Invoke();
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