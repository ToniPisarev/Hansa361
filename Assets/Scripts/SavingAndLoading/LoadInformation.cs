using UnityEngine;
using System.Collections;

public class LoadInformation {

    public static void LoadAllInformation() {

        //LOADS ALL INFORMATION FROM LOCAL FOLDER

        GameInformation.Gold = PlayerPrefs.GetInt("GOLD");

        if (PlayerPrefs.GetString("PLAYERINVENTORY") != null) {
            GameInformation.PlayerInventory = (Inventory)PlayerPref.Load("PLAYERINVENTORY");
        }
        if (PlayerPrefs.GetString("PLAYERQUESTLOG") != null) {
            GameInformation.PlayerQuestLog = (QuestLog)PlayerPref.Load("PLAYERQUESTLOG");
        }
        if (PlayerPrefs.GetString("PLAYERCHARACTER") != null) {
            GameInformation.PlayerCharacter = (BaseCharacter)PlayerPref.Load("PLAYERCHARACTER");
        }
        if (PlayerPrefs.GetString("SIDECHARACTERS") != null) {
            GameInformation.SideCharacters = (BaseCharacter [])PlayerPref.Load("SIDECHARACTERS");
        }     
    }
}
