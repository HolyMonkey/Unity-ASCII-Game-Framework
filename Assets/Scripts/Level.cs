
using UnityEngine.Events;
using System;

class Level
{
    private Func<char, char> _generationRule;

    private Grid _grid;
    private char[,] _map;

    public Level(Grid grid, string[] lvl, Func<char, char> generationRule)
    {
        _grid = grid;
        _map = ConvertToCharArray(lvl);
        _generationRule = generationRule;
    }

    public void Draw()
    {
        for (int i = 0; i < _map.GetLength(0); i++)
            for (int j = 0; j < _map.GetLength(1); j++)
                _grid.Write(j, i, _generationRule(_map[i, j]));
    }

    public void Replace(int x, int y, char newSymbol)
    {
        _map[y, x] = newSymbol;
    }

    public char GetSymbol(int x, int y)
    {
        return _generationRule(_map[y, x]);
    }

    private char[,] ConvertToCharArray(string[] array)
    {
        char[,] result = new char[array.Length, array[0].Length];
        for (int i = 0; i < result.GetLength(0); i++)
            for (int j = 0; j < result.GetLength(1); j++)
                result[i, j] = array[i][j];
        return result;
    }
}