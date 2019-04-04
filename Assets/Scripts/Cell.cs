using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Cell : MonoBehaviour
{
    [SerializeField] private TextMeshPro Symbol;
    [SerializeField] private Color _backgroundColor;

    public Color BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            _backgroundColor = value;
        }
    }

    public string Text
    {
        get
        {
            return Symbol.text;
        } 
        set
        {
            Symbol.text = value;
        }
    }

    public Color Color
    {
        get
        {
            return Symbol.color;
        }
        set
        {
            Symbol.color = value;
        }
    }
}
