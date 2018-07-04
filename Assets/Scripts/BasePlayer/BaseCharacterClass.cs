using UnityEngine;
using System.Collections;
[System.Serializable]

//The character class class
public class BaseCharacterClass {

    public enum CharacterClasses {
        Squire,
        Apprentice,
        Thief,
        Archer,

        Knight,
        Mage,
        Ninja,
        Hunter,

        Paladin,
        ArchMage,
        Assassin,
        Sniper

    }

    //Getter functions
    public CharacterClasses CharacterClassName { get; set; }
    public string CharacterClassDescription { get; set; }

}
