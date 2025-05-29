using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

public class InterstitialAd : IUnityAdsLoadListener, IUnityAdsShowListener, IInitializable
{
    private string _androidAdUnitId = "Interstitial_Android";
    private string _iOSAdUnitId = "Interstitial_iOS";
    private string _adUnitId;

    private UniTaskCompletionSource<bool> _completionSource;

    public void Initialize()
    {
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public async UniTask ShowAd()
    {
        Debug.Log("Showing Ad: " + _adUnitId);
        _completionSource = new UniTaskCompletionSource<bool>();
        Advertisement.Show(_adUnitId, this);
        await _completionSource.Task;
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId))
        {
            if (_completionSource != null)
            {
                if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
                {
                    Debug.Log("Unity Ads Interstitial Ad Completed");
                    _completionSource.TrySetResult(true);
                }
                else
                {
                    Debug.Log("Unity Ads Interstitial AD was not watched to the end");
                    _completionSource.TrySetResult(false);
                }
            }
        }
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log($"The ad was uploaded successfully: {adUnitId}");
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string _adUnitId)
    {
        Debug.Log($"The ad display has started: {_adUnitId}");
    }

    public void OnUnityAdsShowClick(string _adUnitId)
    {
        Debug.Log($"The user clicked on the advertisement: {_adUnitId}");
    }
}