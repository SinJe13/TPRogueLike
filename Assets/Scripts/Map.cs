using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public const float CellSize = 0.08f;

    public static bool[,] grid = new bool[27, 15];


    [Header("Walls")]
    public List<Vector2Int> Obstacles;

    [Header("Ennemies")]
    [SerializeField] private GameObject _prefabEnemy;
    [SerializeField] private List<Vector2Int> Ennemies = new List<Vector2Int>();

    [Header("Loots")]
    [SerializeField] private GameObject _prefabPotion;
    [SerializeField] private GameObject _prefabGold;

    [Header("Pots")]
    [SerializeField] private GameObject _prefabPot;
    [SerializeField] private List<Vector2Int> Pots = new List<Vector2Int>();

    [Header("Vortex")]
    [SerializeField] private GameObject _prefabVortex;
    public List<Vector2Int> Vortex = new List<Vector2Int>();

    [Header("PNJ")]
    [SerializeField] private GameObject _prefabPNJ;
    [SerializeField] private List<Vector2Int> PNJ = new List<Vector2Int>();

    private void Awake()
    {
        foreach (var cell in Obstacles)
        {
            grid[cell.x, cell.y] = true; // true = obstacle
        }

        foreach (var cell in Ennemies)
        {
            Vector3 worldPosition = new Vector3(cell.x * CellSize, cell.y * CellSize, 0);

            var init = Instantiate(_prefabEnemy, worldPosition, Quaternion.identity);
            var enemy = init.GetComponent<Enemy>();
            enemy.Initialize(cell.x, cell.y, 20, 5, 2);
            grid[cell.x, cell.y] = init;
        }

        foreach (var position in Pots)
        {
            Vector3 worldPosition = new Vector3(position.x * CellSize, position.y * CellSize, 0);
            var go = Instantiate(_prefabPot, worldPosition, Quaternion.identity);
            go.tag = "Vase";
            grid[position.x, position.y] = go;
        }

        if (Vortex.Count == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                int x = Vortex[i].x;
                int y = Vortex[i].y;

                Vector3 worldPosition = new Vector3(x * CellSize, y * CellSize, 0);
                var go = Instantiate(_prefabVortex, worldPosition, Quaternion.identity);
                go.tag = "Vortex";
                MainGame.Instance.SetObject(x, y, go);
            }
        }

        foreach (var position in PNJ)
        {
            Vector3Int _gridPosition = new Vector3Int(position.x, position.y);

            Vector3 WorldPosition = GridManager.Instance.CellToWorld(_gridPosition);

            var init = Instantiate(_prefabPNJ, WorldPosition, Quaternion.identity);
            init.tag = "PNJ";

            grid[position.x, position.y] = true;
        }
    }
}