using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyboard : MonoBehaviour, IInput
{

    public bool ButtonForward()
    {
        return UnityEngine.Input.GetKey(KeyCode.W);
    }
    public bool ButtonLeft()
    {
        return UnityEngine.Input.GetKey(KeyCode.A);
    }
    public void ButtonRight()
    {
        return UnityEngine.Input.GetKey(KeyCode.D);
    }
    public bool ButtonShotingMissile()
    {
        return UnityEngine.Input.GetKey(KeyCode.Space);
    }
    public bool ButtonShotingLaser()
    {
        return UnityEngine.Input.GetKey(KeyCode.G);
    }

}
