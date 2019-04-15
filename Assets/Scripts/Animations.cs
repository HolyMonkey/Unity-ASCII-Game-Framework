using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animations : IAnimations, IAnimationProperties
{ 
    private Grid _grid;
    private Cell _cell;
    private Vector2Int _fromPoint, _toPoint;
    private Vector2 _cellScale;
    private float _duration = 1;

    private bool _transiting = false;
    private bool _fadeIn = false;
    private bool _fadeOut = false;
    private bool _stretching = false;

    private int _step;
    private int _dx, _dy, _x, _y, _ystep, _xstep;
    private int _counter = 1;
    
    private float _steps;
    private string _symbol;
    private Color _color;
    private float _opacity;
    
    private float _elapsedTime = 0;

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
        _transiting = true;
        _fromPoint = new Vector2Int(fromX, fromY);
        _toPoint = new Vector2Int(toX, toY);
        return this;
    }

    public IAnimationProperties Duration(float seconds)
    {
        _duration = seconds;
        return this;
    }

    public IAnimationProperties FadeIn()
    {
        _fadeIn = true;
        return this;
    }

    public IAnimationProperties FadeOut()
    {
        _fadeOut = true;
        return this;
    }

    public IAnimationProperties StretchFigure()
    {
        _stretching = true;
        return this;
    }

    public void Start()
    {
        if (_transiting == true)
        {
            _dy = Mathf.Abs(_toPoint.y - _fromPoint.y);
            _dx = Mathf.Abs(_toPoint.x - _fromPoint.x);

            _step = 1;
            _ystep = (_fromPoint.y <= _toPoint.y) ? 1 : -1;
            _xstep = (_fromPoint.x <= _toPoint.x) ? 1 : -1;

            _y = _fromPoint.y;
            _x = _fromPoint.x;
            _steps = _duration / Mathf.Max(Mathf.Abs(_toPoint.y - _fromPoint.y), Mathf.Abs(_toPoint.x - _fromPoint.x));

            _symbol = _grid.GetSymbol(_fromPoint.x, _fromPoint.y).Text;
            _color = _grid.GetSymbol(_fromPoint.x, _fromPoint.y).Color;
        }

        if(_fadeIn == true)
        {
            _opacity = 0;
            _steps =  _duration/10;
        }

        if (_fadeOut == true)
        {
            _steps = _duration/10;
            _opacity = 1f;
        }

        if(_stretching == true)
        {
            _steps = _duration / 10;
            _cellScale = new Vector2(_cell.Symbol.rectTransform.sizeDelta.x, _cell.Symbol.rectTransform.sizeDelta.y);
        }
    }
    
    public void Update()
    {
        if (_transiting == true)
        {
            if (_x != _toPoint.x  || _y != _toPoint.y)
            {
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= _steps)
                {
                     _grid.Write(_x, _y, ' ');

                    if(Mathf.Abs(_toPoint.y - _fromPoint.y) > Mathf.Abs(_toPoint.x - _fromPoint.x))
                    {
                        _y += _ystep;

                        if(_step * Mathf.Abs(_toPoint.x - _fromPoint.x) / Mathf.Abs(_toPoint.y - _fromPoint.y) >= _counter)
                        {
                            _counter++;
                            _x += _xstep;
                        }
                    }
                    else
                    {
                        _x += _xstep;

                        if (_step * Mathf.Abs(_toPoint.y - _fromPoint.y) / Mathf.Abs(_toPoint.x - _fromPoint.x) >= _counter)
                        {
                            _counter++;
                            _y += _ystep;
                        }
                    }

                    _step++;
                   
                    _grid.Write(_x, _y, _symbol[0], _color);
                    _elapsedTime = 0;
                }
            }
            else
                _transiting = false;
        }

        if (_fadeIn == true)
        {
            if (_opacity <= 1)
            {
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= _steps)
                {
                    _color = _cell.Color;
                    _color.a = _opacity;
                    _cell.Color = _color;
                    _opacity += 0.1f;
                    _elapsedTime = 0;
                }
            }
            else
                _fadeIn = false;
        }

        if (_fadeOut == true)
        {
            if (_opacity >= 0)
            {
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= _steps)
                {
                    _color = _cell.Color;
                    _color.a = _opacity;
                    _cell.Color = _color;
                    _opacity -= 0.1f;
                    _elapsedTime = 0;
                }
            }
            else
                _fadeOut = false;
        }

        if(_stretching == true)
        {
            if (_counter <= 10)
            {
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= _steps * _counter)
                {
                    if (_counter < 5)
                    {
                        _cell.Symbol.rectTransform.sizeDelta = new Vector2(_cellScale.x + _cellScale.x * (4 * _counter) / 100, _cellScale.y + _cellScale.y * (4 * _counter) / 100);
                    }
                    else
                    {
                        _cell.Symbol.rectTransform.sizeDelta = new Vector2(_cellScale.x + _cellScale.x * (4 * (10 - _counter)) / 100, _cellScale.y + _cellScale.y * (4 * (10 - _counter)) / 100);
                    }
                    _counter++;
                }
            }
            else
                _stretching = false;
        }
    }
}
