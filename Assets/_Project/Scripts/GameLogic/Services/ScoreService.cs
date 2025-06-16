using System;
using _Project.Scripts.Save;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.GameLogic.Services
{
    public class ScoreService : IInitializable, IScoreService
    {
        public event Action<int> OnScoreChanged;
        private event Action SaveHandler;


        public int CurrentScore { get; private set; }

        private readonly GameState _gameState;
        private readonly ISaveService _saveService;

        private bool _startFlag;

        public ScoreService(
            GameState gameState,
            ISaveService saveService)
        {
            _gameState = gameState;
            _saveService = saveService;
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
            SaveData data = await _saveService.GetData();
            var dataScore = data.ScoreRecord;
            if (dataScore < CurrentScore)
            {
                data.ScoreRecord = CurrentScore;
                await _saveService.SaveData(data);
            }
        }

        private void GameExit()
        {
            _gameState.OnGameOver -= SaveHandler;
        }
    }
}