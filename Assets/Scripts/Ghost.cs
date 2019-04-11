using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ghost : Moveable
{
    private char _currentSkin = '@';
    public override char GetSkin() => _currentSkin;

    public event UnityAction<Ghost> OnGhostMove;

    public Ghost(Vector2Int position, Vector2Int direction, Color color) : base(position, direction, color)
    {

    }

    public override void Move()
    {
        if (IsOnMoveEmpty())
            OnMove += MakeMove;
        base.Move();
    }

    private void MakeMove()
    {
        OnGhostMove(this);
    }
}
