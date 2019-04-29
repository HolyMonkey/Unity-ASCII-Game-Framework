using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : IAnimations
{ 
    private Grid _grid;
    private Cell _cell;

    private Transit _transit;
    private StretchFigure _stretchFigure;
    private Fade _fade;

    public Animations(Grid grid)
    {
        _grid = grid;
    }

    public Animations(Cell cell)
    {
        _cell = cell;
    }
    
    public IAnimationProperties Transit(int fromX, int fromY, int toX, int toY)
    {
        _transit = new Transit(_grid, new Vector2Int(fromX, fromY), new Vector2Int(toX, toY));
        return _transit;
    }

    public IAnimationProperties FadeIn()
    {
        _fade = new Fade(_cell, true);
        return _fade;
    }

    public IAnimationProperties FadeOut()
    {
        _fade = new Fade(_cell, false);
        return _fade;
    }

    public IAnimationProperties StretchFigure()
    {
        _stretchFigure = new StretchFigure(_cell);
        return _stretchFigure;
    }

    public void Update()
    {
        if (_transit != null)
            _transit.Update();

        if (_fade != null)
            _fade.Update();

        if (_stretchFigure != null)
            _stretchFigure.Update();
    }
}
