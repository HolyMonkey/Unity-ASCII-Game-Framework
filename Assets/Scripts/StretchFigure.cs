using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchFigure : Animation
{
    private Cell _cell;
    private Vector2 _cellScale;

    private int _counter = 1;
    private float _steps;
    private float _duration = 1;
    private float _elapsedTime = 0;

    public StretchFigure(Cell cell)
    {
        _cell = cell;
        IsAnimating = true;
    }

    public override Animation Duration(float seconds)
    {
        _duration = seconds;
        return this;
    }

    public override void Start()
    {
        if (IsAnimating == true)
        {
            _steps = _duration / 20;
            _cellScale = new Vector2(_cell.Symbol.rectTransform.sizeDelta.x, _cell.Symbol.rectTransform.sizeDelta.y);
        }
    }

    public override void Update()
    {
        if (IsAnimating == true)
        {
            if (_counter <= 20)
            {
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= _steps * _counter)
                {
                    if (_counter < 10)
                    {
                        _cell.Symbol.rectTransform.sizeDelta = new Vector2(_cellScale.x + _cellScale.x * (2 * _counter) / 100, _cellScale.y + _cellScale.y * (2 * _counter) / 100);
                    }
                    else
                    {
                        _cell.Symbol.rectTransform.sizeDelta = new Vector2(_cellScale.x + _cellScale.x * (2 * (20 - _counter)) / 100, _cellScale.y + _cellScale.y * (2 * (20 - _counter)) / 100);
                    }

                    _counter++;
                }
            }
            else
            {
                IsAnimating = false;
            }
        }
    }
}