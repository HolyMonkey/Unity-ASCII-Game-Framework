using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animation 
{
    public abstract Animation Duration(float seconds);

    public abstract void Start();

    public abstract void Update();

    public bool IsAnimating = false;
}
