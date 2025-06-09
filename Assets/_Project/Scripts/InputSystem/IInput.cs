namespace _Project.Scripts.InputSystem
{
    public interface IInput
    {
        public bool ButtonForward();
        public bool ButtonLeft();
        public bool ButtonRight();
        public bool ButtonShootingMissile();
        public bool ButtonShootingLaser();
    }
}