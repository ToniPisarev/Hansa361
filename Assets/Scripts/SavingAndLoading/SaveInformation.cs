using UnityEngine;
using System.Collections;

public class SaveInformation {

    public static void SaveAllInformation() {
        
        //Saves All player information into local folder!

        PlayerPrefs.SetInt("GOLD", GameInformation.Gold);

        if (GameInformation.PlayerInventory != null) {
            PlayerPref.Save("PLAYERINVENTORY", GameInformation.PlayerInventory);
        }
        if (GameInformation.PlayerQuestLog != null) {
            PlayerPref.Save("PLAYERQUESTLOG", GameInformation.PlayerQuestLog);
        }
        if (GameInformation.PlayerCharacter != null) {
            PlayerPref.Save("PLAYERCHARACTER", GameInformation.PlayerCharacter);
        }
        if (GameInformation.Char1 != null) {
            PlayerPref.Save("CHAR01", GameInformation.Char1);
        }
        if (GameInformation.Char2 != null) {
            PlayerPref.Save("CHAR02", GameInformation.Char2);
        }
        if (GameInformation.Char3 != null) {
            PlayerPref.Save("CHAR03", GameInformation.Char3);
        }
        if (GameInformation.Char4 != null) {
            PlayerPref.Save("CHAR04", GameInformation.Char4);
        }
        if (GameInformation.Char5 != null) {
            PlayerPref.Save("CHAR05", GameInformation.Char5);
        }
        Debug.Log("SAVED ALL INFO!!");
    }
}
