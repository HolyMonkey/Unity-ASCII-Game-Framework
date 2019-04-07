using System;
using System.Collections;
using System.Collections.Generic;

public class Ghost : Creature
{
    public char Skin { get; private set; } = '@';

    public Ghost(int x, int y, int xDir, int yDir) : base(x, y, xDir, yDir)
    {

    }
}
