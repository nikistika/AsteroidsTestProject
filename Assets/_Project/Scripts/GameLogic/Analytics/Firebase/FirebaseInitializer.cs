using Cysharp.Threading.Tasks;
using Firebase;
using UnityEngine;

namespace GameLogic.Analytics
{
    public class FirebaseInitializer
    {
        public async UniTask Initialize()
        {
            var status = await FirebaseApp.CheckAndFixDependenciesAsync();
            if (status != DependencyStatus.Available)
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {status}");
                return;
            }

            Debug.Log("Firebase initialized successfully!");
        }
    }
}