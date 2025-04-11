using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    public void ButtonForward();
    public void ButtonLeft(); 
    public void ButtonRight();
    public void ButtonShotingMissile();
    public void ButtonShotingLaser();
}