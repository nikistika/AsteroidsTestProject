using GameLogic.Analytics;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameLogic
{
    public class BootstrapEntryPoint : IInitializable
    {
        private readonly FirebaseInitializer _firebaseInitializer;
        private readonly AdsInitializer _adsInitializer;
        
        public void Initialize()
        {
            SceneManager.LoadScene(1);
        }
    }
}