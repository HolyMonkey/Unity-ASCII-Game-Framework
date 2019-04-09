using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

class Level
{
    public event Func<char, char> OnGeneration;

    private Grid _grid;
    private char[,] _map;

    public Level(Grid grid, string[] lvl)
    {
        _grid = grid;
        _map = GetFromFile(lvl);
    }

    public void Draw()
    {
        for (int i = 0; i < _map.GetLength(0); i++)
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                if(OnGeneration != null)
                    _grid.Write(j, i, OnGeneration(_map[i, j]));
            }
    }

    public void Replace(int x, int y, char NewSymbol)
    {
        _map[y, x] = NewSymbol;
    }

    public char GetSymbol(int x, int y)
    {
        return OnGeneration(_map[y, x]);
    }

    private char[,] GetFromFile(string[] file)
    {
        char[,] result = new char[file.Length, file[0].Length];
        for (int i = 0; i < result.GetLength(0); i++)
            for (int j = 0; j < result.GetLength(1); j++)
                result[i, j] = file[i][j];
        return result;
    }
}