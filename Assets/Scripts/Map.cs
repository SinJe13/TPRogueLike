using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    public const float CellSize = 0.08f;

    public static bool[,] grid = new bool[27, 15];

    public List<Vector2Int> Obstacles;

    private void Awake()
    {
        foreach (var cell in Obstacles)
        {
            grid[cell.x, cell.y] = true; // true = obstacle
        }
    }
}