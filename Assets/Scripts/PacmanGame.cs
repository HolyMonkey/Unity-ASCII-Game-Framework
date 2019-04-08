using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class PacmanGame : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private float _pacmanBreakDuration;
    [SerializeField] private float _ghostBreakDuration;
    [SerializeField] private TMP_Text _pointsVisual;

    private KeyCode _upButton = KeyCode.W;
    private KeyCode _downButton = KeyCode.S;
    private KeyCode _rightButton = KeyCode.D;
    private KeyCode _leftButton = KeyCode.A;

    private Level _level;
    private Pacman _pacman;
    private Ghost[] _ghosts = new Ghost[2];

    private int _maxPoints;
    private int _currentPoints = 0;

    private char _food = '·';
    private char _wall = '#';
    private float _cellSize = 0.5f;
    private int _gridLength = 30;
    private int _gridHeight = 15;

    private float _pacmanTimer = 0f;
    private float _ghostTimer = 0f;

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
            else
                return symbol;
        };
        _level.Draw();

        _maxPoints = CalculatePoints();

        _pacman = new Pacman(1, 1, 1, 0);
        _grid.Write(_pacman.X, _pacman.Y, _pacman.GetCurrentState(), Color.yellow);
        _ghosts[0] = new Ghost(28, 13, -1, 0);
        _ghosts[1] = new Ghost(17, 11, -1, 0);
        foreach (var ghost in _ghosts)
        {
            _grid.Write(ghost.X, ghost.Y, ghost.Skin, Color.blue);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        CalculateTime(_pacman);
        CalculateTime(_ghosts[0]);

        if (Input.GetKeyUp(_upButton))
            MakeTurn(_upButton);
        else if (Input.GetKeyUp(_downButton))
            MakeTurn(_downButton);
        else if (Input.GetKeyUp(_leftButton))
            MakeTurn(_leftButton);
        else if (Input.GetKeyUp(_rightButton))
            MakeTurn(_rightButton);

        if (IsTargetThere(_pacman.X + _pacman.XDir, _pacman.Y + _pacman.YDir, _wall))
            SmartTurn(_pacman);
    }

    private void GhostsTurn()
    {
        foreach (var ghost in _ghosts)
        {
            DrawMove(ghost, Color.blue);
            GhostMove(ghost);
            if (CheckForPacman(ghost.X, ghost.Y))
                GameEnd(false);
            if (IsTargetThere(ghost.X + ghost.XDir, ghost.Y + ghost.YDir, _wall))
                SmartTurn(ghost);
        }
    }

    private void PacmanTurn()
    {
        if (IsTargetThere(_pacman.X, _pacman.Y, _food))
        {
            AddPoint();
            _level.Replace(_pacman.X, _pacman.Y, char.MinValue);
        }
        DrawMove(_pacman, Color.yellow);
    }

    private void DrawMove(Creature creature, Color color)
    {
        _grid.Write(creature.X, creature.Y, _level.GetSymbol(creature.X, creature.Y));
        creature.Move();
        _grid.Write(creature.X, creature.Y, creature is Pacman ? (creature as Pacman).GetCurrentState() : (creature as Ghost).Skin, color);
    }

    private void GhostMove(Ghost ghost)
    {
        if(ghost.YDir != 0)
        {
            if(!IsTargetThere(ghost.X + 1, ghost.Y, _wall) && !IsTargetThere(ghost.X - 1, ghost.Y, _wall))
            {
                switch(Random.Range(0, 3))
                {
                    case 0: ghost.TurnRight(); break;
                    case 1: ghost.TurnLeft(); break;
                }
            }
            else if(!IsTargetThere(ghost.X + 1, ghost.Y, _wall))
            {
                switch (Random.Range(0, 2))
                {
                    case 0: ghost.TurnRight(); break;
                }
            }
            else if (!IsTargetThere(ghost.X - 1, ghost.Y, _wall))
            {
                switch (Random.Range(0, 2))
                {
                    case 0: ghost.TurnLeft(); break;
                }
            }
        }
        else
        {
            if (!IsTargetThere(ghost.X, ghost.Y + 1, _wall) && !IsTargetThere(ghost.X, ghost.Y - 1, _wall))
            {
                switch (Random.Range(0, 3))
                {
                    case 0: ghost.TurnUp(); break;
                    case 1: ghost.TurnDown(); break;
                }
            }
            else if (!IsTargetThere(ghost.X, ghost.Y + 1, _wall))
            {
                switch (Random.Range(0, 2))
                {
                    case 0: ghost.TurnDown(); break;
                }
            }
            else if (!IsTargetThere(ghost.X, ghost.Y - 1, _wall))
            {
                switch (Random.Range(0, 2))
                {
                    case 0: ghost.TurnUp(); break;
                }
            }
        }
    }

    private void SmartTurn(Creature forWhom)
    {
        if (forWhom.YDir != 0)
        {
            if (!IsTargetThere(forWhom.X + 1, forWhom.Y, _wall) && !IsTargetThere(forWhom.X - 1, forWhom.Y, _wall))
                switch (Random.Range(0, 2))
                {
                    case 0: forWhom.TurnLeft(); break;
                    case 1: forWhom.TurnRight(); break;
                }
            else if (!IsTargetThere(forWhom.X + 1, forWhom.Y, _wall))
                forWhom.TurnRight();
            else if (!IsTargetThere(forWhom.X - 1, forWhom.Y, _wall))
                forWhom.TurnLeft();
            else
            {
                switch (forWhom.YDir)
                {
                    case -1: forWhom.TurnDown(); break;
                    case 1: forWhom.TurnUp(); break;
                }
            }
        }
        else
        {
            if (!IsTargetThere(forWhom.X, forWhom.Y + 1, _wall) && !IsTargetThere(forWhom.X, forWhom.Y - 1, _wall))
                switch (Random.Range(0, 2))
                {
                    case 0: forWhom.TurnUp(); break;
                    case 1: forWhom.TurnDown(); break;
                }
            else if (!IsTargetThere(forWhom.X, forWhom.Y + 1, _wall))
                forWhom.TurnDown();
            else if (!IsTargetThere(forWhom.X, forWhom.Y - 1, _wall))
                forWhom.TurnUp();
            else
            {
                switch (forWhom.XDir)
                {
                    case -1: forWhom.TurnRight(); break;
                    case 1: forWhom.TurnLeft(); break;
                }
            }
        }
    }

    private void MakeTurn(KeyCode pressedKey)
    {
        if (pressedKey == _upButton)
            _pacman.TurnUp();
        else if (pressedKey == _downButton)
            _pacman.TurnDown();
        else if (pressedKey == _leftButton)
            _pacman.TurnLeft();
        else if (pressedKey == _rightButton)
            _pacman.TurnRight();
    }

    private void GameEnd(bool win)
    {
        _grid.Clear();
        _grid.WriteLine(win ? "U have won!" : "Unfortunately, U have lose");
        enabled = false;
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

    private bool CheckForPacman(int x, int y)
    {
        return _pacman.X == x && _pacman.Y == y;
    }

    private bool IsTargetThere(int x, int y, char target)
    {
        return _level.GetSymbol(x, y) == target;
    }

    private void CalculateTime(Pacman forWhom)
    {
        _pacmanTimer += Time.deltaTime;
        if (_pacmanTimer >= _pacmanBreakDuration)
        {
            _pacmanTimer = 0;
            PacmanTurn();
        }
    }

    private void CalculateTime(Ghost forWhom)
    {
        _ghostTimer += Time.deltaTime;
        if (_ghostTimer >= _ghostBreakDuration)
        {
            _ghostTimer = 0;
            GhostsTurn();
        }
    }

    private void AddPoint()
    {
        _currentPoints++;
        _pointsVisual.text = _currentPoints + " points";
        if (_currentPoints >= _maxPoints)
            GameEnd(true);
    }   
}
