using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCamera : MonoBehaviour
{
    public Grid WorkGrid;
    private Vector3 _currentPos;

    void Start()
    {
        gameObject.GetComponent<Camera>().orthographic = true;
        _currentPos = gameObject.transform.position;
    }

    private void ChangePosition(Vector3 newPos,ref Vector3 currentPos)
    {
        gameObject.transform.position = newPos;
        currentPos = gameObject.transform.position;
    }

    public void MoveUp()
    {
        ChangePosition(new Vector3(_currentPos.x, _currentPos.y + WorkGrid.CellSize,_currentPos.z), ref _currentPos);
    }

    public void MoveDown()
    {
        ChangePosition(new Vector3(_currentPos.x, _currentPos.y - WorkGrid.CellSize, _currentPos.z), ref _currentPos);
    }

    public void MoveLeft()
    {
        ChangePosition(new Vector3(_currentPos.x - WorkGrid.CellSize, _currentPos.y, _currentPos.z), ref _currentPos);
    }

    public void MoveRight()
    {
        ChangePosition(new Vector3(_currentPos.x + WorkGrid.CellSize, _currentPos.y, _currentPos.z), ref _currentPos);
    }

}
