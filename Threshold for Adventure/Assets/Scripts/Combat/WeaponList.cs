using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponList")]
public class WeaponList : ScriptableObject
{
    public List<Weapon> weaponList;
}

[System.Serializable]
public class Weapon
{
    public enum WeaponType { None, Sword, Bow, Magic }
    public WeaponType type;
    public string name;
    public float weaponRange;
    public int damage;
    public float cooldown;
}
