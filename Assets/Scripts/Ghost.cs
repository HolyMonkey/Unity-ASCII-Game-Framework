using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Creature
{
    private char _currentSkin = '@';
    public override char GetSkin() => _currentSkin;

    public Ghost(int x, int y, int xDir, int yDir, Color color) : base(x, y, xDir, yDir, color)
    {

    }  
}
