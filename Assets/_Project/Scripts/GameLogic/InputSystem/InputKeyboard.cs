using UnityEngine;

namespace InputSystem
{
    public class InputKeyboard : MonoBehaviour, IInput
    {
        public bool ButtonForward()
        {
            return Input.GetKey(KeyCode.W);
        }

        public bool ButtonLeft()
        {
            return Input.GetKey(KeyCode.A);
        }

        public bool ButtonRight()
        {
            return Input.GetKey(KeyCode.D);
        }

        public bool ButtonShootingMissile()
        {
            return Input.GetKey(KeyCode.Space);
        }

        public bool ButtonShootingLaser()
        {
            return Input.GetKey(KeyCode.G);
        }
    }
}