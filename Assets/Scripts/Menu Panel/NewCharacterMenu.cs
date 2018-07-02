using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NewCharacterMenu : MonoBehaviour {
    private BaseCharacter newCharacter;
    private BasePlayer newPlayer;
    public GameObject playernameField;
    public GameObject classDropDown;
    public GameObject newCharacterMenuPanel;
    public GameObject newWorldMenuPanel;

    // Use this for initialization
    void Start() {
        newCharacter = new BaseCharacter();
        newPlayer = new BasePlayer();
    }

    public void NewCharacterButtonClicked() {

        int classChoice = classDropDown.GetComponent<Dropdown>().value; // get value from dropdown
        string playername = playernameField.GetComponent<InputField>().text; // get value from charnamefield

        if (playername.Length > 0) {

            switch (classChoice) {
                case 0: // squire
                    newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Squire;
                    break;
                case 1: // apprentice
                    newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Apprentice;
                    break;
                case 2: // thief
                    newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Thief;
                    break;
                case 3: // archer
                    newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Archer;
                    break;
            }

            // init character
            newCharacter.PlayerLevel = 1;
            newCharacter.PlayerName = playername;
            GameInformation.SetCharacterStats(newCharacter);

            newPlayer.PlayerCharacter = newCharacter;

            //init inventory
            newPlayer.PlayerInventory = new Inventory {
                Weapons = new List<BaseWeapon>(),
                Equipment = new List<BaseEquipment>(),
                Potions = new List<BasePotion>()
            };
            newPlayer.PlayerInventory.printInventory();

            //init quest log
            newPlayer.PlayerQuestLog = new QuestLog {
                CurrentQuests = new List<Quest>(),
                FinishedQuests = new List<Quest>()
            };

            Debug.Log("Init player and character");
            Debug.Log("My character has these stats : (Health-" + newCharacter.CurrentHealth + ") (Damage-" + newCharacter.PhysicalDamage + ") (Magic damage-" + newCharacter.MagicDamage + ") (Mitigation-" + newCharacter.MitigatedDamage + ") (Intellect-" + newCharacter.Intellect + ") (Strength-" + newCharacter.Strength + ") (Defense-" + newCharacter.Defense + ") " + newCharacter.PlayerClass);


            newPlayer.PlayerQuestLog.PrintAllQuests();

            StoreNewPlayerInfo();     

            // enter game
            newCharacterMenuPanel.SetActive(false);
            newWorldMenuPanel.SetActive(true);

        } else {
            // empty character name - error windows is shown
            ErrorWindow.ShowErrorWindow("Enter player name.");
        }
    }

    private void StoreNewPlayerInfo() {
        GameInformation.PlayerCharacter = newCharacter;
        GameInformation.Gold = newPlayer.Gold;
        GameInformation.PlayerInventory = newPlayer.PlayerInventory;
        GameInformation.PlayerQuestLog = newPlayer.PlayerQuestLog;
        SaveInformation.SaveAllInformation();
    }
}