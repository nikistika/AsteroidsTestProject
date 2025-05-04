using Zenject;

namespace GameLogic.Analytics
{
    public class AnalyticsController : IInitializable
    {
        private IAnalytics _analytics;
        
        public void Initialize()
        {
            _analytics = new FirebaseAnalyticsService();
        }
        
        public void StartGameEvent()
        {
            _analytics.StartGameEvent();
        }

        public void GameOverEvent(int numberMissile, int numberLaser, int numberDestroyedAsteroids, int numberDestroyedUFO)
        {
            _analytics.GameOverEvent(numberMissile, numberLaser, numberDestroyedAsteroids, numberDestroyedUFO);
        }

        public void UsingLaserEvent()
        {
            _analytics.UsingLaserEvent();
        }
    }
}