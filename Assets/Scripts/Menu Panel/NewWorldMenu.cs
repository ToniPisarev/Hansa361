using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class NewWorldMenu : MonoBehaviour {

    public int sceneToStart = 1; //Index number in build settings of scene to load
    public int kingdomsizeValue = 4;

    public GameObject kingdomText;
    public GameObject kingdomSlider;
    public GameObject worldNameField;

    public void KingdomChanged() {
        Text kingdomTextComponent = kingdomText.GetComponent<Text>();
        kingdomsizeValue = (int)kingdomSlider.GetComponent<Slider>().value;
        kingdomTextComponent.text = kingdomsizeValue + " Kingdoms";
    }

    public void CreateButtonClicked() {
        string worldname = worldNameField.GetComponent<InputField>().text;
        if (worldname.Length > 0) {
            ErrorWindow.ShowErrorWindow("World Created");
            WorldInformation.CurrentWorldID = "000001";
            WorldInformation.KingdomCount = (int)kingdomSlider.GetComponent<Slider>().value;

            GameObject.Find("/GameInformation").GetComponent<WorldInformation>().StartWorld();

            /*WWWForm form = new WWWForm(); //here you create a new form connection
            form.AddField("kingdomCount", );
            form.AddField("userID", WorldInformation.UserID);
            form.AddField("name", worldname);
            WWW w = new WWW(URL, form); //here we create a var called 'w' and we sync with our URL and the form
            StartCoroutine(CreateNewWorld(w));
            */

        } else {
            // empty character name - error window is shown
            ErrorWindow.ShowErrorWindow("Empty World name");
        }
    }


}
