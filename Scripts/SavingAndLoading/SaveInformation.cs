using UnityEngine;
using System.Collections;

public class SaveInformation {

    public static void SaveAllInformation()
    {
        PlayerPrefs.SetInt("PLAYERLEVEL", GameInformation.PlayerLevel);
        PlayerPrefs.SetString("PLAYERNAME",GameInformation.PlayerName);
        PlayerPrefs.SetInt("STRENGTH", GameInformation.Strength);
        PlayerPrefs.SetInt("AGILITY", GameInformation.Agility);
        PlayerPrefs.SetInt("INTELLECT", GameInformation.Intellect);
        PlayerPrefs.SetInt("DEFENSE", GameInformation.Defense);
        if(GameInformation.EquipmentOne != null)       
            PPSerialization.Save("EQUIPMENT1", GameInformation.EquipmentOne);
        Debug.Log("SAVED ALL INFO!!");
    }

    //create simpler static functions that save after certain state changes


	
}
