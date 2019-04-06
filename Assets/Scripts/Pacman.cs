using System;
using System.Collections;
using System.Collections.Generic;

class Pacman
{
    public int X { get; private set; }
    public int Y { get; private set; }

    private int _xDir;
    private int _yDir;

    private char[] _openedVerticalStates = { '˅', '˄' };
    private char[] _openedHorizontalStates = { '>', '<' };
    private char _verticalAnimClosed = 'ǁ';
    private char _horizontalAnimClosed = '=';
    private bool _lastStateWasClosed = true;

    public Pacman(int x, int y, int xDir, int yDir)
    {
        X = x;
        Y = y;
        _xDir = xDir;
        _yDir = yDir;
    }

    public void Move()
    {
        X += _xDir;
        Y += _yDir;
        Animation();
    }

    public void TurnLeft()
    {
        _xDir = -1;
        _yDir = 0;
    }

    public void TurnRight()
    {
        _xDir = 1;
        _yDir = 0;
    }

    public void TurnUp()
    {
        _xDir = 0;
        _yDir = -1;
    }

    public void TurnDown()
    {
        _xDir = 0;
        _yDir = 1;
    }

    public char GetCurrentState()
    {
        if(_lastStateWasClosed)
        {
            if (_yDir != 0)
                return _openedVerticalStates[_yDir < 0 ? 0 : 1];
            else
                return _openedHorizontalStates[_xDir < 0 ? 0 : 1];
        }
        else
        {
            if (_yDir != 0)
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
