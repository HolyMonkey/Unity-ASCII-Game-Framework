using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class PacmanGame : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private float _breakDuration;
    [SerializeField] private TMP_Text _pointsVisual;

    private KeyCode _upButton = KeyCode.W;
    private KeyCode _downButton = KeyCode.S;
    private KeyCode _rightButton = KeyCode.D;
    private KeyCode _leftButton = KeyCode.A;

    private Level _level;
    private Pacman _pacman;

    private int _maxPoints;
    private int _currentPoints
    {
        get => _currentPoints;
        set
        {
            _currentPoints = value;
            _pointsVisual.text = _currentPoints.ToString();
            if (_currentPoints >= _maxPoints)
                GameEnd(true);
        }
    }

    private char _food = '·';
    private char _wall = '#';
    private float _cellSize = 0.5f;
    private int _gridLength = 30;
    private int _gridHeight = 15;

    private float _timer = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        _grid.SetCellSize(_cellSize);
        _grid.Reset(_gridLength, _gridHeight);

        string[] map = File.ReadAllLines("Assets/Resources/PacmanLvl.txt");
        _level = new Level(_grid, map);
        _level.GenerationRule += (symbol) =>
        {
            if (symbol == ' ')
                return _food;
            else if(symbol == 'X')
                return ' ';
            else
                return symbol;
        };
        _level.Draw();

        _maxPoints = CalculatePoints();

        _pacman = new Pacman(1, 1, 1, 0);
        _grid.Write(_pacman.X, _pacman.Y, _pacman.GetCurrentState(), Color.yellow);
    }

    // Update is called once per frame
    private void Update()
    {
        if(CalculateTime())
        {            
            if(WasFoodThere(_pacman.X, _pacman.Y))
            {
                _currentPoints++;
                _level.Replace(_pacman.X, _pacman.Y, 'X');
            }
            _grid.Write(_pacman.X, _pacman.Y, _level.GetSymbol(_pacman.X, _pacman.Y));
            _pacman.Move();
            _grid.Write(_pacman.X, _pacman.Y, _pacman.GetCurrentState(), Color.yellow);
        }

        if (Input.GetKeyUp(_upButton))
            _pacman.TurnUp();
        else if (Input.GetKeyUp(_downButton))
            _pacman.TurnDown();
        else if (Input.GetKeyUp(_leftButton))
            _pacman.TurnLeft();
        else if (Input.GetKeyUp(_rightButton))
            _pacman.TurnRight();

        if (IsWallThere(_pacman.X + _pacman.XDir, _pacman.Y + _pacman.YDir))
            SmartTurn();

    }

    private void GameEnd(bool win)
    {
        _grid.Reset(_gridLength, _gridHeight);
        _grid.WriteLine(win ? "U have won!" : "unfortunately, U have lose");
        enabled = false;
    }

    private bool WasFoodThere(int x, int y)
    {
        return _level.GetSymbol(x, y) == _food;
    }

    private int CalculatePoints()
    {
        int res = 0;
        for (int i = 0; i < _gridLength; i++)
        {
            for (int j = 0; j < _gridHeight; j++)
            {
                res += _level.GetSymbol(i, j) == _food ? 1 : 0;
            }
        }
        return res;
    }

    private void SmartTurn()
    {
        if(_pacman.YDir != 0)
        {
            if(!IsWallThere(_pacman.X + 1, _pacman.Y) && !IsWallThere(_pacman.X - 1, _pacman.Y))
                switch(Random.Range(0, 2))
                {
                    case 0: _pacman.TurnLeft();  break;
                    case 1: _pacman.TurnRight(); break;
                }
            else if(!IsWallThere(_pacman.X + 1, _pacman.Y))
                _pacman.TurnRight();
            else if (!IsWallThere(_pacman.X - 1, _pacman.Y))
                _pacman.TurnLeft();
            else
            {
                switch(_pacman.YDir)
                {
                    case -1: _pacman.TurnDown(); break;
                    case 1: _pacman.TurnUp(); break;
                }
            }
        }
        else
        {
            if (!IsWallThere(_pacman.X, _pacman.Y + 1) && !IsWallThere(_pacman.X, _pacman.Y - 1))
                switch (Random.Range(0, 2))
                {
                    case 0: _pacman.TurnUp(); break;
                    case 1: _pacman.TurnDown(); break;
                }
            else if (!IsWallThere(_pacman.X, _pacman.Y + 1))
                _pacman.TurnDown();
            else if (!IsWallThere(_pacman.X, _pacman.Y-1))
                _pacman.TurnUp();
            else
            {
                switch (_pacman.XDir)
                {
                    case -1: _pacman.TurnRight(); break;
                    case 1: _pacman.TurnLeft(); break;
                }
            }
        }
    }

    private bool IsWallThere(int x, int y)
    {
        return _level.GetSymbol(x, y) == _wall;
    }

    private bool CalculateTime()
    {
        _timer += Time.deltaTime;
        if(_timer >= _breakDuration)
        {
            _timer = 0;
            return true;
        }
        return false;
    }
}
