using UnityEngine;

public class MainGame : MonoBehaviour
{
    public static MainGame Instance;

    public Map map;
    public PlayerMove Player;

    private void Awake()
    {
        Instance = this;
    }

    public void SetObject(int x, int y) => Map.grid[x, y] = true;
}
