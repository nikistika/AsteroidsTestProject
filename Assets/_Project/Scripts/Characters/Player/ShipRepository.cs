using _Project.Scripts.GameLogic.Shootnig;

namespace _Project.Scripts.Characters.Player
{
    public class ShipRepository
    {
        public SpaceShip SpaceShip { get; private set; }
        public ShootingLaser ShootingLaser { get; private set; }
        public DataSpaceShip DataSpaceShip { get; private set; }

        public void GetSpaceShip(
            SpaceShip spaceShip,
            ShootingLaser shootingLaser,
            DataSpaceShip dataSpaceShip)
        {
            SpaceShip = spaceShip;
            ShootingLaser = shootingLaser;
            DataSpaceShip = dataSpaceShip;
        }
    }
}