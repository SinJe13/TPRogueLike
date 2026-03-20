using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Map : MonoBehaviour
{
    public static Map Instance;

    public const float CellSize = 0.08f;

    public static GameObject[,] grid = new GameObject[27, 15];

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
    public List<Vector2Int> Pots = new List<Vector2Int>();

    [Header("Vortex")]
    [SerializeField] private GameObject _prefabVortex;
    public List<Vector2Int> Vortex = new List<Vector2Int>();

    [Header("PNJ")]
    [SerializeField] private GameObject _prefabPNJ;
    [SerializeField] private List<Vector2Int> PNJ = new List<Vector2Int>();

    public GameObject PrefabPnj => _prefabPNJ;
    public GameObject PrefabVortex => _prefabVortex;
    public GameObject PrefabPot => _prefabPot;
    public GameObject PrefabPotion => _prefabPotion;
    public GameObject PrefabGold => _prefabGold;

    private void Awake()
    {
        foreach (var cell in Obstacles)
        {
            grid[cell.x, cell.y] = new GameObject(); // true = obstacle
        }

        foreach (var cell in Ennemies)
        {
            Vector3Int _gridPosition = new Vector3Int(cell.x, cell.y);

            Vector3 WorldPosition = GridManager.Instance.CellToWorld(_gridPosition);
            //WorldPosition.x += CellSize/2;
            //WorldPosition.y += CellSize/2;

            var init = Instantiate(_prefabEnemy, WorldPosition, Quaternion.identity);
            var enemy = init.GetComponent<Enemy>();
            enemy.Initialize(cell.x, cell.y, 20, 5, 2);
            grid[cell.x, cell.y] = init;
        }

        foreach (var cell in Pots)
        {
            Vector3Int _gridPosition = new Vector3Int(cell.x, cell.y);

            Vector3 WorldPosition = GridManager.Instance.CellToWorld(_gridPosition);
            //WorldPosition.x += CellSize;
            //WorldPosition.y += CellSize;

            var init = Instantiate(_prefabPot, WorldPosition, Quaternion.identity);
            init.tag = "Vase";
            grid[cell.x, cell.y] = init;
        }

        if (Vortex.Count == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                int x = Vortex[i].x;
                int y = Vortex[i].y;

                Vector3Int _gridPosition = new Vector3Int(x, y);

                Vector3 WorldPosition = GridManager.Instance.CellToWorld(_gridPosition);
                //WorldPosition.x += CellSize/2;
                //WorldPosition.y += CellSize/2;

                var init = Instantiate(_prefabVortex, WorldPosition, Quaternion.identity);
                init.tag = "Vortex";
                MainGame.Instance.SetObject(x, y, init);
            }
        }

        foreach (var cell in PNJ)
        {
            Vector3Int _gridPosition = new Vector3Int(cell.x, cell.y);

            Vector3 WorldPosition = GridManager.Instance.CellToWorld(_gridPosition);
            //WorldPosition.x += CellSize/2;
            //WorldPosition.y += CellSize/2;

            var init = Instantiate(_prefabPNJ, WorldPosition, Quaternion.identity);
            init.tag = "PNJ";

            grid[cell.x, cell.y] = init;
        }
    }
}