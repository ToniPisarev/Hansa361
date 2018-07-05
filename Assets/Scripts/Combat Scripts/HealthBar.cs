using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {


    private BaseCharacter myCharacter;
    private CharController charControl;
    private Image hp;
    private Transform friendly;
    private Transform enemy;

    public GameObject healthBar;
    public int numEnemies = 0;
    public int numFriendly = 0;
    public bool isEnemy;
    public bool updateHealthBar;


    // Use this for initialization
    void Start() {

        //Instantiate and initialize
        friendly = transform.Find("/Map/Friendly");
        enemy = transform.Find("/Map/Enemies");
        numEnemies = enemy.transform.childCount;
        numFriendly = friendly.transform.childCount;

        if (transform.parent.parent.name == "Enemies") {
            isEnemy = true;
        } else {
            isEnemy = false;
        }

        if (isEnemy == false) {
            healthBar = (GameObject)Instantiate(Resources.Load("FriendlyHealth"));
            healthBar.transform.SetParent(GameObject.Find("Canvas").transform, false);
        } else {
            healthBar = (GameObject)Instantiate(Resources.Load("Health"));
            healthBar.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }

    }

    // Update is called once per frame
    void Update() {
        if (updateHealthBar) {
            charControl = transform.parent.GetComponent<CharController>();
            myCharacter = transform.parent.GetComponent<CharController>().myCharacter;
            Vector3 position = Camera.main.WorldToScreenPoint(transform.position);

            if (healthBar != null) {
                healthBar.transform.position = position;
                hp = healthBar.transform.Find("HealthBar").GetComponent<Image>();
                hp.fillAmount = (float)(myCharacter.CurrentHealth) / myCharacter.Health;
                hp.GetComponentInChildren<Text>().text = "" + myCharacter.CurrentHealth;
            } else {
                updateHealthBar = false;
            }

            if (myCharacter.CurrentHealth <= 0 && charControl.isDead == false) {
                charControl.isDead = true;
                updateHealthBar = false;

                if (isEnemy) {
                    TurnBasedCombatStateMachine.enemyDeathCount++;
                    Debug.Log(myCharacter.PlayerName + " died death count - " + TurnBasedCombatStateMachine.enemyDeathCount);
                } else {
                    TurnBasedCombatStateMachine.deathCount++;
                    Debug.Log(myCharacter.PlayerName + " died death count - " + TurnBasedCombatStateMachine.deathCount);
                }

                //Set lose state
                if (TurnBasedCombatStateMachine.deathCount >= numFriendly) {

                    //Destroy all health bars
                    GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("bars");
                    for (var i = 0; i < objectsToDestroy.Length; i++) {
                        Destroy(objectsToDestroy[i]);
                    }

                    transform.parent.GetComponentInParent<TurnBasedCombatStateMachine>().SetCurrentState(4);
                }

                //Set win state
                if (TurnBasedCombatStateMachine.enemyDeathCount >= numEnemies) {
                    //Destroy all health bars
                    GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("bars");
                    for (var i = 0; i < objectsToDestroy.Length; i++) {
                        Destroy(objectsToDestroy[i]);
                    }

                    transform.parent.GetComponentInParent<TurnBasedCombatStateMachine>().SetCurrentState(5);
                }
                Destroy(healthBar);
                
            }
        }
    }
}
