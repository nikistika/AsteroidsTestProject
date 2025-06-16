using _Project.Scripts.Addressable;
using _Project.Scripts.GameLogic;
using _Project.Scripts.GameLogic.Services.Spawners;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Audio
{
    public class AudioControllerSpawner : BaseSpawner<AudioController>   
    {
        private readonly IAssetLoader _assetLoader;
        
        private AudioController _audioControllerPrefab;

        public IAudio AudioControllerObject { get; private set; }
        
        public AudioControllerSpawner(
            GameState gameState, 
            ScreenSize screenSize,
            IAssetLoader assetLoader) : 
            base(gameState, screenSize)
        {
            _assetLoader = assetLoader;
        }

        protected override async UniTask Initialize()
        {
            await GetPrefab();
            AudioControllerObject = await SpawnObject();
        }

        private UniTask<AudioController> SpawnObject()
        {
            var audioControllerObject = Object.Instantiate(_audioControllerPrefab);
            audioControllerObject.gameObject.SetActive(true);
            return UniTask.FromResult(audioControllerObject);
        }
        
        private async UniTask GetPrefab()
        {
            _audioControllerPrefab = await _assetLoader.CreateAudioSources();
        }

        protected override UniTask GameContinue()
        {
            return UniTask.CompletedTask;
        }
    }
}