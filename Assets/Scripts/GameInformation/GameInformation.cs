using UnityEngine;
using System.Collections;

public class GameInformation : MonoBehaviour {

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    public enum PlayerMapStates {
        Travelling,
        Idle
    }

    //The players status in the world
    public static PlayerMapStates PlayerMapState { get; set; }

    //Store player important data
    public static int Gold { get; set; }
    public static Inventory PlayerInventory { get; set; }
    public static QuestLog PlayerQuestLog { get; set; }

    //Store all character info!
    public static BaseCharacter PlayerCharacter { get; set; }
    public static BaseCharacter [] SideCharacters { get; set; }

    //Set characters stats
    public static void SetCharacterStats(BaseCharacter character) {

        if (character.PlayerClass == BaseCharacterClass.CharacterClasses.Apprentice) {
            //Set apprentice stats
            character.Strength = 1;
            character.Agility = 2;
            character.Intellect = 4;
            character.Defense = 3;
        } else if (character.PlayerClass == BaseCharacterClass.CharacterClasses.Squire) {
            //Set squire stats
            character.Strength = 4;
            character.Agility = 2;
            character.Intellect = 1;
            character.Defense = 3;
        } else if (character.PlayerClass == BaseCharacterClass.CharacterClasses.Thief) {
            //Set thief stats
            character.Strength = 3;
            character.Agility = 4;
            character.Intellect = 2;
            character.Defense = 1;
        } else if (character.PlayerClass == BaseCharacterClass.CharacterClasses.Archer) {
            //Set archer stats
            character.Strength = 3;
            character.Agility = 3;
            character.Intellect = 2;
            character.Defense = 2;    
        } else if (character.PlayerClass == BaseCharacterClass.CharacterClasses.Knight) {
            //Set knight stats
            character.Strength = 8;
            character.Agility = 4;
            character.Intellect = 2;
            character.Defense = 6;
        } else if (character.PlayerClass == BaseCharacterClass.CharacterClasses.Hunter) {
            //Set Hunter stats
            character.Strength = 6;
            character.Agility = 7;
            character.Intellect = 4;
            character.Defense = 3;
        } else if (character.PlayerClass == BaseCharacterClass.CharacterClasses.Sniper) {
            //Set sniper stats
            character.Strength = 8;
            character.Agility = 10;
            character.Intellect = 6;
            character.Defense = 6;
        } else if (character.PlayerClass == BaseCharacterClass.CharacterClasses.Ninja) {
            //Set ninja stats
            character.Strength = 4;
            character.Agility = 11;
            character.Intellect = 2;
            character.Defense = 3;
        } else if (character.PlayerClass == BaseCharacterClass.CharacterClasses.Mage) {
            //Set white mage stats
            character.Strength = 2;
            character.Agility = 4;
            character.Intellect = 8;
            character.Defense = 6;
        } else if (character.PlayerClass == BaseCharacterClass.CharacterClasses.Paladin) {
            //Set paladin stats
            character.Strength = 11;
            character.Agility = 6;
            character.Intellect = 3;
            character.Defense = 10;
        } else if (character.PlayerClass == BaseCharacterClass.CharacterClasses.ArchMage) {
            //Set archmage stats
            character.Strength = 3;
            character.Agility = 6;
            character.Intellect = 14;
            character.Defense = 7;
        } else if (character.PlayerClass == BaseCharacterClass.CharacterClasses.Assassin) {
            //Set assassin stats
            character.Strength = 9;
            character.Agility = 14;
            character.Intellect = 3;
            character.Defense = 4;
        }

        character.CurrentXP = 0;
        character.RequiredXP = character.PlayerLevel * character.PlayerLevel + 7 * character.PlayerLevel + 10;
        character.AvailableStatPoints = 0;

        character.Health = 50 + character.PlayerLevel * 10;
        character.Mana = 50 + character.PlayerLevel * 10;
        character.CurrentHealth = character.Health;

        character.PhysicalDamage = (int)(10 + (float)(character.Strength / 20.00f) * character.Health);
        character.MagicDamage = (int)(10 + (float)(character.Intellect / 20.00f) * character.Health);
        character.MitigatedDamage = (int)(2 + (character.Defense / 20.00f * character.Health));
       
    }
}
