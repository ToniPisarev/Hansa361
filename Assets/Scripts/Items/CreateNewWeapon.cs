using UnityEngine;
using System.Collections;

//CREATES NEW WEAPON -- INITIALIZES ALL PROPERTIES -- Randomizes based on level
public class CreateNewWeapon {

    private BaseWeapon newWeapon;

    //can assign names here!
    private int itemRarity = 1;
    private int itemType = 0;
    private int price = 1;
    private string[] spellEffects = new string[6] { "Water", "Ice", "Wind", "Fire", "Lightning", "Darkness" };

    void Start() {
        //test create weapon
        //CreateWeapon();
        //Debug.Log(newWeapon.ItemName);
        //Debug.Log(newWeapon.ItemDescription);
        //Debug.Log(newWeapon.WeaponType.ToString());
        //Debug.Log(newWeapon.ItemID.ToString());
        //Debug.Log(newWeapon.Strength.ToString());
    }

    public BaseWeapon returnWeapon() {

        CreateWeapon(GameInformation.PlayerCharacter.PlayerLevel, 0);
        //Debug.Log(newWeapon.ItemName);
        return newWeapon;
    }

    public BaseWeapon ReturnLeveledWeapon(int level, int type) {
        //type:: 1 is sword, 2 is spear, 3 is tomb, 4 is bow, 5 is dagger
        CreateWeapon(level, type);
        return newWeapon;
    }

    //Creates a new weapon object 
    private void CreateWeapon(int level, int type) {
        newWeapon = new BaseWeapon();

        newWeapon.ItemDescription = "A formidable weapon...";

        DetermineRarity(level);
        DetermineStats();
        ChooseWeaponType(type);

        int rand = Random.Range(1, 10);
        //status effect (element)
        if ((rand > 8 && itemType == 2) || (itemType != 2 && rand > 7)) {
            newWeapon.SpellEffectID = Random.Range(1, 5);
            price = price + 50;
        } else {
            newWeapon.SpellEffectID = 0;
        }

        DetermineValue();
        DeterminePrice();
        DetermineName();
    }

    private void DeterminePrice() {
        price = price + 2 * (newWeapon.Strength + newWeapon.Agility + newWeapon.Defense + newWeapon.Intellect);
        newWeapon.Price = 2 * newWeapon.Value + price;
    }

    private void DetermineName() {
        newWeapon.ItemName = newWeapon.ItemRarity.ToString() + " " + newWeapon.WeaponType.ToString();
        if (newWeapon.SpellEffectID != 0) {
            newWeapon.ItemName = newWeapon.ItemName + " of " + spellEffects[newWeapon.SpellEffectID];
        }
    }

    private void DetermineStats() {
        if (itemRarity < 3) {
            newWeapon.Strength = Random.Range(1, 10);
            newWeapon.Intellect = Random.Range(1, 10);
            newWeapon.Agility = Random.Range(1, 10);
            newWeapon.Defense = Random.Range(1, 10);
        } else if (itemRarity < 5) {
            newWeapon.Strength = Random.Range(10, 20);
            newWeapon.Intellect = Random.Range(10, 20);
            newWeapon.Agility = Random.Range(10, 20);
            newWeapon.Defense = Random.Range(10, 20);
        } else {
            newWeapon.Strength = Random.Range(20, 30);
            newWeapon.Intellect = Random.Range(20, 30);
            newWeapon.Agility = Random.Range(20, 30);
            newWeapon.Defense = Random.Range(20, 30);
        }
    }

    private void DetermineRarity(int level) {
        int temp = Random.Range(1, 10);
        if (level / 25 > temp) {
            newWeapon.ItemRarity = BaseStatItem.ItemRaritys.Legendary;
            itemRarity = 5;
        } else if (level / 18 > temp) {
            newWeapon.ItemRarity = BaseStatItem.ItemRaritys.Flawless;
            itemRarity = 4;
        } else if (level / 10 > temp) {
            newWeapon.ItemRarity = BaseStatItem.ItemRaritys.Great;
            itemRarity = 3;
        } else if (level / 4 > temp) {
            newWeapon.ItemRarity = BaseStatItem.ItemRaritys.Common;
            itemRarity = 2;
        } else {
            newWeapon.ItemRarity = BaseStatItem.ItemRaritys.Rusty;
            itemRarity = 1;
        }
    }

    private void DetermineValue() {
        int multiplier = 1;
        if (newWeapon.SpellEffectID != 0) {
            multiplier = 3;
        }
        if (itemType == 1 || itemType == 2) {
            newWeapon.Value = itemRarity * Random.Range(10, 15) * multiplier;
        } else if (itemType == 3) {
            newWeapon.Value = itemRarity * Random.Range(13, 18) * multiplier;
        } else { newWeapon.Value = itemRarity * Random.Range(7, 12) * multiplier; }

    }

    private void ChooseWeaponType(int type) {
        int random;
        if (type == 0) { random = Random.Range(2, 5); } else { random = type; }
        itemType = random;
        if (random == 1) {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.Sword;
        } else if (random == 2) {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.Spear;
        } else if (random == 3) {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.Tomb;
        }
        if (random == 4) {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.Bow;
        }
        if (random == 5) {
            newWeapon.WeaponType = BaseWeapon.WeaponTypes.Dagger;
        }
    }
}
