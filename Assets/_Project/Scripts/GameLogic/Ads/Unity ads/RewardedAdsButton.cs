using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace GameLogic.Ads.Unity_ads
{
    public class RewardedAdsButton : IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private string _androidAdUnitId = "Rewarded_Android";
        private string _iOSAdUnitId = "Rewarded_iOS";
        private string _adUnitId = null;

        private UniTaskCompletionSource<bool> _completionSource;

        public void Initialize()
        {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
            _adUnitId = _androidAdUnitId;
#else
        _adUnitId = "unsupported_platform";
        Debug.LogWarning("Unsupported platform for ads");
#endif
        }

        public void LoadAd()
        {
            if (string.IsNullOrEmpty(_adUnitId))
            {
                Debug.LogError("Ad Unit ID is not set");
                return;
            }

            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        public async UniTask<bool> ShowAd()
        {
            if (string.IsNullOrEmpty(_adUnitId))
            {
                Debug.LogError("Ad Unit ID is not set");
                return false;
            }

            _completionSource = new UniTaskCompletionSource<bool>();
            Advertisement.Show(_adUnitId, this);
            return await _completionSource.Task;
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            throw new System.NotImplementedException();
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId))
            {
                if (_completionSource != null)
                {
                    if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
                    {
                        Debug.Log("Unity Ads Rewarded Ad Completed");
                        _completionSource.TrySetResult(true);
                    }
                    else
                    {
                        Debug.Log("Unity Ads Rewarded AD was not watched to the end");
                        _completionSource.TrySetResult(false);
                    }
                }
            }
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            _completionSource?.TrySetResult(false);
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            throw new System.NotImplementedException();
        }

        public void OnDestroy()
        {
            _completionSource?.TrySetResult(false);
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            throw new System.NotImplementedException();
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            throw new System.NotImplementedException();
        }
    }
}