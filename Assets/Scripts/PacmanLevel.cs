using UnityEngine.Events;
using System;

class PacmanLevel
{
    private Grid _grid;
    private char[,] _map;

    private char _food;

    public PacmanLevel(Grid grid, string[] lvl, char food)
    {
        _grid = grid;
        _map = ConvertToCharArray(lvl);
        _food = food;
    }

    public void Draw()
    {
        for (int x = 0; x < _map.GetLength(0); x++)
            for (int y = 0; y < _map.GetLength(1); y++)
                _grid.Write(x, y, GenerationRule(_map[x, y]));
    }

    public void Replace(int x, int y, char newSymbol)
    {
        _map[x, y] = newSymbol;
    }

    public char GetSymbol(int x, int y)
    {
        return GenerationRule(_map[x, y]);
    }

    private char[,] ConvertToCharArray(string[] array)
    {
        char[,] result = new char[array[0].Length, array.Length];
        for (int x = 0; x < result.GetLength(0); x++)
            for (int y = 0; y < result.GetLength(1); y++)
                result[x, y] = array[y][x];
        return result;
    }

    private char GenerationRule(char symbol)
    {
        if (symbol == ' ')
            return _food;
        return symbol;
    }
}