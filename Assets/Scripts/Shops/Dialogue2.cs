using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue2 : MonoBehaviour {

    private Text textComponent;

    //First time add separate dialogue
    public static string[] DialogueStrings = new string[] { "The roads are dangerous and full of terrors", "I can use some help if you are interested in a bit of coin" };

    private bool isEndOfDialogue = false;
    private readonly float SecondsBetweenChars = 0.05f;

    public GameObject shopOption1;
    public GameObject shopOption2;
    public GameObject shopOption3;
    public GameObject questPanel;
    public GameObject text;
    public GameObject shopKeep;
    public GameObject leaveShop;

    private bool init = false;

    // Use this for initialization
    void Start() {
        textComponent = text.GetComponent<Text>();
        textComponent.text = "";
        HideIcons();
    }

    public void ShopQuestkeep() {
        HideIcons();
        if (init) {
            DialogueStrings[0] = "Do I look like a damn shopkeep to you?";
            DialogueStrings[1] = "Go bother my partner if you're lookin to buy";
        }
        textComponent.text = "";

        isEndOfDialogue = false;
        if (!isEndOfDialogue) {
            StartCoroutine(DisplayString(DialogueStrings[0]));
        }
    }

    public void InitShopkeep() {
        HideIcons();

        if (init) {
            DialogueStrings[0] = "Thought a bit more about my offers?";
            DialogueStrings[1] = "It's good pay for good work";
        }

        textComponent.text = "";
        init = true;
        isEndOfDialogue = false;
        shopKeep.SetActive(false);
        questPanel.SetActive(true);

        if (!isEndOfDialogue) {
            StartCoroutine(DisplayString(DialogueStrings[0]));
        }
    }

    public void CloseShopkeep() {
        leaveShop.SetActive(true);
        shopKeep.SetActive(true);
        questPanel.SetActive(false);
    }

    private IEnumerator DisplayString(string StringToDisplay) {
        int stringLength = StringToDisplay.Length;
        int currentCharIndex = 0;

        while (currentCharIndex < stringLength) {
            textComponent.text += StringToDisplay[currentCharIndex];
            currentCharIndex++;

            if (currentCharIndex < stringLength) {
                if (Input.GetKey("space")) {
                    textComponent.text = StringToDisplay;
                    isEndOfDialogue = true;
                    break;
                } else {
                    yield return new WaitForSeconds(SecondsBetweenChars);
                }

            } else {
                isEndOfDialogue = true;
                break;
            }
        }
        ShowOptions();
    }

    private void ShowOptions() {
        shopOption1.SetActive(true);
        shopOption2.SetActive(true);
        shopOption3.SetActive(true);
    }

    private void HideIcons() {
        shopOption1.SetActive(false);
        shopOption2.SetActive(false);
        shopOption3.SetActive(false);

    }
}


