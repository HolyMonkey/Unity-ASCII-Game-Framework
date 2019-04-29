using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchFigure : IAnimationProperties
{
    private Cell _cell;
    private Vector2 _cellScale;

    private bool _isStretching = false;

    private int _counter = 1;
    private float _steps;
    private float _duration = 1;
    private float _elapsedTime = 0;


    public StretchFigure(Cell cell)
    {
        _cell = cell;
        _isStretching = true;
    }

    public IAnimationProperties Duration(float seconds)
    {
        this._duration = seconds;
        return this;
    }

    public void Start()
    {
        if (_isStretching == true)
        {
            _steps = _duration / 10;
            _cellScale = new Vector2(_cell.Symbol.rectTransform.sizeDelta.x, _cell.Symbol.rectTransform.sizeDelta.y);
        }
    }


    public void Update()
    {
        if (_isStretching == true)
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
                _isStretching = false;
        }
    }
}
