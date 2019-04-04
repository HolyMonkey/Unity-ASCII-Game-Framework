using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class Fill : MonoBehaviour
{
    public Transform Parent;
    public GameObject CellPrefab;
    public Vector2 CellSize;

    public void Rect(float x, float y, int width, int height, char symbol, Color color)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var rect = Instantiate(CellPrefab, Parent);
                rect.transform.position = new Vector3(x + i * CellSize.x, y - j * CellSize.y, rect.transform.position.z);
                rect.transform.localScale = new Vector3(CellSize.x, CellSize.y, 0);
                rect.GetComponentInChildren<TextMeshPro>().color = color;
                rect.GetComponentInChildren<TextMeshPro>().text = symbol.ToString();
            }
        }
    }

}
