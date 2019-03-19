using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grid : MonoBehaviour
{
    [Tooltip ("Add gird with GridCamera component")]
    public GridCamera Camera;
    public GameObject CellObject;

    private Vector2 _gridOffset;
    private Vector2 _cellSize = new Vector2(1,1);
    private Vector2 _cellScale;
    private GameObject [,] _grid;

    public float CellSize
    {
        get { return _cellSize.x; }
    }

    public void SetCellSize(float cellSize)
    {
        _cellSize = new Vector2(cellSize,cellSize);
    }

    public void UpdateView(int x, int y)
    {
        _grid = new GameObject[x,y];

        _cellScale.x = 1 / _cellSize.x;
        _cellScale.y = 1 / _cellSize.y;

        CellObject.transform.localScale = new Vector2(_cellScale.x, _cellScale.y);

        _gridOffset.x = - (x / 2) * _cellSize.x + _cellSize.x / 2;
        _gridOffset.y = (y / 2) * _cellSize.y - _cellSize.y / 2;

        for (int row = 0; row < y; row++)
        {
            for (int col = 0; col < x; col++)
            {
                Vector2 pos = new Vector2(col * _cellSize.x + _gridOffset.x, -row * _cellSize.y + _gridOffset.y);

                _grid[col, row] = Instantiate(CellObject, pos, Quaternion.identity) as GameObject;
                _grid[col, row].transform.parent = gameObject.transform;
            }
        }
    }

    public void Write(int x, int y, char symbol, Color color)
    {
        _grid[x,y].GetComponentInChildren<TextMeshPro>().text = symbol.ToString();
        _grid[x,y].GetComponentInChildren<TextMeshPro>().color = color;
    }

    public bool HasSymbol(int x, int y, char symbol)
    {
        if (_grid[x, y].GetComponentInChildren<TextMeshPro>().text == symbol.ToString())
            return true;
        else
            return false;
    }
}