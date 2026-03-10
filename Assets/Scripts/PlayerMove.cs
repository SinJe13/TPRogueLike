using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMove : MonoBehaviour
{
    Vector2Int _gridPosition;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _gridPosition += new Vector2Int(1, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _gridPosition += new Vector2Int(-1, 0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _gridPosition += new Vector2Int(0, -1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _gridPosition += new Vector2Int(0, 1);
        }

        transform.position = new Vector3(_gridPosition.x * Map.CellSize, _gridPosition.y * Map.CellSize, 0);

    }
}
