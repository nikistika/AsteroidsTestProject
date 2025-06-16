using UnityEngine;

namespace _Project.Scripts.InputSystem
{
    public class InputKeyboard : MonoBehaviour, IInput
    {
        public bool IsPressedForwardButton()
        {
            return Input.GetKey(KeyCode.W);
        }

        public bool IsPressedLeftButton()
        {
            return Input.GetKey(KeyCode.A);
        }

        public bool IsPressedRightButton()
        {
            return Input.GetKey(KeyCode.D);
        }

        public bool IsPressedMissileShootButton()
        {
            return Input.GetKey(KeyCode.Space);
        }

        public bool IsPressedLaserShootButton()
        {
            return Input.GetKey(KeyCode.G);
        }
    }
}