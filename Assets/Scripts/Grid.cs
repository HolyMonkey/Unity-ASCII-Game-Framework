using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grid : MonoBehaviour
{
    [Tooltip ("Add gird with GridCamera component")]
    public GridCamera Camera;

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

    public void UpdateView(int X, int Y)
    {
        GameObject cellObject = new GameObject
        {
            name = "Cell"
        };

        _grid = new GameObject[X,Y];

        _cellScale.x = 1 / _cellSize.x;
        _cellScale.y = 1 / _cellSize.y;

        cellObject.transform.localScale = new Vector2(_cellScale.x, _cellScale.y);

        _gridOffset.x = - (X / 2) * _cellSize.x + _cellSize.x / 2;
        _gridOffset.y = (Y / 2) * _cellSize.y - _cellSize.y / 2;

        for (int row = 0; row < Y; row++)
        {
            for (int col = 0; col < X; col++)
            {
                Vector2 pos = new Vector2(col * _cellSize.x + _gridOffset.x, -row * _cellSize.y + _gridOffset.y);

                _grid[col, row] = Instantiate(cellObject, pos, Quaternion.identity) as GameObject;
                _grid[col, row].transform.parent = gameObject.transform;
            }
        }
        
        Destroy(cellObject);
    }

    public void Write(int X, int Y, char symbol, Color color)
    {
        if(_grid[X, Y].transform.childCount > 0)
            Destroy(_grid[X,Y].transform.GetChild(0).gameObject);

        GameObject cellSymbol = new GameObject
        {
            name = symbol.ToString()
        };

        cellSymbol.AddComponent<TextMeshPro>().text = symbol.ToString();
        TextMeshPro tmp = cellSymbol.GetComponent<TextMeshPro>();

        tmp.rectTransform.sizeDelta = new Vector2(_cellSize.x,_cellSize.y*1.5f);

        cellSymbol.transform.SetParent(_grid[X, Y].transform);
        cellSymbol.transform.localPosition = Vector2.zero;

        tmp.enableAutoSizing = true;
        tmp.color = color;
        tmp.fontSizeMin = 0f;
        tmp.alignment = TextAlignmentOptions.Center;
    }

    public bool CheckForSymbol(int X, int Y, char symbol)
    {
        if (_grid[X, Y].transform.Find(symbol.ToString()))
            return true;
        else
            return false;
    }
}