using GameLogic.Analytics;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameLogic
{
    public class BootstrapEntryPoint : IInitializable
    {
        private readonly FirebaseInitializer _firebaseInitializer;

        public BootstrapEntryPoint(
             FirebaseInitializer firebaseInitializer)
        {
            _firebaseInitializer = firebaseInitializer;
        }
        
        public async void Initialize()
        {
            await _firebaseInitializer.Initialize();
            SceneManager.LoadScene(1);
        }
    }
}