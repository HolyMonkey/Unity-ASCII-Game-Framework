using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationProperties
{
    IAnimationProperties Duration(float seconds);
    void Start();
}
