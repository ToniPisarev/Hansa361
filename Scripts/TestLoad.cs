using UnityEngine;
using System.Collections;

public class TestLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LoadInformation.LoadAllInformation();
        Debug.Log("player name : "+ GameInformation.PlayerName);
        //Debug.Log("player class: " + newPlayer.PlayerClass.CharacterClassName);
        Debug.Log("player class: " + GameInformation.PlayerLevel);
        Debug.Log("player class: " + GameInformation.Strength);
        Debug.Log("player class: " + GameInformation.Agility);
        Debug.Log("player class: " + GameInformation.Defense);
        Debug.Log("player class: " + GameInformation.Intellect);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
