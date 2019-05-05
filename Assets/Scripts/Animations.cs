using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations
{
    private Grid _grid;
    private Cell _cell;

    private Animation _animation;
    private static Queue<Animation> _queue = new Queue<Animation>();
    private static Animation _currentAnimation;

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
        _animation = new Transit(_grid, new Vector2Int(fromX, fromY), new Vector2Int(toX, toY));
        _queue.Enqueue(_animation);
        return _animation;
    }

    public Animation FadeIn()
    {
        _animation = new Fade(_cell, true);
        _queue.Enqueue(_animation);
        return _animation;
    }

    public Animation FadeOut()
    {
        _animation = new Fade(_cell, false);
        _queue.Enqueue(_animation);
        return _animation;
    }

    public Animation StretchFigure()
    {
        _animation = new StretchFigure(_cell);
        _queue.Enqueue(_animation);
        return _animation;
    }

    public void Update()
    {
        if (_queue.Count != 0 && _currentAnimation == null)
        {
            _currentAnimation = _queue.Dequeue();
        }

        if (_currentAnimation != null)
        {
            if (_currentAnimation.IsAnimating == true)
            {
                _currentAnimation.Update();
            }
            else
            {
                _currentAnimation = null;
            }
        }
    }
}