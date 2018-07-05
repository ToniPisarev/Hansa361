using UnityEngine;
using System.Collections;
[System.Serializable]

//Represents a weapon
public class BaseWeapon : BaseStatItem { //BaseWeapon <- BaseStatItem <- BaseItem

    public enum WeaponTypes
    {
        Sword,
        Spear,
        Tomb,
        Bow,
        Dagger
    }

    public WeaponTypes WeaponType { get; set; }
    public int Value { get; set; }
}
