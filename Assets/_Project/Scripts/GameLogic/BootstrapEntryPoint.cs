using GameLogic.Analytics;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameLogic
{
    public class BootstrapEntryPoint : IInitializable
    {
        private readonly FirebaseInitializer _firebaseInitializer;
        private readonly AdsInitializer _adsInitializer;

        public BootstrapEntryPoint(
             FirebaseInitializer firebaseInitializer,
             AdsInitializer adsInitializer)
        {
            _firebaseInitializer = firebaseInitializer;
            _adsInitializer = adsInitializer;
        }
        
        public async void Initialize()
        {
            SceneManager.LoadScene(1);
        }
    }
}