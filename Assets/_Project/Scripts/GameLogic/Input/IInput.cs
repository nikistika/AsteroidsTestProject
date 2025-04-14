using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    public bool ButtonForward();
    public bool ButtonLeft(); 
    public bool ButtonRight();
    public bool ButtonShootingMissile();
    public bool ButtonShootingLaser();
}