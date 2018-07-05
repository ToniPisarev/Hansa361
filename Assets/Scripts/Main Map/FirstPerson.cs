using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPerson : MonoBehaviour {

    public float speed = 10.0f;
    private CreateNewQuest newQuest = new CreateNewQuest();
    private SpawnCharacterMainMap startScript;
    private bool isJumping = false;

    // Use this for initialization
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        startScript = GameObject.Find("Start").GetComponent<SpawnCharacterMainMap>();
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.transform.parent && collision.gameObject.transform.parent.name.Equals("Areas")) {
            GetComponentInChildren<MouseLook>().inGame = true;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        isJumping = false;

        if (collision.gameObject.transform.parent && collision.gameObject.transform.parent.name.Equals("Areas")) {
            int areaID = int.Parse(collision.gameObject.name);
            //Debug.Log("Current area " + areaID);
            WorldInformation.CurrentArea = areaID;
            GetComponentInChildren<MouseLook>().inGame = false;

            //Random encounter
            if (Random.value < 0.1) {
                Quest randomBatteQuest = newQuest.ReturnQuest();
                randomBatteQuest.QuestType = Quest.QuestTypes.Random;
                randomBatteQuest.QuestName = "Random Encounter!";
                randomBatteQuest.QuestLocation = WorldInformation.Areas.Find(x => x.AreaID == WorldInformation.CurrentArea);
                WorldInformation.CurrentQuest = randomBatteQuest;

                if (WorldInformation.CurrentQuest.QuestLocation.AreaType == Area.AreaTypes.Plains) {
                    SceneManager.LoadScene("Combat1");
                } else if (WorldInformation.CurrentQuest.QuestLocation.AreaType == Area.AreaTypes.Desert) {
                    SceneManager.LoadScene("Combat2");
                } else {
                    SceneManager.LoadScene("Combat1");
                }
            } else {
                MapHud.LoadAreaOptions(startScript.ShopButton, startScript.HudContent, startScript.AreaText);
            }
        }
    }

    public static void SpawnPlayer() {
        Instantiate(Resources.Load("Player"), new Vector3(Random.Range(50, 450), 100, Random.Range(50, 450)), Quaternion.Euler(0, -90, 0));
    }

    // Update is called once per frame
    void Update() {
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        if (transform.position.y < -10) {
            SpawnPlayer();
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 100, ForceMode.Impulse);
            isJumping = true;
        }
    }
}
