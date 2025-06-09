namespace _Project.Scripts.GameLogic.Services
{
    public interface IKillService
    {
        public void AddMissile(int missile);
        public void AddLaser(int laser);
        public void AddAsteroid(int asteroid);
        public void AddUFO(int ufo);


    }
}