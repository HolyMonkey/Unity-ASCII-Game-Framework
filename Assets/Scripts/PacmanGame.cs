using UnityEngine;
using UnityEngine.Events;
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

    private KeyCode _lastPressedButton;

    private PacmanLevel _level;
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

    private bool _isGameOver = false;
    private bool _isPacmanStuck = false;

    private void Start()
    {
        _lastPressedButton = _upButton;

        _grid.SetCellSize(_cellSize);
        _grid.Reset(_gridLength, _gridHeight);

        string[] map = Resources.Load<TextAsset>("PacmanLvl").ToString().Split('\n');
        ChangeMap(ref map);

        _level = new PacmanLevel(_grid, map, _food);
        _level.Draw();

        _maxPoints = CalculatePoints();

        _pacman = new Pacman(new Vector2Int(1, 1), new Vector2Int(1, 0), Color.yellow);
        _grid.Write(_pacman.Position.x, _pacman.Position.y, _pacman.GetSkin(), Color.yellow);
        _ghosts[0] = new Ghost(new Vector2Int(28, 13), new Vector2Int(-1, 0), Color.blue);
        _ghosts[1] = new Ghost(new Vector2Int(17, 11), new Vector2Int(-1, 0), Color.blue);
        foreach (var ghost in _ghosts)
            _grid.Write(ghost.Position.x, ghost.Position.y, ghost.GetSkin(), ghost.CurrentColor);
    }

    private void ChangeMap(ref string[] map)
    {
        string[] newMap = new string[map.Length];
        for (int i = 0; i < newMap.Length; i++)
        {
            newMap[i] = map[i];
        }
        for (int i = 0; i < newMap.Length - 1; i++)
        {
            string newRow = null;
            for (int j = 0; j < newMap[i].Length - 1; j++)
            {
                newRow += newMap[i][j];
            }
            newMap[i] = newRow;
        }
        map = newMap;
    }

    private void PacmanMove()
    {
        if (IsTargetThere(_pacman.Position.x, _pacman.Position.y, _food))
        {
            AddPoint();
            _level.Replace(_pacman.Position.x, _pacman.Position.y, char.MinValue);
        }
        _pacman.Move();
        EraseSomething(_pacman.Position.x - _pacman.Direction.x, _pacman.Position.y - _pacman.Direction.y);
        DrawCreature(_pacman);
    }

    private void GhostMove(Ghost ghost)
    {
        ghost.Move();
        if (CheckForPacman(ghost.Position.x, ghost.Position.y) || CheckForPacman(ghost.Position.x - ghost.Direction.x, ghost.Position.y - ghost.Direction.y))
            GameEnd(false);
        EraseSomething(ghost.Position.x - ghost.Direction.x, ghost.Position.y - ghost.Direction.y);
        DrawCreature(ghost);
        PossibleTurn(ghost);
        if (IsTargetThere(ghost.Position.x + ghost.Direction.x, ghost.Position.y + ghost.Direction.y, _wall))
            SmartTurn(ghost);
    }

    private void Update()
    {
        CalculateTimeForGhosts();
        CalculateTimeForPacman();

        if (Input.GetKeyUp(_upButton))
            ButtonClick(_upButton);
        else if (Input.GetKeyUp(_downButton))
            ButtonClick(_downButton);
        else if (Input.GetKeyUp(_leftButton))
            ButtonClick(_leftButton);
        else if (Input.GetKeyUp(_rightButton))
            ButtonClick(_rightButton);
    }

    private void ButtonClick(KeyCode button)
    {
        _lastPressedButton = button;
        if (_isPacmanStuck)
        {
            PacmanTurn(_lastPressedButton);
            _isPacmanStuck = false;
        }
    }

    private void PacmanTurn(KeyCode pressedButton)
    {
        if (pressedButton == _upButton && !IsTargetThere(_pacman.Position.x, _pacman.Position.y - 1, _wall))
            _pacman.TurnUp();
        else if (pressedButton == _downButton && !IsTargetThere(_pacman.Position.x, _pacman.Position.y + 1, _wall))
            _pacman.TurnDown();
        else if (pressedButton == _leftButton && !IsTargetThere(_pacman.Position.x - 1, _pacman.Position.y, _wall))
            _pacman.TurnLeft();
        else if (pressedButton == _rightButton && !IsTargetThere(_pacman.Position.x + 1, _pacman.Position.y, _wall))
            _pacman.TurnRight();
    }

    private void DrawCreature(Moveable creature)
    {
        if (_isGameOver)
            return;
        _grid.Write(creature.Position.x, creature.Position.y, creature.GetSkin(), creature.CurrentColor);
    }

    private void EraseSomething(int x, int y)
    {
        if (_isGameOver)
            return;
        _grid.Write(x, y, _level.GetSymbol(x, y));
    }

    private void PossibleTurn(Moveable creature, UnityAction action = null)
    {
        if(creature.Direction.y != 0)
        {
            if(!IsTargetThere(creature.Position.x + 1, creature.Position.y, _wall) && !IsTargetThere(creature.Position.x - 1, creature.Position.y, _wall))
                TurnVariant(creature, action, 3, () => creature.TurnRight(), () => creature.TurnLeft());
            else if(!IsTargetThere(creature.Position.x + 1, creature.Position.y, _wall))
                TurnVariant(creature, action, 2, () => creature.TurnRight());
            else if (!IsTargetThere(creature.Position.x - 1, creature.Position.y, _wall))
                TurnVariant(creature, action, 2, () => creature.TurnLeft());
        }
        else
        {
            if (!IsTargetThere(creature.Position.x, creature.Position.y + 1, _wall) && !IsTargetThere(creature.Position.x, creature.Position.y - 1, _wall))
                TurnVariant(creature, action, 3, () => creature.TurnUp(), () => creature.TurnDown());
            else if (!IsTargetThere(creature.Position.x, creature.Position.y + 1, _wall))
                TurnVariant(creature, action, 2, () => creature.TurnDown());
            else if (!IsTargetThere(creature.Position.x, creature.Position.y - 1, _wall))
                TurnVariant(creature, action, 2, () => creature.TurnUp());
        }
    }

    private void TurnVariant(Moveable creature, UnityAction action, int maxNumber, params UnityAction[] avaibaleMethods)
    {
        if (creature is Ghost)
            RandomTurn(maxNumber, avaibaleMethods);
        else
            action?.Invoke();
    }

    private void RandomTurn(int maxNumber, params UnityAction[] avaibaleMethods)
    {
        int chosenNumber = Random.Range(0, maxNumber);
        if (chosenNumber < avaibaleMethods.Length)
            avaibaleMethods[chosenNumber].Invoke();
    }

    private void SmartTurn(Moveable forWhom)
    {
        if (forWhom.Direction.y != 0)
        {
            if (!IsTargetThere(forWhom.Position.x + 1, forWhom.Position.y, _wall) && !IsTargetThere(forWhom.Position.x - 1, forWhom.Position.y, _wall))
                switch (Random.Range(0, 2))
                {
                    case 0: forWhom.TurnLeft(); break;
                    case 1: forWhom.TurnRight(); break;
                }
            else if (!IsTargetThere(forWhom.Position.x + 1, forWhom.Position.y, _wall))
                forWhom.TurnRight();
            else if (!IsTargetThere(forWhom.Position.x - 1, forWhom.Position.y, _wall))
                forWhom.TurnLeft();
            else
            {
                switch (forWhom.Direction.y)
                {
                    case -1: forWhom.TurnDown(); break;
                    case 1: forWhom.TurnUp(); break;
                }
            }
        }
        else
        {
            if (!IsTargetThere(forWhom.Position.x, forWhom.Position.y + 1, _wall) && !IsTargetThere(forWhom.Position.x, forWhom.Position.y - 1, _wall))
                switch (Random.Range(0, 2))
                {
                    case 0: forWhom.TurnUp(); break;
                    case 1: forWhom.TurnDown(); break;
                }
            else if (!IsTargetThere(forWhom.Position.x, forWhom.Position.y + 1, _wall))
                forWhom.TurnDown();
            else if (!IsTargetThere(forWhom.Position.x, forWhom.Position.y - 1, _wall))
                forWhom.TurnUp();
            else
            {
                switch (forWhom.Direction.x)
                {
                    case -1: forWhom.TurnRight(); break;
                    case 1: forWhom.TurnLeft(); break;
                }
            }
        }
    }

    private void GameEnd(bool win)
    {
        _isGameOver = true;
        _grid.Clear();   
        _grid.WriteLine(win ? "U have won!" : "Unfortunately, U have lose");
        enabled = false;
    }

    private int CalculatePoints()
    {
        int result = 0;
        for (int i = 0; i < _gridLength; i++)
        {
            for (int j = 0; j < _gridHeight; j++)
            {
                result += _level.GetSymbol(i, j) == _food ? 1 : 0;
            }
        }
        return result;
    }

    private bool CheckForPacman(int x, int y)
    {
        return _pacman.Position.x == x && _pacman.Position.y == y;
    }

    private bool IsTargetThere(int x, int y, char target)
    {
        return _level.GetSymbol(x, y) == target;
    }

    private void CalculateTimeForPacman()
    {
        _pacmanTimer += Time.deltaTime;
        if (_pacmanTimer >= _pacmanBreakDuration)
        {
            _pacmanTimer = 0;
            PossibleTurn(_pacman, () => PacmanTurn(_lastPressedButton));
            
            if (!IsTargetThere(_pacman.Position.x + _pacman.Direction.x, _pacman.Position.y + _pacman.Direction.y, _wall))
            {
                PacmanTurn(_lastPressedButton);
                PacmanMove();
            }
            else
            {
                _isPacmanStuck = true;
                DrawCreature(_pacman);
                _pacman.Animation();          
            }  
        }
    }

    private void CalculateTimeForGhosts()
    {
        _ghostTimer += Time.deltaTime;
        if (_ghostTimer >= _ghostBreakDuration)
        {
            _ghostTimer = 0;
            foreach (var ghost in _ghosts)
                GhostMove(ghost);
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
