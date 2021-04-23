using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager current;
    public WeaponList list;
    public int currentWeaponNum;

    private void Awake()
    {
        current = this;
    }
    public void Equip(string weaponName)
    {
        currentWeaponNum = -1;
        for (int i = 0; i < list.weaponList.Count; i++)
        {
            if (weaponName == list.weaponList[i].name)
            {
                currentWeaponNum = list.weaponList.IndexOf(list.weaponList[i]);
                SaveData.current.profile.weapon = weaponName;
                WeaponChoice.current.Choice(CurrentWeapon().type);
            }
        }
    }

    public Weapon CurrentWeapon()
    {
        return list.weaponList[currentWeaponNum];
    }
}
