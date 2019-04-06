using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PacmanGame : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    private Level _level;
    private char _food = '·';

    // Start is called before the first frame update
    void Start()
    {
        string[] map = File.ReadAllLines("Assets/Resources/PacmanLvl.txt");
        _level = new Level(_grid, map);
        _level.GenerationRule += (symbol) =>
        {
            if (symbol == ' ')
                return _food;
            else
                return symbol;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
