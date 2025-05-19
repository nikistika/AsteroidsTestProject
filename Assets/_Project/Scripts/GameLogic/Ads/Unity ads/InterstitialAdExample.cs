using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace GameLogic.Ads.Unity_ads
{
    public class InterstitialAdExample : IUnityAdsLoadListener, IUnityAdsShowListener, IInitializable
    {
        private string _androidAdUnitId = "Interstitial_Android";
        private string _iOSAdUnitId = "Interstitial_iOS";
        private string _adUnitId;
        
        private UniTaskCompletionSource<bool> _completionSource;
        
        public void Initialize()
        {
            // Get the Ad Unit ID for the current platform:
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOSAdUnitId
                : _androidAdUnitId;
        }

        // Load content to the Ad Unit:
        public void LoadAd()
        {
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        // Show the loaded content in the Ad Unit:
        public async UniTask<bool> ShowAd()
        {
            // Note that if the ad content wasn't previously loaded, this method will fail
            Debug.Log("Showing Ad: " + _adUnitId);
            _completionSource = new UniTaskCompletionSource<bool>();
            Advertisement.Show(_adUnitId, this);
            return await _completionSource.Task;
        }

        // Implement Load Listener and Show Listener interface methods: 
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            // Optionally execute code if the Ad Unit successfully loads content.
        }

        public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        }

        public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        }

        public void OnUnityAdsShowStart(string _adUnitId)
        {
        }

        public void OnUnityAdsShowClick(string _adUnitId)
        {
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
    }
}