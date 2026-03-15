using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    public const float CellSize = 0.08f;

    bool[,] _grid = new bool[20, 20];

    public List<Vector2Int> Obstacles;

    private void Awake()
    {
        foreach (var cell in Obstacles)
        {
            _grid[cell.x, cell.y] = new GameObject();
        }

        //foreach (var cell in Enemies)
        //{
        //    Vector3 position = new Vector3(cell.x * CellSize, cell.y * CellSize);

        //}
    }
}
