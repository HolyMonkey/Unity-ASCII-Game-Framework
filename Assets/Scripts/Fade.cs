using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : Animation
{
    private Cell _cell;
    private Color _color;

    private bool _fadeIn = false;
    private bool _fadeOut = false;

    private float _duration = 1;
    private float _steps;
    private float _opacity;
    private float _elapsedTime = 0;

    public Fade(Cell cell, bool fadingIn)
    {
        _cell = cell;

        if (fadingIn == true)
        {
            _fadeIn = true;
        }
        else
        {
            _fadeOut = true;
        }
    }

    public override Animation Duration(float seconds)
    {
        this._duration = seconds;
        return this;
    }

    public override void Start()
    {
        if (_fadeIn == true)
        {
            _opacity = 0;
            _steps = _duration / 10;
        }

        if (_fadeOut == true)
        {
            _steps = _duration / 10;
            _opacity = 1f;
        }
    }

    public override void Update()
    {
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
    }
}