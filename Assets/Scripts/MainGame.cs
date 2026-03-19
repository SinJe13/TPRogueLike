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
        return Map.grid[x, y];
    }

    public void SetObject(int x, int y, GameObject obj) => Map.grid[x, y] = obj;
}
