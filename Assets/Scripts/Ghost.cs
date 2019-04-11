using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ghost : Moveable
{
    private char _currentSkin = '@';
    public override char GetSkin() => _currentSkin;

    public Ghost(Vector2Int position, Vector2Int direction, Color color) : base(position, direction, color)
    {

    }
}
