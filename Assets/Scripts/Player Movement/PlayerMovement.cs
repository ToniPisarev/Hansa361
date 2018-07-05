using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
/*
public class PlayerMovement : MonoBehaviour {

    //private GameInfo gameInfo = new GameInfo();
    private Vector3 newPos;
    private string pathID;

    int rand;
    int edgeFinder = 0;

    private GameObject Player;

    public GameObject HudContent;
    public GameObject ShopButton;
    public GameObject AreaText;
    public GameObject DecisionPanel;

    private string ID;

    private CreateNewQuest newQuest = new CreateNewQuest();
    //private updateAreas UA = new updateAreas();

    // Use this for initialization
    void Start() {
        ID = gameObject.name;
        //Debug.Log("Starting ID is: " + ID);
        Player = GameObject.Find("Puppeteer");
        WorldInformation.CurrentArea = "1";
        GameObject currArea = GameObject.Find(WorldInformation.CurrentArea);
        Player.transform.position = currArea.transform.position;
        GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Idle;

    }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            PathTravel();
        }
    }

    private void PathTravel() {

        if (String.Compare(WorldInformation.CurrentArea, ID) != 0) {

            GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Travelling;
            edgeFinder = 0;
            GameObject v;
            int tempID = 50;
            float tempDist = 1000f;
            Debug.Log("Your current area:  " + WorldInformation.CurrentArea);
            while (edgeFinder < 5 && WorldInformation.Edges[Int32.Parse(WorldInformation.CurrentArea) - 1, edgeFinder] != 0) {
                //Debug.Log("Just saw that you have an edge to " + WorldInformation.Edges[Int32.Parse(WorldInformation.CurrentArea) - 1, edgeFinder]);
                v = GameObject.Find(WorldInformation.Edges[Int32.Parse(WorldInformation.CurrentArea) - 1, edgeFinder] + "");
                //Debug.Log("The obj dist is: " + Vector3.Distance(v.transform.position, gameObject.transform.position));
                if (tempDist >= Vector3.Distance(v.transform.position, gameObject.transform.position)) {
                    tempDist = Vector3.Distance(v.transform.position, gameObject.transform.position);
                    tempID = WorldInformation.Edges[Int32.Parse(WorldInformation.CurrentArea) - 1, edgeFinder];
                    // Debug.Log("Your tempID is:  " + tempID);
                }
                edgeFinder++;
            }
            rand = WorldInformation.rnd.Next(0, 21);
            Debug.Log(rand + " random encounter number");
            if (rand < 1) {
                //Scene Switch! with GameInformation.PlayerCharacter.PlayerLevel    Random encounter
                Quest randomBatteQuest = newQuest.ReturnQuest();
                randomBatteQuest.QuestType = Quest.QuestTypes.Random;
                randomBatteQuest.QuestName = "Random Encounter!";
                randomBatteQuest.QuestLocation = WorldInformation.Areas.Find(x => x.IconNumber == Int32.Parse(WorldInformation.CurrentArea));

                WorldInformation.CurrentQuest = randomBatteQuest;

                if (WorldInformation.CurrentQuest.QuestLocation.AreaType == Area.AreaTypes.Plains) {
                    SceneManager.LoadScene("Combat1");
                } else if (WorldInformation.CurrentQuest.QuestLocation.AreaType == Area.AreaTypes.Desert) {
                    SceneManager.LoadScene("Combat2");
                } else {
                    SceneManager.LoadScene("Combat1");
                }
            } else {
                travel(WorldInformation.CurrentArea, tempID + "");
            }
            GameInformation.PlayerMapState = GameInformation.PlayerMapStates.Idle;
        } else {
            MapHud.LoadAreaOptions(ShopButton, HudContent, AreaText, DecisionPanel);
        }
    }

    private void travel(string A, string B) {

        int curr = Int32.Parse(A);
        int dest = Int32.Parse(B);
        //newPos = transform.position;
        if (curr != dest) {
            if (curr < dest) {

                pathID = A + "to" + B;
                //Debug.Log("Your pathID :" + pathID);

                iTween.MoveTo(Player, iTween.Hash("path", iTweenPath.GetPath(pathID), "time", 2, "orienttopath", true, "easetype", iTween.EaseType.linear, "oncompletetarget", gameObject, "onComplete", "PathTravel"));
                WorldInformation.CurrentArea = B;
                //Debug.Log("The new current ID: " + WorldInformation.CurrentArea);

            } else {

                pathID = B + "to" + A;
                //Debug.Log("Your pathID :" + pathID);
                iTween.MoveTo(Player, iTween.Hash("path", iTweenPath.GetPathReversed(pathID), "time", 2, "orienttopath", true, "easetype", iTween.EaseType.linear, "oncompletetarget", gameObject, "onComplete", "PathTravel"));
                WorldInformation.CurrentArea = B;
                //Debug.Log("The new current ID: " + WorldInformation.CurrentArea);
            }
        }
    }
}


    */