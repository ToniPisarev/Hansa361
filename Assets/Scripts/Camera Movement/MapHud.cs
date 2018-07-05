﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MapHud : MonoBehaviour {

    public static void LoadAreaOptions(GameObject ShopButton, GameObject HUDContent, GameObject AreaText, GameObject DecisionPanel) {
        int index = 0;
        int height = -65;

        //iterate through areas in area list to find area options
        //Debug.Log(WorldInformation.CurrentArea + " current area");
        Area myArea = WorldInformation.Areas.Find(x => x.AreaID == Int32.Parse(WorldInformation.CurrentArea));

        //Delete quests from other area
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in HUDContent.transform) if (String.Compare(child.gameObject.name, "Button") != 0) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        Text tx = AreaText.GetComponent<Text>();
        tx.text = myArea.AreaName + " \n(King " + myArea.DefendingKingdom.KingName + ")";
        if (myArea.AreaType == Area.AreaTypes.City) {
            //Show shop option!
            ShopButton.SetActive(true);
            height = -150;

        } else {
            ShopButton.SetActive(false);
        }

        //Iterate through current quests
        //iterate through quest locations in current quests to find options
        for (int i = 0; i < GameInformation.PlayerQuestLog.CurrentQuests.Count; i++) {
            if (GameInformation.PlayerQuestLog.CurrentQuests[i].QuestLocation.IconNumber == myArea.IconNumber) {

                //Show current quests in the right area!
                GameObject questButton = (GameObject)Instantiate(Resources.Load("QuestButtonMainHuD"));
                questButton.transform.SetParent(HUDContent.transform);
                SetListener(questButton.GetComponent<Button>(), index, GameInformation.PlayerQuestLog.CurrentQuests[i], questButton);
                index++;

                RectTransform ButtonRect = (RectTransform)questButton.transform;
                ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
                ButtonRect.localScale = new Vector3(1, 1, 1);

                GameObject Name = questButton.transform.GetChild(0).gameObject;
                Text text = Name.GetComponent<Text>();
                text.text = GameInformation.PlayerQuestLog.CurrentQuests[i].QuestName;

                Name = questButton.transform.GetChild(1).gameObject;
                text = Name.GetComponent<Text>();

                text.text = "Reward: " + GameInformation.PlayerQuestLog.CurrentQuests[i].GoldReward + " Gold";
                if (GameInformation.PlayerQuestLog.CurrentQuests[i].EquipmentReward != null) {
                    text.text += ", 1 piece of Equipment";
                }
                if (GameInformation.PlayerQuestLog.CurrentQuests[i].WeaponReward != null) {
                    text.text += ", 1 Weapon";
                }
                if (GameInformation.PlayerQuestLog.CurrentQuests[i].PotionReward != null) {
                    text.text += ", 1 Potion";
                }

                height = height - 100;
            }
        }

        height = height - 50;
        //Resizes the window based on how many items the player/shop has
        RectTransform ViewRect = (RectTransform)HUDContent.transform;
        ViewRect.sizeDelta = new Vector2(0, height * (-1));
    }


    private static void SetListener(Button questButton, int index, Quest q, GameObject questObject) {
        //Adds a listener onto a button 
        int i = index;
        questButton.onClick.AddListener(delegate { StartQuest(i, q, questObject); });

    }

    private static void StartQuest(int i, Quest q, GameObject questButton) {
        
        GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Travelling;

        //Quests
        if (q.QuestType == Quest.QuestTypes.Delivery) {

            // Get quest rewards
            GameInformation.Gold += q.GoldReward;
            if (q.WeaponReward != null) {
                GameInformation.PlayerInventory.Weapons.Add(q.WeaponReward);
            }
            if (q.EquipmentReward != null) {
                GameInformation.PlayerInventory.Equipment.Add(q.EquipmentReward);
            }
            if (q.PotionReward != null) {
                GameInformation.PlayerInventory.Potions.Add(q.PotionReward);
            }

            // move the quest to FinishedQuests
            GameInformation.PlayerQuestLog.FinishedQuests.Add(q);
            GameInformation.PlayerQuestLog.CurrentQuests.Remove(q);
            Destroy(questButton);
        }
        else if (q.QuestType == Quest.QuestTypes.Control) {
            
        } else {

            WorldInformation.CurrentQuest = q;

            GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Idle;
            if (q.QuestLocation.AreaType == Area.AreaTypes.Plains) {
                SceneManager.LoadScene("Combat1");
            } else if (q.QuestLocation.AreaType == Area.AreaTypes.Desert) {
                SceneManager.LoadScene("Combat1");
            } else {
                SceneManager.LoadScene("Combat2");
            }
        }

    }

    public void AttackControl() {
        GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Idle;
        WorldInformation.Attacker = 0;
        if (WorldInformation.Control.QuestLocation.AreaType == Area.AreaTypes.Plains) {
            SceneManager.LoadScene("Combat1");
        } else if (WorldInformation.Control.QuestLocation.AreaType == Area.AreaTypes.Desert) {
            SceneManager.LoadScene("Combat3");
        } else {
            SceneManager.LoadScene("Combat2");
        }
        Debug.Log("Attack");

    }
    public void DefendControl() {
        GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Idle;
        WorldInformation.Attacker = 1;
        if (WorldInformation.Control.QuestLocation.AreaType == Area.AreaTypes.Plains) {
            SceneManager.LoadScene("Combat1");
        } else if (WorldInformation.Control.QuestLocation.AreaType == Area.AreaTypes.Desert) {
            SceneManager.LoadScene("Combat3");
        } else {
            SceneManager.LoadScene("Combat2");
        }
        Debug.Log("Defend");
    }
    public void Shop() {
        SceneManager.LoadScene("Store");
    }


    // Update is called once per frame
    void Update() {

    }


}
