using System;
using Cysharp.Threading.Tasks;
using GameLogic;
using GameLogic.SaveLogic.SaveData;
using GameLogic.SaveLogic.SaveData.Save;
using GameLogic.SaveLogic.SaveData.Time;
using SaveLogic;
using UnityEngine;
using Zenject;

namespace Service
{
    public class ScoreService : IInitializable, IScoreService
    {
        public event Action<int> OnScoreChanged;
        private event Action SaveHandler;

        
        public int CurrentScore { get; private set; }

        private readonly GameState _gameState;
        private readonly ITimeService _timeService;
        private readonly ISaveService _saveService;

        private bool _startFlag;

        public ScoreService(
            GameState gameState,
            ISaveService saveService,
            ITimeService timeService)
        {
            _gameState = gameState;
            _saveService = saveService;
            _timeService = timeService;
        }

        public void Initialize()
        {
            SaveHandler = () => SaveData().Forget();
            _gameState.OnGameOver += SaveHandler;
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

        private async UniTask SaveData()
        {
            SaveConfig cloudData = await _cloudSaveService.LoadData();
            SaveConfig localData = _localSaveService.GetData();

            DateTime timeCloudData = _timeService.ConvertToDateTime(cloudData.SavingTime);
            DateTime timeLocalData = _timeService.ConvertToDateTime(localData.SavingTime);
            
            if (timeCloudData < timeLocalData)
            {
                
            }
            
            if (localData.ScoreRecord < CurrentScore)
            {
                localData.ScoreRecord = CurrentScore;
                _localSaveService.SetData(localData);
                await _cloudSaveService.SaveData(localData);
            }
        }
        
        private void GameExit()
        {
            _gameState.OnGameOver -= SaveHandler;
        }
    }
}