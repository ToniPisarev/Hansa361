﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LoadQuestPause : MonoBehaviour {
    public GameObject QstContent;
    public GameObject QuestDetailPanel;
    public GameObject currentQuestButton;

    public void ShowAllCurrentQuests() {
        currentQuestButton.transform.GetComponent<Button>().Select();
        // Destroy all contents first
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in QstContent.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));


        int height = -10;

        foreach (Quest q in GameInformation.PlayerQuestLog.CurrentQuests) {
            ShowQuest(q, height, false);

            height -= 100;
        }
        RectTransform r = (RectTransform)QstContent.transform;
        r.sizeDelta = new Vector2(0, height * (-1) + 10);
    }
    public void ShowAllFinishedQuests() {
        // Destroy all contents first
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in QstContent.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        int height = -10;
        foreach (Quest q in GameInformation.PlayerQuestLog.FinishedQuests) {
            ShowQuest(q, height, true);

            height -= 100;
        }

        RectTransform r = (RectTransform)QstContent.transform;
        r.sizeDelta = new Vector2(0, height * (-1) + 10);
    }

    private void ShowQuest(Quest q, int height, bool isCompleted) {
        GameObject newB = (GameObject)Instantiate(Resources.Load("QuestMenuButton"));
        newB.transform.SetParent(QstContent.transform);
        SetListener(newB.GetComponent<Button>(), q, isCompleted);
        RectTransform ButtonRect = (RectTransform)newB.transform;
        ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
        ButtonRect.localScale = new Vector3(1, 1, 1);
        GameObject Name = newB.transform.GetChild(0).gameObject; // name
        Text text;
        text = Name.GetComponent<Text>();
        text.text = q.QuestName;

        Name = newB.transform.GetChild(1).gameObject; // alliance
        text = Name.GetComponent<Text>();
        text.text = "Alliance: " + q.QuestAlliance.KingName;

        Name = newB.transform.GetChild(2).gameObject; // enemy
        text = Name.GetComponent<Text>();
        if (q.QuestEnemy != null) text.text = "Enemy: " + q.QuestEnemy.KingName;
        else Name.SetActive(false);

        Name = newB.transform.GetChild(3).gameObject; // reward
        text = Name.GetComponent<Text>();
        text.text = "Reward: $ " + q.GoldReward;

        Name = newB.transform.GetChild(4).gameObject; // to-do
        text = Name.GetComponent<Text>();
        string s = "";
        switch (q.QuestType) {
            case Quest.QuestTypes.CaravanProtect:
                s = "Protect Caravan";
                break;
            case Quest.QuestTypes.Control:
                s = "Control";
                break;
            case Quest.QuestTypes.Delivery:
                s = "Delivery";
                break;
            case Quest.QuestTypes.TreasureMapGetMap:
                s = "Get Map";
                break;
            default:
                s = "Cool Quest!";
                break;

        }
        text.text = s + "\nRecommended: Lv." + q.RecommendedLevel;
    }

    private void SetListener(Button B, Quest q, bool isCompleted) {
        B.onClick.AddListener(delegate { ShowQuestDetail(q, isCompleted); });
    }

    private void ShowQuestDetail(Quest q, bool isCompleted) {
        QuestDetailPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = q.QuestName;
        QuestDetailPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Alliance: " + q.QuestAlliance.KingName;
        if (q.QuestEnemy != null) QuestDetailPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Enemy: " + q.QuestEnemy.KingName;
        else QuestDetailPanel.transform.GetChild(2).gameObject.SetActive(false);
        Text r = QuestDetailPanel.transform.GetChild(3).gameObject.GetComponent<Text>();
        r.text = "Reward\n$ " + q.GoldReward;
        if (q.EquipmentReward != null) r.text += "\n" + q.EquipmentReward.ItemName;
        if (q.PotionReward != null) r.text += "\n" + q.PotionReward.ItemName;
        if (q.WeaponReward != null) r.text += "\n" + q.WeaponReward.ItemName;
        QuestDetailPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = q.QuestDescription;
        string s = "";
        switch (q.QuestType) {
            case Quest.QuestTypes.CaravanProtect:
                s = "Protect Caravan";
                break;
            case Quest.QuestTypes.Control:
                s = "Control";
                break;
            case Quest.QuestTypes.Delivery:
                s = "Delivery";
                break;
            case Quest.QuestTypes.TreasureMapGetMap:
                s = "Get Map";
                break;
            default:
                s = "Cool Quest!";
                break;

        }
        QuestDetailPanel.transform.GetChild(5).gameObject.GetComponent<Text>().text = s + "\nRecommended: Lv." + q.RecommendedLevel;
        QuestDetailPanel.transform.GetChild(6).gameObject.SetActive(isCompleted);
        QuestDetailPanel.SetActive(true);
    }
}
