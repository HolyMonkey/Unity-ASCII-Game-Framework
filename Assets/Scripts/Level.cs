using System.Collections;
using System.Collections.Generic;

class Level
{
    public delegate char Rule(char symbol);
    public event Rule GenerationRule;

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
                _grid.Write(j, i, GenerationRule(_map[i, j]));
    }

    public void Replace(int x, int y, char NewSymbol)
    {
        _map[y, x] = NewSymbol;
    }

    public char GetSymbol(int x, int y)
    {
        return GenerationRule(_map[y, x]);
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