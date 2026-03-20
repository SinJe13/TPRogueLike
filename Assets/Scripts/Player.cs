using Assets.Scripts;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    Vector2Int _gridPosition = new Vector2Int(0, 0);

    [SerializeField] private GameObject PNJInteractionUI;
    [SerializeField] private GameObject NoMoneyUI;
    private bool isNearPNJ = false;

    private int _level = 1;
    private int _xp = 0;
    private int _xpToNextLevel = 10;
    private int _gold = 5;

    private void Start()
    {
        _gridPosition = (Vector2Int) GridManager.Instance.WorldToCell(transform.position);
        transform.position = GridManager.Instance.CellToWorld((Vector3Int)_gridPosition);

        HP = 50;
        Armor = 2;
        Strenght = 10;
    }

    void Update()
    {
        Vector2Int coordPlayer = _gridPosition;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(1, 0, coordPlayer);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(-1, 0, coordPlayer);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(0, -1, coordPlayer);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(0, 1, coordPlayer);
        }

        if (isNearPNJ && Input.GetKeyDown(KeyCode.Return)) TryBuyHeal();

        //// Vérification obstacle
        //if (coordPlayer.x >= 0 && coordPlayer.x < 27 &&
        //    coordPlayer.y >= 0 && coordPlayer.y < 15 &&
        //    !Map.grid[coordPlayer.x, coordPlayer.y])
        //{
        //    _gridPosition = coordPlayer;

        //    transform.position = GridManager.Instance.CellToWorld((Vector3Int) _gridPosition);
        //}

    }

    private void Move(int dirX, int dirY, Vector2Int coordPlayer)
    {
        coordPlayer = _gridPosition + new Vector2Int(dirX, dirY);

        GameObject obj = MainGame.Instance.GetObject(coordPlayer.x, coordPlayer.y);

        if (coordPlayer.x >= 0 && coordPlayer.x < Map.grid.GetLength(0) &&
            coordPlayer.y >= 0 && coordPlayer.y < Map.grid.GetLength(1) &&
            obj == null)
        {
            _gridPosition = coordPlayer;

            transform.position = GridManager.Instance.CellToWorld((Vector3Int)_gridPosition);
        }

        if( obj != null && obj.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Fight(enemy);
            return;
        }

        if (obj != null && obj.CompareTag("PNJ"))
        {
            isNearPNJ = true;
            PNJInteractionUI.SetActive(true);
        }
        else
        {
            isNearPNJ = false;
            PNJInteractionUI.SetActive(false);
            NoMoneyUI.SetActive(false);
        }

        if (obj != null && obj.CompareTag("Potion"))
        {
            HP = Mathf.Min(HP + 20, 100);
            Destroy(obj);
            MainGame.Instance.SetObject(coordPlayer.x, coordPlayer.y, null);
            return;
        }

        if (obj != null && obj.CompareTag("Gold"))
        {
            AddGold(10);
            Destroy(obj);
            MainGame.Instance.SetObject(coordPlayer.x, coordPlayer.y, null);
            return;
        }

        if (obj != null && obj.CompareTag("Vase"))
        {
            Vector3Int currentPot = new Vector3Int(coordPlayer.x, coordPlayer.y);
            currentPot.x += dirX;
            currentPot.y += dirY;

            Debug.Log(currentPot.ToString());
            Debug.Log(coordPlayer.ToString());

            if (currentPot.x >= 0 && currentPot.y >= 0 && currentPot.x < 100 && currentPot.y < 100 && MainGame.Instance.GetObject(currentPot.x, currentPot.y) == null)
            {
                MainGame.Instance.SetObject(coordPlayer.x, coordPlayer.y, null);
                obj.transform.position = GridManager.Instance.CellToWorld(currentPot);
                MainGame.Instance.SetObject(currentPot.x, currentPot.y, obj);
            }
            else Debug.Log("Impossible de pousser");
            return;

            //int vaseNewX = coordPlayer.x + dirX;
            //int vaseNewY = coordPlayer.y + dirY;
            //Vector2Int VaseGridPosition = new Vector2Int(vaseNewX, vaseNewY);
            //Vector3Int CellPosition = GridManager.Instance.WorldToCell((Vector3Int)VaseGridPosition);
            //CellPosition.x += (int)(Map.CellSize / 2);
            //CellPosition.y += (int)(Map.CellSize / 2);

            //if (vaseNewX >= 0 && vaseNewY >= 0 && vaseNewX < 100 && vaseNewY < 100 && MainGame.Instance.GetObject(vaseNewX, vaseNewY) == null)
            //{
            //    MainGame.Instance.SetObject(coordPlayer.x, coordPlayer.y, null);
            //    obj.transform.position = GridManager.Instance.CellToWorld(CellPosition);
            //    MainGame.Instance.SetObject(CellPosition.x, CellPosition.y, obj);
            //}
            //else Debug.Log("Impossible de pousser");
            //return;
        }

        if (obj != null && obj.CompareTag("Vortex"))
        {
            TeleportToOtherVortex(coordPlayer.x, coordPlayer.y);
            return;
        }
    }

    private void Fight(Enemy enemy)
    {
        //Debug.Log($"1 {Strenght} 2 {Armor} 3 {enemy.GetStrenghtPoints()} 4 {enemy.GetArmorPoints()}");

        int damageToEnemy = Mathf.Max(0, Strenght - enemy.GetArmorPoints());
        int damageToPlayer = Mathf.Max(0, enemy.GetStrenghtPoints() - Armor);

        //Debug.Log($"Combat: {gameObject.name} inflige {damageToEnemy} degats a {enemy.gameObject.name}");
        enemy.Hit(damageToEnemy);

        if (enemy.GetHP() > 0)
        {
            //Debug.Log($"{enemy.gameObject.name} contre-attaque et inflige {damageToPlayer} degats");
            this.Hit(damageToPlayer);
        }
        else GainExperience(5);
    }

    void GainExperience(int xpGained)
    {
        if (_level >= 5) return;

        _xp += xpGained;
        Debug.Log($"XP gagner : {xpGained}, XP total : {_xp}/{_xpToNextLevel}");

        if (_xp >= _xpToNextLevel) LevelUp();
    }

    void LevelUp()
    {
        if (_level >= 5) return;

        _level++;
        _xp = 0;
        _xpToNextLevel += 10;

        HP += 10;
        Strenght += 2;

        Debug.Log($"Niveau {_level} atteint ! PV max : {HP} - Attaque : {Strenght}");
    }

    private void AddGold(int amount)
    {
        _gold += amount;
        Debug.Log($"Or actuel : {_gold}");
    }

    void Heal(int cost, int healAmount)
    {
        if (_gold >= cost)
        {
            _gold -= cost;
            HP = Mathf.Min(HP + healAmount, 100);
            Debug.Log($"Soigner de {healAmount} PV. PV actuels : {HP} - Or actuel : {_gold}");
        }
        else TryBuyHeal();
    }

    private void TryBuyHeal()
    {
        if (_gold >= 5)
        {
            Heal(5, 20);
            PNJInteractionUI.SetActive(false);
            NoMoneyUI.SetActive(false);
        }
        else NoMoneyUI.SetActive(true);
    }

    void TeleportToOtherVortex(int x, int y)
    {
        List<Vector2Int> vortexList = Map.Instance.Vortex;
        Vector2Int currentVortex = new Vector2Int(x, y);
        Vector2Int destinationVortex = (vortexList[0] == currentVortex) ? vortexList[1] : vortexList[0];

        Debug.Log($"teleportation de ({x}, {y}) à ({destinationVortex.x}, {destinationVortex.y})");
        Vector2Int TP = new Vector2Int(destinationVortex.x, destinationVortex.y);

        _gridPosition = TP;
        transform.position = GridManager.Instance.CellToWorld((Vector3Int)TP);
    }
}