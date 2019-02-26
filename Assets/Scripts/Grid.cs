using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grid : MonoBehaviour
{
    [SerializeField] private Vector2 _gridSize;
    [SerializeField] private Sprite _cellSprite;

    private Vector2 _gridOffset;
    private Vector2 _cellSize;
    private Vector2 _cellScale;
    private TextMeshPro _tmpSymbol;
    private GameObject [,] grid;

    void Start()
    {
        InitCells();
    }

    void InitCells()
    {
        GameObject cellObject = new GameObject();

        grid = new GameObject[(int)_gridSize.x,(int) _gridSize.y];
        cellObject.AddComponent<SpriteRenderer>().sprite = _cellSprite;

        cellObject.name = "Sprite";

        _cellSize = _cellSprite.bounds.size;

        _cellScale.x = 1 / _cellSize.x;
        _cellScale.y = 1 / _cellSize.y;

        _cellSize = new Vector2(1, 1);

        cellObject.transform.localScale = new Vector2(_cellScale.x, _cellScale.y);

        
        _gridOffset.x = -(_gridSize.x / 2)+ _cellSize.x / 2;
        _gridOffset.y = (_gridSize.y / 2) - _cellSize.y / 2;

        for (int row = 0; row < _gridSize.y; row++)
        {
            for (int col = 0; col < _gridSize.x; col++)
            {
                Vector2 pos = new Vector2(col * _cellSize.x + _gridOffset.x + transform.position.x, -row* _cellSize.y + _gridOffset.y + transform.position.y);
                
                grid[col, row] = Instantiate(cellObject, pos, Quaternion.identity) as GameObject;
                grid[col, row].GetComponent<SpriteRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                
                grid[col, row].transform.parent = transform;
            }
        }

        Destroy(cellObject);
    }

    public void Write(int X, int Y, string symbol, Color color)
    {
        GameObject Symbol = new GameObject();
        Symbol.name = "Symbol";

        Symbol.AddComponent<TextMeshPro>().text = symbol;
        TextMeshPro tmp = Symbol.GetComponent<TextMeshPro>();

        tmp.rectTransform.sizeDelta= new Vector2(1,1.5f);

        Symbol.transform.SetParent(grid[X, Y].transform);
        Symbol.transform.localPosition= Vector2.zero;

        tmp.enableAutoSizing = true;
        tmp.color = color;
        tmp.fontSizeMin = 0f;
        tmp.alignment = TextAlignmentOptions.Center;
    }
}