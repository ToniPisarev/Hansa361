using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue1 : MonoBehaviour {

    private Text textComponent;

    //First time add separate dialogue
    public static string[] DialogueStrings = new string[] { "Hello! Welcome to my shop!", "The best goods at a convenient location!", "What do you need?" };

    private bool isEndOfDialogue = false;
    private bool init = false;
    private readonly float secondsBetweenChars = 0.05f;

    public GameObject shopOption1;
    public GameObject shopOption2;
    public GameObject shopOption3;
    public GameObject shopPanel;
    public GameObject text;
    public GameObject questKeep;
    public GameObject LeaveShop;

    // Use this for initialization
    void Start() {
        StateManager.ShopState = StateManager.ShopStates.OUTSIDE;

        textComponent = text.GetComponent<Text>();
        textComponent.text = "";
        HideIcons();
    }

    public void RumorsShopkeep() {
        HideIcons();

        DialogueStrings[0] = "Nothing I'd pay for unfortunately...";
        DialogueStrings[1] = "Why don't you ask my parter over there. He is always looking for new bodies to put to work";
        DialogueStrings[2] = "Anything else I can help you with?";

        textComponent.text = "";
        isEndOfDialogue = false;

        if (!isEndOfDialogue) {
            textComponent.text = "";
            StartCoroutine(DisplayString(DialogueStrings[0]));
        }
    }

    public void InitShopkeep() {

        if (StateManager.ShopState == StateManager.ShopStates.OUTSIDE) {

            StateManager.ShopState = StateManager.ShopStates.SHOP;

            HideIcons();
            if (init) {
                DialogueStrings[0] = "Back for more?";
                DialogueStrings[1] = "Obviously you couldn't resist my quality wares";
                DialogueStrings[2] = "What else can I help you with?";
            }

            textComponent.text = "";
            init = true;
            questKeep.SetActive(false);

            isEndOfDialogue = false;
            shopPanel.SetActive(true);

            if (!isEndOfDialogue) {
                textComponent.text = "";
                StartCoroutine(DisplayString(DialogueStrings[0]));
            }
        }
    }

    public void CloseShopkeep() {
        StateManager.ShopState = StateManager.ShopStates.OUTSIDE;
        shopPanel.SetActive(false);
        questKeep.SetActive(true);
        LeaveShop.SetActive(true);
    }

    private IEnumerator DisplayString(string StringToDisplay) {
        int stringLength = StringToDisplay.Length;
        int currentCharIndex = 0;
        textComponent.text = "";

        while (currentCharIndex < stringLength) {
            textComponent.text += StringToDisplay[currentCharIndex];
            currentCharIndex++;
            if (currentCharIndex < stringLength) {
                if (Input.GetKey("space")) {
                    textComponent.text = StringToDisplay;
                    isEndOfDialogue = true;
                    break;
                } else {
                    yield return new WaitForSeconds(secondsBetweenChars);
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

