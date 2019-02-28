using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCamera : MonoBehaviour
{
    public Grid workGrid;
    private Vector3 _currentPos;

    void Start()
    {
        Camera.main.orthographic = true;
        _currentPos = Camera.main.transform.position;
    }

    private void ChangePosition(Vector3 newPos,ref Vector3 _currentPos)
    {
        Camera.main.transform.position = newPos;
        _currentPos = Camera.main.transform.position;
    }

    public void MoveUp()
    {
        ChangePosition(new Vector3(_currentPos.x, _currentPos.y + workGrid.cellSize,_currentPos.z), ref _currentPos);
    }

    public void MoveDown()
    {
        ChangePosition(new Vector3(_currentPos.x, _currentPos.y - workGrid.cellSize, _currentPos.z), ref _currentPos);
    }

    public void MoveLeft()
    {
        ChangePosition(new Vector3(_currentPos.x - workGrid.cellSize, _currentPos.y, _currentPos.z), ref _currentPos);
    }

    public void MoveRight()
    {
        ChangePosition(new Vector3(_currentPos.x + workGrid.cellSize, _currentPos.y, _currentPos.z), ref _currentPos);
    }

}
