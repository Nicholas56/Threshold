using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat current;
    Animator anim;
    float timer = 2;

    private void Awake()
    {
        current = this;
        anim = GetComponent<Animator>();
    }

    public bool IsInRange(float distance)
    {
        if (distance < WeaponManager.current.CurrentWeapon().weaponRange) { return true; }
        else return false;
    }

    private void Update()
    {
        if (Time.time > timer)
        {
            timer = Time.time + 2;
            Collider[] cols = Physics.OverlapSphere(transform.position, 10);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].GetComponent<EnemyCharacter>())
                {
                    cols[i].GetComponent<EnemyCharacter>().BeginCombat();
                }
            }
        }
    }
    public void StartCombat()
    {
        StartCoroutine("Combat");
    }
    public void EndCombat()
    {
        StopCoroutine("Combat");
        anim.SetTrigger("CombatEnd");
    }

    IEnumerator Combat()
    {
        if (EnemyManager.current.Enemy() == null) { EndCombat(); }
        yield return new WaitForSeconds(WeaponManager.current.CurrentWeapon().cooldown);
        //Play attack animation (dependig on weapon)
        anim.SetTrigger("Attack");
        EnemyManager.current.Enemy().ChangeHealth(-Damage());

        StartCombat();
    }

    int Damage()
    {
        switch (WeaponManager.current.CurrentWeapon().type)
        {
            case Weapon.WeaponType.Sword:
                if (SaveData.current.profile.GetSkill("Swordsmanship").GetLevel() > 0) 
                {
                    return WeaponManager.current.CurrentWeapon().damage + SaveData.current.profile.GetSkill("Swordsmanship").GetLevel();
                }
                break;
            case Weapon.WeaponType.Bow:
                if (SaveData.current.profile.GetSkill("Archery").GetLevel() > 0)
                {
                    return WeaponManager.current.CurrentWeapon().damage + SaveData.current.profile.GetSkill("Archery").GetLevel();
                }
                break;
            case Weapon.WeaponType.Magic:
                if (SaveData.current.profile.GetSkill("Magic").GetLevel() > 0)
                {
                    return WeaponManager.current.CurrentWeapon().damage + SaveData.current.profile.GetSkill("Magic").GetLevel();
                }
                break;
        }
        return WeaponManager.current.CurrentWeapon().damage;
    }
    
    public void ChooseAttackAnimation()
    {
        anim.SetBool("Sword", false); anim.SetBool("Bow", false); anim.SetBool("Magic", false);
        switch (WeaponManager.current.CurrentWeapon().type)
        {
            case Weapon.WeaponType.Sword: anim.SetBool("Sword", true); break;
            case Weapon.WeaponType.Bow: anim.SetBool("Bow", true); break;
            case Weapon.WeaponType.Magic: anim.SetBool("Magic", true); break;
        }
    }
}
