using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChoice : MonoBehaviour
{
    public GameObject weaponSword;
    public GameObject weaponBow;
    public GameObject weaponMagic;

    public static WeaponChoice current;

    private void Awake()
    {
        current = this;
    }

    public void Choice(Weapon.WeaponType type)
    {
        weaponSword.SetActive(false);weaponBow.SetActive(false);weaponMagic.SetActive(false);
        switch (type)
        {
            case Weapon.WeaponType.Sword:weaponSword.SetActive(true);break;
            case Weapon.WeaponType.Bow:weaponBow.SetActive(true);break;
            case Weapon.WeaponType.Magic:weaponMagic.SetActive(true);break;
        }
    }
}
