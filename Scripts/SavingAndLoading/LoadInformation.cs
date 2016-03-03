using UnityEngine;
using System.Collections;

public class LoadInformation {
    
    
    public static void LoadAllInformation()
    {
        
        GameInformation.PlayerName = PlayerPrefs.GetString("PLAYERNAME");
        GameInformation.PlayerLevel = PlayerPrefs.GetInt("PLAYERLEVEL");
        GameInformation.Strength = PlayerPrefs.GetInt("STRENGTH");
        GameInformation.Intellect = PlayerPrefs.GetInt("INTELLECT");
        GameInformation.Agility = PlayerPrefs.GetInt("AGILITY");
        GameInformation.Defense = PlayerPrefs.GetInt("DEFENSE");
        
        if(PlayerPrefs.GetString("EQUIPMENT1") != null)
        {
            GameInformation.EquipmentOne = (BaseEquipment) PPSerialization.Load("EQUIPMENT1");
        }
    }
	
}
