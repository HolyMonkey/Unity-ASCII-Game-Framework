using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private  Vector2 _gridSize;

    private Vector2 _gridOffset;

    [SerializeField]
    private Sprite _cellSprite;
    private Vector2 _cellSize;
    private Vector2 _cellScale;

    void Start()
    {
        InitCells();
    }

    void InitCells()
    {
        GameObject cellObject = new GameObject();

        cellObject.AddComponent<SpriteRenderer>().sprite = _cellSprite;

        _cellSize = _cellSprite.bounds.size;

        _cellScale.x = 1 / _cellSize.x;
        _cellScale.y = 1 / _cellSize.y;

        _cellSize = new Vector2(1, 1);

        cellObject.transform.localScale = new Vector2(_cellScale.x, _cellScale.y);

        _gridOffset.x = -(_gridSize.x / 2) + _cellSize.x / 2;
        _gridOffset.y = -(_gridSize.y / 2) + _cellSize.y / 2;

        for (int row = 0; row < _gridSize.x; row++)
        {
            for (int col = 0; col < _gridSize.y; col++)
            {
                Vector2 pos = new Vector2(col * _cellSize.x + _gridOffset.x + transform.position.x, row * _cellSize.y + _gridOffset.y + transform.position.y);
               
                GameObject cO = Instantiate(cellObject, pos, Quaternion.identity) as GameObject;
                cO.GetComponent<SpriteRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                
                cO.transform.parent = transform;
            }
        }
        Destroy(cellObject);
    }
}