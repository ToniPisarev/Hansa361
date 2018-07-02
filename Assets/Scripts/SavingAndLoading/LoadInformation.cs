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
        if (PlayerPrefs.GetString("CHAR01") != null) {
            GameInformation.Char1 = (BaseCharacter)PlayerPref.Load("CHAR01");
        }
        if (PlayerPrefs.GetString("CHAR02") != null) {
            GameInformation.Char2 = (BaseCharacter)PlayerPref.Load("CHAR02");
        }
        if (PlayerPrefs.GetString("CHAR03") != null) {
            GameInformation.Char3 = (BaseCharacter)PlayerPref.Load("CHAR03");
        }
        if (PlayerPrefs.GetString("CHAR04") != null) {
            GameInformation.Char4 = (BaseCharacter)PlayerPref.Load("CHAR04");
        }
        if (PlayerPrefs.GetString("CHAR05") != null) {
            GameInformation.Char5 = (BaseCharacter)PlayerPref.Load("CHAR05");
        }
    }
}
