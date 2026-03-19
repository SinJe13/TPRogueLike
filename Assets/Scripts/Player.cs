using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Player : Character
{
    Vector2Int _gridPosition = new Vector2Int(0, 0);

    //[SerializeField] private GameObject PNJInteractionUI;
    //[SerializeField] private GameObject NoMoneyUI;
    private bool isNearPNJ = false;

    private int _level = 1;
    private int _xp = 0;
    private int _xpToNextLevel = 10;
    private int _gold = 0;

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


    }

    private void Fight(Enemy enemy)
    {
        Debug.Log($"1 {Strenght} 2 {Armor} 3 {enemy.GetStrenghtPoints()} 4 {enemy.GetArmorPoints()}");

        int damageToEnemy = Mathf.Max(0, Strenght - enemy.GetArmorPoints());
        int damageToPlayer = Mathf.Max(0, enemy.GetStrenghtPoints() - Armor);

        Debug.Log($"Combat: {gameObject.name} inflige {damageToEnemy} degats a {enemy.gameObject.name}");
        enemy.Hit(damageToEnemy);

        if (enemy.GetHP() > 0)
        {
            Debug.Log($"{enemy.gameObject.name} contre-attaque et inflige {damageToPlayer} degats");
            this.Hit(damageToPlayer);
        }
        //else GainExperience(5);
    }
}