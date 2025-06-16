namespace _Project.Scripts.InputSystem
{
    public interface IInput
    {
        public bool IsPressedForwardButton();
        public bool IsPressedLeftButton();
        public bool IsPressedRightButton();
        public bool IsPressedMissileShootButton();
        public bool IsPressedLaserShootButton();
    }
}