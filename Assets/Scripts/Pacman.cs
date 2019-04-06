using System;
using System.Collections;
using System.Collections.Generic;

class Pacman
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public int XDir { get; private set; }
    public int YDir { get; private set; }

    private char[] _openedVerticalStates = { '˅', '˄' };
    private char[] _openedHorizontalStates = { '>', '<' };
    private char _verticalAnimClosed = 'ǁ';
    private char _horizontalAnimClosed = '=';
    private bool _lastStateWasClosed = true;

    public Pacman(int x, int y, int xDir, int yDir)
    {
        X = x;
        Y = y;
        XDir = xDir;
        YDir = yDir;
    }

    public void Move()
    {
        X += XDir;
        Y += YDir;
        Animation();
    }

    public void TurnLeft()
    {
        XDir = -1;
        YDir = 0;
    }

    public void TurnRight()
    {
        XDir = 1;
        YDir = 0;
    }

    public void TurnUp()
    {
        XDir = 0;
        YDir = -1;
    }

    public void TurnDown()
    {
        XDir = 0;
        YDir = 1;
    }

    public char GetCurrentState()
    {
        if(_lastStateWasClosed)
        {
            if (YDir != 0)
                return _openedVerticalStates[YDir < 0 ? 0 : 1];
            else
                return _openedHorizontalStates[XDir < 0 ? 0 : 1];
        }
        else
        {
            if (YDir != 0)
                return _verticalAnimClosed;
            else
                return _horizontalAnimClosed;
        }
    }

    private void Animation()
    {
        _lastStateWasClosed = !_lastStateWasClosed;
    }
}
