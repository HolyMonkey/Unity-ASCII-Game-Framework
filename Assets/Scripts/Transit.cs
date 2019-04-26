using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transit : Animation
{
    private Grid _grid;

    private bool _transiting = false;
    private Vector2Int _fromPoint, _toPoint;

    private int _step;
    private int _dx, _dy, _x, _y, _ystep, _xstep;
    private int _counter = 1;

    private float _steps;
    private float _duration = 1;
    private float _elapsedTime = 0;

    private string _symbol;
    private Color _color;

    public Transit(Grid grid, Vector2Int from, Vector2Int to)
    {
        _grid = grid;
        _transiting = true;
        _fromPoint = from;
        _toPoint = to;
    }

    public override Animation Duration(float seconds)
    {
        this._duration = seconds;
        return this;
    }

    public override void Start()
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
    }

    public override void Update()
    {
        if (_transiting == true)
        {
            if (_x != _toPoint.x || _y != _toPoint.y)
            {
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= _steps)
                {
                    _grid.Write(_x, _y, ' ');

                    if (Mathf.Abs(_toPoint.y - _fromPoint.y) > Mathf.Abs(_toPoint.x - _fromPoint.x))
                    {
                        _y += _ystep;

                        if (_step * Mathf.Abs(_toPoint.x - _fromPoint.x) / Mathf.Abs(_toPoint.y - _fromPoint.y) >= _counter)
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

    }
}
