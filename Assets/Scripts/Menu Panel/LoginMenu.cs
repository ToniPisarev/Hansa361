using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using LitJson;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour {

    public GameObject loggedinUsername;
    public GameObject loginMenuPanel;
    public GameObject newCharacterMenuPanel;

    public void LoginButtonClicked() {

        LoadInformation.LoadAllInformation();

        // no characters yet
        if (GameInformation.PlayerCharacter == null) {
            Debug.Log("No data stored yet");
            ErrorWindow.ShowErrorWindow("No player information yet.");
        } else {
            loginMenuPanel.SetActive(false);
            loggedinUsername.SetActive(true);
            loggedinUsername.GetComponent<Text>().text = "Welcome " + GameInformation.PlayerCharacter.PlayerName + "!";
            Debug.Log("Loading previous character - My character has these stats : (Health-" + GameInformation.PlayerCharacter.Health + ") (Damage-" + GameInformation.PlayerCharacter.PhysicalDamage + ") (Magic damage-" + GameInformation.PlayerCharacter.MagicDamage + ") (Mitigation-" + GameInformation.PlayerCharacter.MitigatedDamage + ") (Intellect-" + GameInformation.PlayerCharacter.Intellect + ") (Strength-" + GameInformation.PlayerCharacter.Strength + ") (Defense-" + GameInformation.PlayerCharacter.Defense + ")");
            GameInformation.PlayerQuestLog.PrintAllQuests();
            GameInformation.PlayerInventory.printInventory();

            ErrorWindow.ShowErrorWindow("Logging In");
            GameObject.Find("/GameInformation").GetComponent<WorldInformation>().StartWorld();
        }
    }
}
