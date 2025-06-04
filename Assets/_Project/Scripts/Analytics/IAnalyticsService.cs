namespace GameLogic.Analytics
{
    public interface IAnalyticsService
    {
        public void Initialize();
        public void StartGameEvent();
        public void GameOverEvent(int numberMissile, int numberLaser, int numberDestroyedAsteroids,
            int numberDestroyedUFO);
        public void UsingLaserEvent();
    }
}