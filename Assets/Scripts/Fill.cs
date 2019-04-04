using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class Fill
{
    private Grid _currentGrid;

    public Fill(Grid grid)
    {
        _currentGrid = grid;
    }

    public void Rect(int x, int y, int width, int height, char symbol, Color color)
    {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                _currentGrid.Write(x + i, y - j, symbol, color);
    }

}
