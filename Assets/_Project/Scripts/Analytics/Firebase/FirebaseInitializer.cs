using Cysharp.Threading.Tasks;
using Firebase;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Analytics.Firebase
{
    public class FirebaseInitializer : IInitializable
    {
        public async void Initialize()
        {
            await StartWork();
        }

        public async UniTask StartWork()
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