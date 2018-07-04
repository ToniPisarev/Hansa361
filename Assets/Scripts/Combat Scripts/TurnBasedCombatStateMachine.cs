using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnBasedCombatStateMachine : MonoBehaviour {

    private int turnNumber = 0;
    private Button back;
    private Transform endscene;
    private Text content;
    private Text stateText;
    private Transform loss;
    private Transform scrollView;

    public bool isFresh = true;

    public static int deathCount = 0;
    public static int enemyDeathCount = 0;

    public static int experienceGain = 40;
    public static int goldPenalty = 500; // minus this if lost

    public enum BattleStates { START, PLAYERCHOICE, PLAYERANIMATE, ENEMYCHOICE, LOSE, WIN }
    private BattleStates currentState;


    public void SetCurrentState(int battleState) {
        currentState = (BattleStates)battleState;
    }

    public BattleStates GetCurrentState() {
        return currentState;
    }

    // Use this for initialization
    void Start() {

        currentState = BattleStates.PLAYERCHOICE;
        back = transform.Find("Canvas/Back").GetComponent<Button>();
        endscene = transform.Find("Canvas/EndScene");
        content = endscene.Find("Scroll View/Viewport/Content").GetComponent<Text>();
        scrollView = endscene.Find("Scroll View");
        stateText = endscene.Find("State").GetComponent<Text>();
        loss = endscene.Find("Loss");
        //worldInfo = GameObject.Find("GameInformation").GetComponent<WorldInformation>();
    }

    private string DisplayString(BaseCharacter c) {

        int oldD = c.Defense;
        int oldA = c.Agility;
        int oldI = c.Intellect;
        int oldS = c.Strength;
        int oldH = c.Health;

        return
            "\nDefense: " + oldD + "-->" + c.Defense +
            "\nAgility: " + oldA + "-->" + c.Agility +
            "\nIntellect: " + oldI + "-->" + c.Intellect +
            "\nStrength: " + oldS + "-->" + c.Strength +
            "\nHealth: " + oldH + "-->" + c.CurrentHealth + "\n";
    }

    public void NextScene() {
        endscene.gameObject.SetActive(false);
        loss.gameObject.SetActive(false);
        scrollView.gameObject.SetActive(false);

        Debug.Log("ON TO THE NEXT SCENE");
        SceneManager.LoadScene("MainMap");
    }

    public void Lose() {
        back.gameObject.SetActive(true);
        back.onClick.AddListener(() => NextScene());
        isFresh = false;
        Debug.Log("YOU LOSE -------------------------");
        scrollView.gameObject.SetActive(false);
        endscene.gameObject.SetActive(true);
        loss.gameObject.SetActive(true);
        stateText.text = "You Lose!";

        // if lose, minus gold
        GameInformation.Gold -= goldPenalty;
        if (GameInformation.Gold < 0) {
            GameInformation.Gold = 0;
        }
    }

    public void Win() {
        back.gameObject.SetActive(true);
        back.onClick.AddListener(() => NextScene());
        isFresh = false;
        Debug.Log("YOU WIN -------------------------");
        endscene.gameObject.SetActive(true);
        scrollView.gameObject.SetActive(true);
        stateText.text = "You Win!";
        //string quest = WorldInformation.CurrentQuest.QuestName;
        Debug.Log(WorldInformation.CurrentQuest.QuestName);
        content.text = "You have finished the quest: \n* *\n";
        content.text += WorldInformation.CurrentQuest.QuestName;
        content.text += "\n* *\nAll characters have gained 40 experience.";

        // if win, get the rewards
        GameInformation.Gold += WorldInformation.CurrentQuest.GoldReward;

        if (WorldInformation.CurrentQuest.WeaponReward != null) {
            GameInformation.PlayerInventory.Weapons.Add(WorldInformation.CurrentQuest.WeaponReward);
        }
        if (WorldInformation.CurrentQuest.EquipmentReward != null) {
            GameInformation.PlayerInventory.Equipment.Add(WorldInformation.CurrentQuest.EquipmentReward);
        }
        if (WorldInformation.CurrentQuest.PotionReward != null) {
            GameInformation.PlayerInventory.Potions.Add(WorldInformation.CurrentQuest.PotionReward);
        }

        // move the quest to FinishedQuests
        GameInformation.PlayerQuestLog.FinishedQuests.Add(WorldInformation.CurrentQuest);
        GameInformation.PlayerQuestLog.CurrentQuests.Remove(WorldInformation.CurrentQuest);
        WorldInformation.CurrentQuest = null;

        //Gain XP, check for levelup
        Promote(GameInformation.PlayerCharacter);
        if (GameInformation.SideCharacters != null) {
            for (int i = 0; i < GameInformation.SideCharacters.Length; i++) {
                if (GameInformation.SideCharacters != null) {
                    Promote(GameInformation.SideCharacters[i]);
                }
            }
        }
        
    }


    //Helper func: Gain XP, check for levelup
    private void Promote(BaseCharacter character) {
        if (character != null) {

            character.CurrentXP += experienceGain;

            if (character.CurrentXP >= character.RequiredXP) {
                character.PlayerLevel++;
                character.CurrentXP = 0;
                character.AvailableStatPoints++;

                // save old stats for GUI display
                int oldD = character.Defense;
                int oldA = character.Agility;
                int oldI = character.Intellect;
                int oldS = character.Strength;
                int oldH = character.Health;

                // increase stats when level up
                int tier = (int)character.PlayerClass / 4 + 1;

                //basic increase

                character.Defense += tier;
                character.Agility += tier;
                character.Intellect += tier;
                character.Strength += tier;
                character.Health += tier * 20;

                //extra increase based on class
                if ((int)character.PlayerClass % 4 == 0) {
                    //squire
                    character.Defense += tier;
                    character.Strength += tier;
                } else if ((int)character.PlayerClass % 4 == 1) {
                    //Apprentice
                    character.Agility += tier;
                    character.Intellect += tier;
                } else if ((int)character.PlayerClass % 4 == 2) {
                    //Thief
                    character.Agility += tier;
                    character.Strength += tier;
                } else {
                    //Archer
                    character.Defense += tier;
                    character.Intellect += tier;
                }

                SaveInformation.SaveAllInformation();
                

                content.text += "\n* *\nLevel up!\n* *\n" + character.PlayerName +
                "\nDefense: " + oldD + "-->" + character.Defense +
                "\nAgility: " + oldA + "-->" + character.Agility +
                "\nIntellect: " + oldI + "-->" + character.Intellect +
                "\nStrength: " + oldS + "-->" + character.Strength;


            }
        }
    }

    void Update() {

        //Debug.Log(currentState);

        switch (currentState) {
            //setup functions that run when each state is active.
            case (BattleStates.START):

                //setup battle function
                break;

            //Set players turn to true------------------------------------------------------------------------------------------------------
            case (BattleStates.PLAYERCHOICE):

                Transform players = transform.GetChild(0);
                for (int i = 0; i < players.childCount; i++) {
                    CharController currentPlayer = players.GetChild(i).GetComponent<CharController>();

                    if (currentPlayer.myTurn) {
                        //Debug.Log("Waiting for player " + i);
                        break; //Dont Continue untill this players turn is over.
                    }

                    if (!currentPlayer.myTurn && turnNumber == i) {
                        currentPlayer.myTurn = true;
                        turnNumber += 1;
                        break;
                    }
                }

                if (turnNumber == players.childCount && !players.GetChild(turnNumber - 1).GetComponent<CharController>().myTurn) {
                    turnNumber = 0;
                    currentState = BattleStates.ENEMYCHOICE;
                }

                break;

            //Set enemies turn to true--------------------------------------------------------------------------------------------------------
            case (BattleStates.ENEMYCHOICE):
                //Need Artificial intelligence here
                Transform enemies = transform.GetChild(1);
                for (int i = 0; i < enemies.childCount; i++) {
                    CharController currentPlayer = enemies.GetChild(i).GetComponent<CharController>();
                    if (currentPlayer.myTurn)
                        break; //Dont Continue untill this players turn is over.
                    if (!currentPlayer.myTurn && turnNumber == i) {
                        currentPlayer.myTurn = true;
                        turnNumber += 1;
                        break;
                    }
                }

                if (turnNumber == enemies.childCount && !enemies.GetChild(turnNumber - 1).GetComponent<CharController>().myTurn) {
                    turnNumber = 0;
                    currentState = BattleStates.PLAYERCHOICE;
                }

                break;

            //LOSS---------------------------------------------------------------------------------------------------------------------------------------
            case (BattleStates.LOSE):
                if (isFresh) {
                    Lose();
                }

                currentState = BattleStates.START;
                break;


            case (BattleStates.WIN):
                if (isFresh) {
                    Win();
                }
                currentState = BattleStates.START;
                break;

        }
    }
}
