using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Testing : MonoBehaviour
{
    public Grid Grid;
    private int _fromX=0, _fromY=0, _toX=10, _toY=3;
    private float _duration = 10;

    private int _gridLength = 30;
    private int _gridHeight = 30;

    static private int error;
    static private int dx, dy,y,ystep,xstep,tempX,tempY,x;

    void Start()
    {
        float _cellSize = 0.5f;

        Grid.SetCellSize(_cellSize);
        Grid.Reset(_gridLength, _gridHeight);

        Grid.Write(0, 4, '@', Color.yellow);

        Grid.Animations.Transit(0, 4, 7, 0).Duration(5).Start();
        //Grid.GetSymbol(0,4).Animations.StretchFigure().Duration(2).Start();


        //Grid.GetSymbol(0, 4).Animations.FadeOut().Duration(10).Start();
        //Invoke("ChangePosition", 3);

    }
    void ChangePosition()
    {

        Grid.GetSymbol(20, 0).Animations.FadeIn().Duration(2).Start();
    }

    void Update()
    {
        
    }
}
