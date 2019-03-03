using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCamera : MonoBehaviour
{
    public Grid WorkGrid;
    private Vector3 _currentPosition;

    void Start()
    {
        gameObject.GetComponent<Camera>().orthographic = true;
        _currentPosition = transform.position;
    }

    private void ChangePosition(Vector3 newPosition)
    {
        gameObject.transform.position = newPosition;
        _currentPosition = gameObject.transform.position;
    }

    public void MoveUp()
    {
        ChangePosition(new Vector3(_currentPosition.x, _currentPosition.y + WorkGrid.CellSize, _currentPosition.z));
    }

    public void MoveDown()
    {
        ChangePosition(new Vector3(_currentPosition.x, _currentPosition.y - WorkGrid.CellSize, _currentPosition.z));
    }

    public void MoveLeft()
    {
        ChangePosition(new Vector3(_currentPosition.x - WorkGrid.CellSize, _currentPosition.y, _currentPosition.z));
    }

    public void MoveRight()
    {
        ChangePosition(new Vector3(_currentPosition.x + WorkGrid.CellSize, _currentPosition.y, _currentPosition.z));
    }

}
