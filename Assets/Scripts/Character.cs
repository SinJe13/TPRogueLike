using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviour
{
    protected int HP;
    protected int Armor;
    protected int Strenght;

    public int GetHP() => HP;
    public int GetArmorPoints() => Armor;
    public int GetStrenghtPoints() => Strenght;

    public void Hit(int damage)
    {
        int actualDamage = Mathf.Max(0, damage - Armor);
        HP -= actualDamage;
        Debug.Log($"{gameObject.name} prend {actualDamage} degats. PV restants : {HP}");

        if (HP <= 0) Die();
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} est mort");
        Destroy(gameObject);
    }
}
