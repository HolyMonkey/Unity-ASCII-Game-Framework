using System.Collections;
using System.Collections.Generic;

class Level
{
    public delegate char Rule(char symbol);
    public event Rule GenerationRule;

    private Grid _grid;
    private char[,] _level;

    public Level(Grid grid, string[] lvl)
    {
        _grid = grid;
        _level = GetFromFile(lvl);
    }

    public void Reset()
    {
        for (int i = 0; i < _level.GetLength(0); i++)
            for (int j = 0; j < _level.GetLength(1); j++)
                _grid.Write(GenerationRule(_level[i, j]));
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