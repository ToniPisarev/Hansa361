using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MapHud : MonoBehaviour {

    public static void LoadAreaOptions(GameObject ShopButton, GameObject HUDContent, GameObject AreaText) {
        int index = 0;
        int height = -65;

        Area myArea = WorldInformation.Areas.Find(x => x.AreaID == WorldInformation.CurrentArea);

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

        //iterate through quest locations in current quests to find options
        for (int i = 0; i < GameInformation.PlayerQuestLog.CurrentQuests.Count; i++) {
            if (GameInformation.PlayerQuestLog.CurrentQuests[i].QuestLocation.AreaID == myArea.AreaID) {

                //Show current quests in the right area!
                GameObject questButton = (GameObject)Instantiate(Resources.Load("QuestButtonMainHuD"));
                questButton.transform.SetParent(HUDContent.transform);
                SetListener(questButton, index, GameInformation.PlayerQuestLog.CurrentQuests[i]);
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

    private static void SetListener(GameObject questObject, int index, Quest q) {
        questObject.GetComponent<Button>().onClick.AddListener(delegate { StartQuest(index, q, questObject); });
    }

    private static void StartQuest(int i, Quest q, GameObject questButton) {

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
        } else if (q.QuestType == Quest.QuestTypes.Control) {

        } else {

            WorldInformation.CurrentQuest = q;
           
            if (q.QuestLocation.AreaType == Area.AreaTypes.Plains) {
                SceneManager.LoadScene("Combat1");
            } else if (q.QuestLocation.AreaType == Area.AreaTypes.Desert) {
                SceneManager.LoadScene("Combat1");
            } else {
                SceneManager.LoadScene("Combat2");
            }
        }
    }

    public void Shop() {
        SceneManager.LoadScene("Store");
    }
}
