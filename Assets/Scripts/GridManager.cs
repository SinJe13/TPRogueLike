using UnityEngine;

public class GridManager : MonoBehaviour
{
    static private GridManager _instance;
    static public GridManager Instance { get { return _instance; } }

    [SerializeField] Grid _grid;

    private void Awake()
    {
        _instance = this;
    }

    public Vector3 CellToWorld(Vector3Int position)
    {
        return _grid.CellToWorld(position);
    }

    public Vector3Int WorldToCell(Vector3 position)
    {
        return _grid.WorldToCell(position);
    }
}


