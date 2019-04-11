using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : Moveable
{
    private char[] _openedVerticalStates = { 'u', 'n' };
    private char[] _openedHorizontalStates = { ')', '(' };
    private char _verticalAnimClosed = 'o';
    private char _horizontalAnimClosed = 'O';
    private bool _lastStateWasClosed = true;

    public Pacman(Vector2Int position, Vector2Int direction, Color color) : base(position, direction, color)
    {

    }

    protected override void ChangePosition()
    {
        base.ChangePosition();
        Animation();
    }

    public override char GetSkin()
    {
        if(_lastStateWasClosed)
            return Direction.y != 0 ? _openedVerticalStates[Direction.y < 0 ? 0 : 1] : _openedHorizontalStates[Direction.x < 0 ? 0 : 1];           
        else
            return Direction.y != 0 ? _verticalAnimClosed : _horizontalAnimClosed;
    }

    public void Animation()
    {
        _lastStateWasClosed = !_lastStateWasClosed;
    }
}
