using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Snake : MonoBehaviour
{
    public Grid Grid;
    public ConsoleInput ConsoleInput;
    public TMP_Text TextBox;

    private int _snakePosX = 5;
    private int _snakePosY = 5;
    private int _dirX = 1;
    private int _dirY = 0;

    private int _gridLength = 30;
    private int _gridHeight = 30;
    private char _food = 'A';
    private char _snake = '@';
    private char _background = '#';
    private Color _backgroundColor = Color.black;

    private List<Vector2> tail = new List<Vector2>();

    void Start()
    {
        float _cellSize = 0.5f;

        Grid.SetCellSize(_cellSize);
        Grid.Reset(_gridLength, _gridHeight);

        for (int i = 0; i < _gridLength; i++)
            for (int j = 0; j < _gridHeight; j++)
                Grid.Write(i, j,_background, _backgroundColor);

        Grid.Write(_snakePosX, _snakePosY, _snake, Color.yellow);

        SpawnFood();
        InvokeRepeating("ChangePosition",0,0.1f);
    }

    void SpawnFood()
    {
        int X, Y;

        do
        {
            X = Random.Range(0, _gridLength);
            Y = Random.Range(0, _gridHeight);
        }
        while (Grid.HasSymbol(X, Y, _snake));
       
        Grid.Write(X, Y,_food,Color.red);
    }

    void ChangePosition()
    {
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.RightArrow) && _dirX == 0)
            {
                _dirX = 1;
                _dirY = 0;
            }
            else if (Input.GetKey(KeyCode.UpArrow) && _dirY == 0)
            {
                _dirX = 0;
                _dirY = -1;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && _dirY == 0)
            {
                _dirX = 0;
                _dirY = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && _dirX == 0)
            {
                _dirX = -1;
                _dirY = 0; 
            }
        }

        if ((_snakePosX + _dirX >= 0 && _snakePosX + _dirX < _gridLength && _snakePosY + _dirY >= 0 && _snakePosY + _dirY < _gridHeight) && !Grid.HasSymbol(_snakePosX + _dirX, _snakePosY + _dirY,_snake))
        {
            Vector2 currentPosition = new Vector2(_snakePosX, _snakePosY);

            if (Grid.HasSymbol(_snakePosX + _dirX, _snakePosY + _dirY, _food))
            {
                tail.Insert(0, currentPosition);
                SpawnFood();
            }
            else if (tail.Count>0)
            {
                Grid.Write((int)tail[tail.Count - 1].x, (int)tail[tail.Count - 1].y, _background, _backgroundColor);
                tail[tail.Count-1] = currentPosition;
                tail.Insert(0, tail[tail.Count - 1]);
                tail.RemoveAt(tail.Count - 1);
            }
            else
                Grid.Write((int)currentPosition.x, (int)currentPosition.y, _background, _backgroundColor);

            Grid.Write(_snakePosX += _dirX, _snakePosY += _dirY, _snake, Color.yellow);
            TextBox.text = (tail.Count + 1).ToString();
        }
        else
        {
            Time.timeScale = 0;
            Debug.Log("Вы проиграли");
        }
    }
}
