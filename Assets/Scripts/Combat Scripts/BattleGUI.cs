using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleGUI : MonoBehaviour {
    /*
        public int deathCount = 0;
        public int enemyDeathCount = 0;
        public int numEnemies = 0;
        public int numFriendly = 0;
        public bool defaultName;
        public Vector3 currentEnemyPos;
        private Transform friendly;
        private Transform enemy;
        private Transform currentEnemy;
        private int myEnemy;
        private Image hp;
        private Image mp;
        private CharController c;

        void Start() {

            //Instantiate and initialize
            friendly = transform.Find("/Map/Friendly");
            enemy = transform.Find("/Map/Enemies");
            //currentEnemyPos = enemy.GetChild(0).transform.position; //by default target is first enemy ....................
            myEnemy = 0;
            defaultName = true;
            numEnemies = enemy.transform.childCount;
            numFriendly = friendly.transform.childCount;

            Transform p = transform.Find("Player");
            if (friendly.childCount == 2) {
                p.Find("SidePlayer1").gameObject.SetActive(true);
            } else if (friendly.childCount == 3) {
                p.Find("SidePlayer1").gameObject.SetActive(true);
                p.Find("SidePlayer2").gameObject.SetActive(true);
            } else if (friendly.childCount == 4) {
                p.Find("SidePlayer1").gameObject.SetActive(true);
                p.Find("SidePlayer2").gameObject.SetActive(true);
                p.Find("SidePlayer3").gameObject.SetActive(true);
            } else if (friendly.childCount == 5) {
                p.Find("SidePlayer1").gameObject.SetActive(true);
                p.Find("SidePlayer2").gameObject.SetActive(true);
                p.Find("SidePlayer3").gameObject.SetActive(true);
                p.Find("SidePlayer4").gameObject.SetActive(true);
            } else if (friendly.childCount == 6) {
                p.Find("SidePlayer1").gameObject.SetActive(true);
                p.Find("SidePlayer2").gameObject.SetActive(true);
                p.Find("SidePlayer3").gameObject.SetActive(true);
                p.Find("SidePlayer4").gameObject.SetActive(true);
                p.Find("SidePlayer5").gameObject.SetActive(true);
            }


            //Player
            Text playerName = transform.Find("Player/SidePlayer0/PlayerIcon/PlayerName").GetComponent<Text>();
            if (defaultName) {
                playerName.text = "Hero";
            } else {
                playerName.text = GameInformation.PlayerCharacter.PlayerName;
            }
            Text spName1 = transform.Find("Player/SidePlayer1/PlayerIcon/PlayerName").GetComponent<Text>();
            if (defaultName) {
                spName1.text = "SideKick";
            } else {
                spName1.text = GameInformation.Char1.PlayerName;
            }
            Text spName2 = transform.Find("Player/SidePlayer2/PlayerIcon/PlayerName").GetComponent<Text>();
            if (defaultName) {
                spName1.text = "SideKick";
            } else { spName2.text = GameInformation.Char2.PlayerName; }

            Text spName3 = transform.Find("Player/SidePlayer3/PlayerIcon/PlayerName").GetComponent<Text>();
            if (defaultName) { spName1.text = "SideKick"; } else { spName3.text = GameInformation.Char3.PlayerName; }

            Text spName4 = transform.Find("Player/SidePlayer4/PlayerIcon/PlayerName").GetComponent<Text>();
            if (defaultName) { spName1.text = "SideKick"; } else { spName4.text = GameInformation.Char4.PlayerName; }

            Text spName5 = transform.Find("Player/SidePlayer5/PlayerIcon/PlayerName").GetComponent<Text>();
            if (defaultName) { spName1.text = "SideKick"; } else { spName5.text = GameInformation.Char5.PlayerName; }


            //Target

            //Text enemyName = transform.Find("Target").Find("PlayerIcon").Find("PlayerName").GetComponent<Text>();
            //enemyName.text = "Enemy";

        }

        void Update() {

            for (int i = 0; i < friendly.childCount; i++) {
                c = friendly.GetChild(i).GetComponent<CharController>();
                hp = transform.Find("Player/SidePlayer" + i + "/HealthBar").GetComponent<Image>();
                mp = transform.Find("Player/SidePlayer" + i + "/EnergyBar").GetComponent<Image>();
                float fillAmount = (c.myHealth) / 100.0f;
                float mpfillAmount = (c.myMana) / 100.0f;
                hp.fillAmount = fillAmount;
                mp.fillAmount = mpfillAmount;
                hp.GetComponentInChildren<Text>().text = Mathf.Round(fillAmount * 100) + "%";

                if (c.myHealth <= 0 && c.isDead == false) {
                    c.isDead = true;
                    deathCount++;
                    //Set lose state
                    if (deathCount == numFriendly)
                        transform.parent.GetComponentInParent<TurnBasedCombatStateMachine>().SetCurrentState(4);
                }

            }

            for (int i = 0; i < enemy.childCount; i++) {
                //Debug.Log(currentEnemyPos);
                if (enemy.GetChild(i).transform.position == currentEnemyPos || myEnemy == i) {
                    myEnemy = i;
                    c = enemy.GetChild(i).GetComponent<CharController>();
                    //hp = transform.Find("Target/HealthBar").GetComponent<Image>();
                    mp = transform.Find("Player/SidePlayer" + i + "/EnergyBar").GetComponent<Image>();
                    float fillAmount = (c.myHealth) / 100.0f;
                    float mpfillAmount = (c.myMana) / 100.0f;
                    hp.fillAmount = fillAmount;
                    mp.fillAmount = mpfillAmount;
                    hp.GetComponentInChildren<Text>().text = Mathf.Round(fillAmount * 100) + "%";

                }
                if (c.myHealth <= 0 && c.isDead == false) {
                    c.isDead = true;
                    enemyDeathCount++;
                    //Set win state
                    if (enemyDeathCount == numEnemies)
                        transform.parent.GetComponentInParent<TurnBasedCombatStateMachine>().SetCurrentState(5);
                }
            }
        */
}
