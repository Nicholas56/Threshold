using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyList enemies;
    public static EnemyManager current;
    EnemyCharacter currentEnemy;
    private void Awake()
    {
        current = this;
    }

    public Enemy GetEnemy(int id) { return enemies.enemies[id]; }
    public Enemy GetEnemy(string name) 
    {
        for (int i = 0; i < enemies.enemies.Count; i++)
        {
            if (name == enemies.enemies[i].name) { return enemies.enemies[i]; }
        }
        return null;
    }
    public void SetCurrentEnemy(EnemyCharacter current) { currentEnemy = current; }
    public EnemyCharacter Enemy() { return currentEnemy; }

    public void DealDamageToPlayer(Enemy enemy)
    {
        if (SaveData.current.profile.armor > 0)
        {
            SaveData.current.profile.armor = Mathf.Max(0, SaveData.current.profile.armor - ArmorDamageMultiplier(enemy));
        }
        else
        {
            SaveData.current.profile.health = Mathf.Max(0, SaveData.current.profile.health - enemy.attack);
        }
    }

    int ArmorDamageMultiplier(Enemy enemy)
    {
        float altDamage = enemy.attack;
        switch (enemy.type)
        {
            case global::Enemy.AttackType.Physical:
                if(SkillManager.current.CheckPlayerHasSkill("Heavy Armor"))
                {
                    altDamage -= SaveData.current.profile.GetSkill("Heavy Armor").GetLevel();
                }
                if (SkillManager.current.CheckPlayerHasSkill("Light Armor"))
                {
                    altDamage -= SaveData.current.profile.GetSkill("Light Armor").GetLevel() * 0.5f;
                }
                break;
            case global::Enemy.AttackType.Magical:
                if (SkillManager.current.CheckPlayerHasSkill("Magical Armor"))
                {
                    altDamage -= SaveData.current.profile.GetSkill("Magical Armor").GetLevel();
                }
                if (SkillManager.current.CheckPlayerHasSkill("Light Armor"))
                {
                    altDamage -= SaveData.current.profile.GetSkill("Light Armor").GetLevel() * 0.5f;
                }
                break;
        }
        return Mathf.Max(1, Mathf.FloorToInt(altDamage));
    }
}
