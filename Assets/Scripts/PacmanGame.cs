using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PacmanGame : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    private Level _level;

    private char _food = '·';
    private float _cellSize = 0.5f;
    private int _gridLength = 30;
    private int _gridHeight = 15;

    // Start is called before the first frame update
    void Start()
    {
        _grid.SetCellSize(_cellSize);
        _grid.Reset(_gridLength, _gridHeight);

        string[] map = File.ReadAllLines("Assets/Resources/PacmanLvl.txt");
        _level = new Level(_grid, map);
        _level.GenerationRule += (symbol) =>
        {
            if (symbol == ' ')
                return _food;
            else
                return symbol;
        };

        _level.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
