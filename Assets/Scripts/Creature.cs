using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public abstract class Creature
{
    public Vector2Int Position { get; private set; }
    public Vector2Int Direction { get; private set; }

    public Color CurrentColor { get; set; }

    public event Action OnMove;

    public Creature(int x, int y, int xDir, int yDir, Color color)
    {
        Position = new Vector2Int(x, y);
        Direction = new Vector2Int(xDir, yDir);
        CurrentColor = color;
    }

    public virtual void Move()
    {
        Position = new Vector2Int(Position.x + Direction.x, Position.y + Direction.y);
        OnMove?.Invoke();
    }

    public void TurnLeft()
    {
        Direction = Vector2Int.left;
    }

    public void TurnRight()
    {
        Direction = Vector2Int.right;
    }

    public void TurnUp()
    {
        Direction = Vector2Int.down;
    }

    public void TurnDown()
    {
        Direction = Vector2Int.up;
    }

    public abstract char GetSkin();
}
