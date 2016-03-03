
using UnityEngine;
using System.Collections;

public class CreateNewCharacter : MonoBehaviour {

    private BasePlayer newPlayer;
    private bool isMageClass;
    private bool isWarriorClass;
    private bool isThiefClass;
    private bool isArcherClass;
    private string playerName = "Enter Name";

    // Use this for initialization
    void Start () {
        newPlayer = new BasePlayer();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        playerName = GUILayout.TextArea(playerName,20);
        isMageClass = GUILayout.Toggle(isMageClass, "Mage Class");
        isWarriorClass = GUILayout.Toggle(isWarriorClass, "Warrior Class");
        isThiefClass = GUILayout.Toggle(isThiefClass, "Thief Class");
        isArcherClass = GUILayout.Toggle(isArcherClass, "Archer Class");
        if (GUILayout.Button("Create"))
        {
            newPlayer.PlayerName = playerName;
            if (isMageClass)
            {
                newPlayer.PlayerClass = new BaseMageClass();
            }
            else if (isWarriorClass)
            {
                newPlayer.PlayerClass = new BaseWarriorClass();
            }
            else if (isThiefClass)
            {
                newPlayer.PlayerClass = new BaseThiefClass();
            }
            else if (isArcherClass)
            {
                newPlayer.PlayerClass = new BaseArcherClass();
            }


            newPlayer.PlayerLevel = 1;
            newPlayer.Strength = newPlayer.PlayerClass.Strength;
            newPlayer.Agility = newPlayer.PlayerClass.Agility;
            newPlayer.Intellect = newPlayer.PlayerClass.Intellect;
            newPlayer.Defense = newPlayer.PlayerClass.Defense;
            StoreNewPlayerInfo();
            SaveInformation.SaveAllInformation();

            Debug.Log("player name : "+ newPlayer.PlayerName);
            Debug.Log("player class: " + newPlayer.PlayerClass.CharacterClassName);
            Debug.Log("player level: " + newPlayer.PlayerLevel);
            Debug.Log("player strength: " + newPlayer.Strength);
            Debug.Log("player agility: " + newPlayer.Agility);
            Debug.Log("player defense: " + newPlayer.Defense);
            Debug.Log("player intellect: " + newPlayer.Intellect);
        }
        if (GUILayout.Button("Load"))
        {
            Application.LoadLevel("loadTest");
        }
    }


    private void StoreNewPlayerInfo()
    {
        GameInformation.PlayerName = newPlayer.PlayerName;
        GameInformation.PlayerLevel = newPlayer.PlayerLevel;
        GameInformation.Strength = newPlayer.Strength;
        GameInformation.Agility = newPlayer.Agility;
        GameInformation.Intellect = newPlayer.Intellect;
        GameInformation.Defense = newPlayer.Defense;
    }
}
