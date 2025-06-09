using _Project.Scripts.Analytics.Firebase;
using Zenject;

namespace _Project.Scripts.Analytics
{
    public class AnalyticsService : IInitializable, IAnalyticsService
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

        public void GameOverEvent(int numberMissile, int numberLaser, int numberDestroyedAsteroids,
            int numberDestroyedUFO)
        {
            _analytics.GameOverEvent(numberMissile, numberLaser, numberDestroyedAsteroids, numberDestroyedUFO);
        }

        public void UsingLaserEvent()
        {
            _analytics.UsingLaserEvent();
        }
    }
}