using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Player : Character
{
    Vector2Int _gridPosition = new Vector2Int(0, 0);

    [SerializeField] private GameObject PNJInteractionUI;
    [SerializeField] private GameObject NoMoneyUI;
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
        Armor = 10;
        Strenght = 2;
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
        
        if (coordPlayer.x >= 0 && coordPlayer.x < 27 &&
            coordPlayer.y >= 0 && coordPlayer.y < 15 &&
            !Map.grid[coordPlayer.x, coordPlayer.y])
        {
            _gridPosition = coordPlayer;

            transform.position = GridManager.Instance.CellToWorld((Vector3Int)_gridPosition);
        }
    }
}