using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;
 
public class AdsInitializer :  IUnityAdsInitializationListener
{
    private string _androidGameId = "5855675";
    private string _iOSGameId = "5855674";
    private bool _testMode = true;
    private string _gameId;
 
    private TaskCompletionSource<bool> _initializationComplete;
    
 
    public async UniTask<bool> Initialize()
    {
        _initializationComplete = new TaskCompletionSource<bool>();
        
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
            return await _initializationComplete.Task;
        }
        
        return true;
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}