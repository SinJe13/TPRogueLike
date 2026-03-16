using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector2Int _gridPosition = new Vector2Int(0, 0);

    private void Start()
    {
        _gridPosition = (Vector2Int) GridManager.Instance.WorldToCell(transform.position);
    }

    void Update()
    {
        Vector2Int coordPlayer = _gridPosition;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            coordPlayer = _gridPosition + new Vector2Int(1, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            coordPlayer = _gridPosition + new Vector2Int(-1, 0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            coordPlayer = _gridPosition + new Vector2Int(0, -1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            coordPlayer = _gridPosition + new Vector2Int(0, 1);
        }

        
        // Vérification obstacle
        if (coordPlayer.x >= 0 && coordPlayer.x < 20 &&
            coordPlayer.y >= 0 && coordPlayer.y < 20 &&
            !Map.grid[coordPlayer.x, coordPlayer.y])
        {
            _gridPosition = coordPlayer;

            transform.position = GridManager.Instance.CellToWorld((Vector3Int) _gridPosition);
        }
    }
}