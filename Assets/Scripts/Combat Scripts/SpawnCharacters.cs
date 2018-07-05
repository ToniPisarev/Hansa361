using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Attach to Object (0,0) 
public class SpawnCharacters : MonoBehaviour {

    GameObject Friendly;
    GameObject Enemies;

    public static BaseCharacter[] friendlyList;
    public static BaseCharacter[] enemyList;

    GameObject[] friendlyPrefab = new GameObject[6];
    GameObject[] enemyPrefab = new GameObject[6];

    Vector3[] friendlyPositions = new Vector3[6];
    Vector3[] enemyPositions = new Vector3[6];

    private static BaseCharacter newSide;

    // Use this for initialization
    void Start() {

        // Find the parents
        Friendly = GameObject.Find("Friendly");
        Enemies = GameObject.Find("Enemies");

        friendlyList = new BaseCharacter[6];
        enemyList = new BaseCharacter[6];

        // For Demo: Generate 4 side characters
        LoadInformation.LoadAllInformation();
        int level = GameInformation.PlayerCharacter.PlayerLevel;

        if ( Random.value < 0.5) {
            AddNewSideCharacter(CreateLeveledCharacter(level));
        }        
       
        // Get the six friendly chararcter
        friendlyList[0] = GameInformation.PlayerCharacter;
        int numFriendlies = 1;

        if (GameInformation.SideCharacters != null) {
            for (int i = 0; i < GameInformation.SideCharacters.Length; i++) {
                if (GameInformation.SideCharacters[i] != null) {
                    numFriendlies++;
                    friendlyList[i+1] = GameInformation.SideCharacters[i];
                }
            }
        }

        // Generate some enemies
        for (int i = 0; i < numFriendlies; i++) {
            enemyList[i] = CreateLeveledCharacter(GameInformation.PlayerCharacter.PlayerLevel);
        }

        for (int i = 0; i < 6; i++) {

            // Generate some position
            // TODO:testing
            friendlyPositions[i] = ConvertPosition(13 + i, 15);
            enemyPositions[i] = ConvertPosition(13 + i, 17);


            // decide friendly prefab and instantiate
            if (friendlyList[i] != null) {

                if ((int)friendlyList[i].PlayerClass % 4 == 0) {
                    friendlyPrefab[i] = (GameObject)Instantiate(Resources.Load("Warrior"), friendlyPositions[i], Quaternion.identity);
                } else if ((int)friendlyList[i].PlayerClass % 4 == 1) {
                    friendlyPrefab[i] = (GameObject)Instantiate(Resources.Load("Mage"), friendlyPositions[i], Quaternion.identity);
                } else if ((int)friendlyList[i].PlayerClass % 4 == 2) {
                    friendlyPrefab[i] = (GameObject)Instantiate(Resources.Load("Thief"), friendlyPositions[i], Quaternion.identity);
                } else {
                    friendlyPrefab[i] = (GameObject)Instantiate(Resources.Load("Archer"), friendlyPositions[i], Quaternion.identity);
                }


                friendlyPrefab[i].transform.SetParent(Friendly.transform);

                //set tag (for the CharController to know which friend it is)
                friendlyPrefab[i].tag = i.ToString() + "friendlyPlayer";
            }

            // decide enemy prefab and instantiate
            if (enemyList[i] != null && friendlyList[i] != null) {

                //turn around the enemies
                Quaternion rotation = Quaternion.Euler(0f, 180f, 0f);

                if ((int)enemyList[i].PlayerClass % 4 == 0) {
                    enemyPrefab[i] = (GameObject)Instantiate(Resources.Load("Warrior"), enemyPositions[i], rotation);
                } else if ((int)enemyList[i].PlayerClass % 4 == 1) {
                    enemyPrefab[i] = (GameObject)Instantiate(Resources.Load("Mage"), enemyPositions[i], rotation);
                } else if ((int)enemyList[i].PlayerClass % 4 == 2) {
                    enemyPrefab[i] = (GameObject)Instantiate(Resources.Load("Thief"), enemyPositions[i], rotation);
                } else {
                    enemyPrefab[i] = (GameObject)Instantiate(Resources.Load("Archer"), enemyPositions[i], rotation);
                }

                enemyPrefab[i].transform.SetParent(Enemies.transform);

                //set tag (for the CharController to know which enemy it is)
                enemyPrefab[i].tag = i.ToString() + "enemyPlayer";
            }
        }
    }

    //Adds side character to game info
    public static void AddNewSideCharacter(BaseCharacter side) {

        if(GameInformation.SideCharacters == null) {
            GameInformation.SideCharacters = new BaseCharacter[5];
        }

        if(GameInformation.SideCharacters != null) {
            for (int i = 0; i < GameInformation.SideCharacters.Length; i++) {
                if (GameInformation.SideCharacters[i] == null) {
                    side.PlayerName = side.PlayerClass.ToString();
                    GameInformation.SideCharacters[i] = side;
                    break;
                }
            }
        }
    }

    //Create new character
    public static BaseCharacter CreateNewCharacter(BaseCharacterClass.CharacterClasses pClass) {
        //Sets all standard info!
        BaseCharacter newCharacter = new BaseCharacter();
        newCharacter.PlayerLevel = 1;
        newCharacter.PlayerClass = pClass;
        GameInformation.SetCharacterStats(newCharacter);
        return newCharacter;
    }

    //Create random leveled character
    public static BaseCharacter CreateLeveledCharacter(int level) {

        BaseCharacter newCharacter = new BaseCharacter();
        newCharacter.PlayerLevel = level;
        int classDecison = Random.Range(1, 4);

        if (level < 12) {
            //tier 1 class
            if (classDecison == 1) {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Apprentice;
            } else if (classDecison == 2) {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Archer;
            } else if (classDecison == 3) {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Squire;
            } else {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Thief;
            }
        } else if (level < 20) {
            //tier 2 class
            if (classDecison == 1) {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Mage;
            } else if (classDecison == 2) {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Hunter;
            } else if (classDecison == 3) {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Knight;
            } else {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Ninja;
            }
        } else {
            //tier 3 class
            if (classDecison == 1) {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.ArchMage;
            } else if (classDecison == 2) {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Sniper;
            } else if (classDecison == 3) {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Paladin;
            } else {
                newCharacter.PlayerClass = BaseCharacterClass.CharacterClasses.Assassin;
            }
        }

        GameInformation.SetCharacterStats(newCharacter);
        return newCharacter;
    }

    //Create specific class and level
    public static BaseCharacter CreateLeveledCharacter(int level, BaseCharacterClass.CharacterClasses playerClass) {

        BaseCharacter newCharacter = new BaseCharacter();
        newCharacter.PlayerLevel = level;
        newCharacter.PlayerClass = playerClass;
        GameInformation.SetCharacterStats(newCharacter);
        return newCharacter;

    }

    //helper func: map index to transform position
    public Vector3 ConvertPosition(int x, int y) {

        Cell c = GetComponent<Cell>();

        Vector2 temp = c.convertIndexToWorldPos(x, y);
        TileDraw.Map.Tile aTile = c.GetTileFromPointInCell(x, y);

        return new Vector3(temp.x, aTile.GetHeight(), temp.y);
    }


}
