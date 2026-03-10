using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMove : MonoBehaviour
{
    Vector2Int _gridPosition = new Vector2Int(7, 9);
    Vector2Int _coordPlayer;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _coordPlayer = _gridPosition + new Vector2Int(1, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _coordPlayer = _gridPosition + new Vector2Int(-1, 0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _coordPlayer = _gridPosition + new Vector2Int(0, -1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _coordPlayer = _gridPosition + new Vector2Int(0, 1);
        }

        //if (Map.coord[_coordPlayer.x, _coordPlayer.y] != 0)
        //{
            _gridPosition = _coordPlayer;
            transform.position = new Vector3(_gridPosition.x * Map.CellSize, _gridPosition.y * Map.CellSize, 0);
        //}

    }
}
