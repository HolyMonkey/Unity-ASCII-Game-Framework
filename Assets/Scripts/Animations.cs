using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations
{
    private Grid _grid;
    private Cell _cell;

    private Transit _transit;
    private StretchFigure _stretchFigure;
    private Fade _fade;

    private Animation _animations;

    public Animations(Grid grid)
    {
        _grid = grid;
    }

    public Animations(Cell cell)
    {
        _cell = cell;
    }

    public Animation Transit(int fromX, int fromY, int toX, int toY)
    {
        _animations = new Transit(_grid, new Vector2Int(fromX, fromY), new Vector2Int(toX, toY));
        return _animations;
    }

    public Animation FadeIn()
    {
        _animations = new Fade(_cell, true);
        return _animations;
    }

    public Animation FadeOut()
    {
        _animations = new Fade(_cell, false);
        return _animations;
    }

    public Animation StretchFigure()
    {
        _animations = new StretchFigure(_cell);
        return _animations;
    }

    public void Update()
    {
        _animations?.Update();
    }
}