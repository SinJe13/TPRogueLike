using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Enemy : Character
    {
        public void Initialize(int x, int y, int hp, int strenght, int armor)
        {
            HP = hp;
            Strenght = strenght;
            Armor = armor;
            //Vector3Int EnemyPosition = new Vector3Int(x, y);
            //transform.position = GridManager.Instance.CellToWorld(EnemyPosition);
        }
        protected override void Die()
        {
            Debug.Log($"{gameObject.name} est mort");

            Vector2Int gridPosition = (Vector2Int)GridManager.Instance.WorldToCell(transform.position);

            MainGame.Instance.SetObject(gridPosition.x, gridPosition.y, null);

            //GenerateLoot(gridPosition.x, gridPosition.y);

            Destroy(gameObject);
        }

        private void GenerateLoot(int x, int y)
        {
            float randomValue = Random.value;
            GameObject loot = null;

            if (randomValue < 0.3f)
            {
                //loot = Instantiate(Map.Instance.PrefabPotion, new Vector3(x * MainGame.Instance.CellSize, y * MainGame.Instance.CellSize, 0), Quaternion.identity);
                //loot.tag = "Potion";
            }
            else if (randomValue < 0.9f)
            {
                //loot = Instantiate(MainGame.Instance.PrefabGold, new Vector3(x * MainGame.Instance.CellSize, y * Map.Instance.CellSize, 0), Quaternion.identity);
                //loot.tag = "Gold";
            }
            if (loot != null) MainGame.Instance.SetObject(x, y, loot);
        }
    }
}