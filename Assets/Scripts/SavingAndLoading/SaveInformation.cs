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
        if (GameInformation.SideCharacters != null) {
            PlayerPref.Save("SIDECHARACTERS", GameInformation.SideCharacters);
        }
        
            
        Debug.Log("SAVED ALL INFO!!");
    }
}
