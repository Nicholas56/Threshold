using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyList")]
public class EnemyList : ScriptableObject
{
    public List<Enemy> enemies;
}

[System.Serializable]
public class Enemy
{
    public string name;
    public int hitPoints;
    public int attack;
    public float movementSpeed;
    public float attackSpeed;

    public int expReward;
    public enum AttackType { Physical, Magical}
    public AttackType type;
}
