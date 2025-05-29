namespace GameLogic.Analytics
{
    public interface IAnalytics
    {
        public void StartGameEvent();

        public void GameOverEvent(int numberMissile, int numberLaser, int numberDestroyedAsteroids,
            int numberDestroyedUFO);

        public void UsingLaserEvent();
    }
}