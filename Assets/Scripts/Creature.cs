using System;
using System.Collections;
using System.Collections.Generic;

public class Creature
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public int XDir { get; private set; }
    public int YDir { get; private set; }

    public Creature(int x, int y, int xDir, int yDir)
    {
        X = x;
        Y = y;
        XDir = xDir;
        YDir = yDir;
    }

    public virtual void Move()
    {
        X += XDir;
        Y += YDir;
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
}
