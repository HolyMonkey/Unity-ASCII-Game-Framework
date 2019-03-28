using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Cell : MonoBehaviour
{
    public TextMeshPro Symbol;

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
