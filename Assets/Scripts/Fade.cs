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
            _opacity = 0;
        }
        else
        {
            _fadeOut = true;
            _opacity = 1f;
        }
    }

    public override Animation Duration(float seconds)
    {
        _duration = seconds;
        _steps = _duration / 20;
        return this;
    }

    public override void Start()
    {
        IsAnimating = true;
    }

    void Fading(ref bool fade, float dirOpacity)
    {
        if (fade == true)
        {
            if (_opacity <= 1 || _opacity >= 0)
            {
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= _steps)
                {
                    _color = _cell.Color;
                    _color.a = _opacity;
                    _cell.Color = _color;
                    _opacity += dirOpacity;
                    _elapsedTime = 0;
                }
            }
            else
            {
                fade = false;
                IsAnimating = false;
            }
        }
    }

    public override void Update()
    {
        Fading(ref _fadeIn, 0.05f);
        Fading(ref _fadeOut, -0.05f);
    }
}