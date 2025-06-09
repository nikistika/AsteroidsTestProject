using Firebase.Analytics;

namespace _Project.Scripts.Analytics.Firebase
{
    public class FirebaseAnalyticsService : IAnalytics
    {
        public void StartGameEvent()
        {
            FirebaseAnalytics.LogEvent("game_start", new Parameter("Start", "StartGame"));
        }

        public void GameOverEvent(int numberMissile, int numberLaser, int numberDestroyedAsteroids,
            int numberDestroyedUFO)
        {
            FirebaseAnalytics.LogEvent(
                "game_over",
                new("number_missile", numberMissile),
                new("number_laser", numberLaser),
                new("number_destroyed_asteroids", numberDestroyedAsteroids),
                new("number_destroyed_ufo", numberDestroyedUFO));
        }

        public void UsingLaserEvent()
        {
            FirebaseAnalytics.LogEvent("laser_used", new Parameter("Laser", "UsingLaser"));
        }
    }
}