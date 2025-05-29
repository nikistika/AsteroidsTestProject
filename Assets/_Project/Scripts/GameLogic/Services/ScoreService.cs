using System;
using GameLogic;
using GameLogic.SaveLogic.SaveData;
using Zenject;

namespace Managers
{
    public class ScoreService : IInitializable
    {
        public event Action<int> OnScoreChanged;

        public int CurrentScore { get; private set; }

        private readonly GameState _gameState;
        private readonly SaveController _saveController;

        private bool _startFlag;


        public ScoreService(
            GameState gameState,
            SaveController saveController)
        {
            _gameState = gameState;
            _saveController = saveController;
        }

        public void Initialize()
        {
            _gameState.OnGameOver += SaveData;
            _gameState.OnGameExit += GameExit;

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

        private void SaveData()
        {
            SavedData data = _saveController.GetData();
            if (data.ScoreRecord < CurrentScore)
            {
                data.ScoreRecord = CurrentScore;
                _saveController.SetData(data);
            }
        }

        private void GameExit()
        {
            _gameState.OnGameOver -= SaveData;
        }
    }
}