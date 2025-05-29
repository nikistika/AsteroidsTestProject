using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace GameLogic.Ads.Unity_ads
{
    public class AdsInitializer : IUnityAdsInitializationListener, IInitializable
    {
        private string _androidGameId = "5855675";
        private string _iOSGameId = "5855674";
        private bool _testMode = true;
        private string _gameId;

        private UniTaskCompletionSource<bool> _initializationComplete;

        public async void Initialize()
        {
            await StartWork();
        }

        public async UniTask StartWork()
        {
            _initializationComplete = new UniTaskCompletionSource<bool>();

#if UNITY_IOS
        _gameId = _iOSGameId;
#elif UNITY_ANDROID
            _gameId = _androidGameId;
#elif UNITY_EDITOR
        _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif

            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _testMode, this);
                await _initializationComplete.Task;
            }
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads Initializer Ad Completed");
            _initializationComplete.TrySetResult(true);
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log("Unity Ads Initializer AD was not watched to the end");
            _initializationComplete.TrySetResult(false);
        }
    }
}