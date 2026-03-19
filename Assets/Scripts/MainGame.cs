using UnityEngine;

public class MainGame : MonoBehaviour
{
    public static MainGame Instance;

    public Map map;
    public Player Player;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetObject(int x, int y)
    {
        if (x >= 0 && x < Map.grid.GetLength(0) &&
            y >= 0 && y < Map.grid.GetLength(1))
        {
            return Map.grid[x, y];
        }

        return null;
    }

    public void SetObject(int x, int y, GameObject obj) => Map.grid[x, y] = obj;
}
