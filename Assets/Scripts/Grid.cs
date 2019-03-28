using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Tooltip ("Add gird with GridCamera component")]
    public GridCamera Camera;
    public Cell CellTemplate;

    private Vector2 _gridOffset;
    private Vector2 _cellSize = new Vector2(1,1);
    private Vector2 _cellScale;
    private Cell [,] _grid;

    public float CellSize
    {
        get { return _cellSize.x; }
    }

    public void SetCellSize(float cellSize)
    {
        _cellSize = new Vector2(cellSize,cellSize);
    }

    public void Reset(int x, int y)
    {
        _grid = new Cell[x,y];

        _cellScale.x = 1 / _cellSize.x;
        _cellScale.y = 1 / _cellSize.y;

        CellTemplate.transform.localScale = new Vector2(_cellScale.x, _cellScale.y);

        _gridOffset.x = - (x / 2) * _cellSize.x + _cellSize.x / 2;
        _gridOffset.y = (y / 2) * _cellSize.y - _cellSize.y / 2;

        for (int row = 0; row < y; row++)
        {
            for (int col = 0; col < x; col++)
            {
                Vector2 pos = new Vector2(col * _cellSize.x + _gridOffset.x, -row * _cellSize.y + _gridOffset.y);

                _grid[col, row] = Instantiate(CellTemplate, pos, Quaternion.identity) as Cell;
                _grid[col, row].transform.parent = gameObject.transform;
            }
        }
    }

    public void Write(int x, int y, char symbol, Color? color = null)
    {
        _grid[x,y].Text = symbol.ToString();
        _grid[x,y].Color = color ?? Color.white;
    }

    public bool HasSymbol(int x, int y, char symbol)
    {
        if (_grid[x, y].Text == symbol.ToString())
            return true;
        else
            return false;
    }
}