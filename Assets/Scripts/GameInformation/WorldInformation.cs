using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Linq;
using LitJson;

//Stores all the world information -- This is what is going to get loaded from online
public class WorldInformation : MonoBehaviour {

    public static string CurrentArea { get; set; }
    public static string UserID { get; set; }
    public static int KingdomCount { get; set; }
    public static string CurrentWorldID { get; set; }
    public static System.Random rnd = new System.Random();

    //paths total of 34 Areas
    public static int[,] Edges = new int[34, 5] { { 2, 3, 24, 34, 0 }, { 1, 3, 0, 0, 0 }, { 1, 2, 4, 0, 0 }, { 3, 5, 27, 0, 0 }, { 4, 6, 7, 0, 0 }, { 5, 7, 32, 33, 0 }, { 5, 6, 9, 10, 0 }, { 9, 11, 26, 0, 0 }, { 7, 8, 10, 11, 0 }, { 7, 9, 12, 13, 14 }, { 8, 9, 12, 29, 0 }, { 10, 11, 13, 0, 0 }, { 10, 12, 14, 0, 0 }, { 10, 13, 15, 0, 0 }, { 14, 16, 30, 0, 0 }, { 15, 17, 30, 31, 0 }, { 16, 18, 31, 0, 0 }, { 17, 19, 31, 0, 0 }, { 18, 20, 33, 0, 0 }, { 19, 21, 0, 0, 0 }, { 20, 22, 23, 0, 0 }, { 21, 23, 32, 33, 0 }, { 21, 22, 24, 25, 0 }, { 1, 23, 25, 34, 0 }, { 23, 24, 34, 0, 0 }, { 8, 27, 28, 0, 0 }, { 4, 26, 28, 0, 0 }, { 26, 27, 0, 0, 0 }, { 11, 0, 0, 0, 0 }, { 15, 16, 0, 0, 0 }, { 16, 17, 18, 0, 0 }, { 6, 22, 33, 0, 0 }, { 6, 19, 22, 32, 0 }, { 1, 24, 25, 0, 0 } };

    private static string[] AreaNames = new string[34] { "Alnerwick", "Bardford", "Holden", "Ashborne", "Aramore", "Gilramore", "Tarmsworth", "Lancaster", "Shadowfen", "Jueht Fields", "Kiahs Grassland", "Cloud Prairie", "Strotyl Plateau", "Boa Valley", "Giant's Expanse", "Grasshopper Plains", "Mutolm Meadow", "Kicalt Fields", "Great Meadow", "Great Plains", "Sacred Grasslands", "Abandoned Fields", "Ruehn's Expanse", "Lazy Foot Gardens", "Gilivore Prairie", "Knife Range", "The Parched Fields", "The Angry Wilds", "The Red Sea", "Desert of Akrid", "Unresting Barrens", "Desolated Savanna", "The Sea of Fire", "The Wasteland" };
    //0-8 are cities, 9-25 are plains, 26-33 are deserts
    private static int[] AreaIDs = new int[34] { 1, 32, 19, 29, 14, 16, 23, 7, 28, 2, 3, 4, 5, 11, 12, 15, 17, 18, 20, 22, 24, 27, 30, 31, 33, 34, 21, 6, 8, 9, 10, 13, 25, 26 };

    private static string[] KingNames = new string[10] { "Toni", "Dan", "Kimothy III", "Wolfgang", "Alexander", "Vybihal", "Queen Narges", "Trottier", "Tom", "Marty" };
    //private string url = "http://tomaswolfgang.com/hansa361/GetWorldInformation.php";

    public static JsonData AreaData;
    public static List<Area> Areas { get; set; }
    public static Inventory shopInv = new Inventory();
    public static List<Quest> AvailableQuests { get; set; }

    public static List<Kingdom> Kingdoms { get; set; }
    public static Quest CurrentQuest { get; set; }
    public static Quest Control { get; set; }

    public static int Attacker { get; set; }
    public static int DayCounter { get; set; }

    private static CreateNewWeapon WeaponCreator = new CreateNewWeapon();
    private static CreateNewEquipment EquipmentCreator = new CreateNewEquipment();
    private static CreateNewPotion PotionCreator = new CreateNewPotion();
    private static CreateNewQuest questCreator = new CreateNewQuest();

    private int areaNamer = 0;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void InitAreas() {
        Areas = new List<Area>();
        for (int i = 0; i < 34; i++) {
            Areas.Add(InitArea());
        }
    }

    private void InitKingdoms() {
        Kingdoms = new List<Kingdom>();
        for (int i = 0; i < KingdomCount; i++) {
            Kingdoms.Add(InitKingdom(i));
            //PrintKingdomInfo(Kingdoms[i]);
        }
    }

    private Kingdom InitKingdom(int i) {
        Kingdom newKingdom = new Kingdom {
            KingdomID = i,
            KingName = KingNames[i]
        };
        return newKingdom;
    }

    private Area InitArea() {
        //Gets this info from db
        Area newArea = new Area {
            AreaID = AreaIDs[areaNamer],
            AreaName = AreaNames[areaNamer]
        };

        if (areaNamer <= 8) {
            newArea.AreaType = Area.AreaTypes.City;
        } else if (areaNamer <= 25) {
            newArea.AreaType = Area.AreaTypes.Plains;
        } else {
            newArea.AreaType = Area.AreaTypes.Desert;
        }

        newArea.IconNumber = AreaIDs[areaNamer];
        //Debug.Log("Area created : " + newArea.IconNumber);
        areaNamer++;
        return newArea;
    }

    private void PrintAreaInfo(Area a) {
        Debug.Log(a.AreaID + ". ------ . " + a.AreaName + ".---------." + a.AreaType.ToString() + ".-------.Owned By:" + a.DefendingKingdom.KingdomID + "    -------   Enemy:" + a.AttackingKingdom.KingdomID);
    }

    private void PrintKingdomInfo(Kingdom a) {
        Debug.Log(a.KingdomID + " king id ----------- " + a.KingName + "King name");
    }

    public void StartWorld() {

        if (KingdomCount <= 0) {
            KingdomCount = 6;
        }

        InitKingdoms();
        InitAreas();
        CurrentArea = "1";  

        for (int i = 0; i < 34; i++) {
            int rand = rnd.Next(0, KingdomCount);
            Area holder = Areas[i];
            holder.DefendingKingdom = Kingdoms.Find(x => x.KingdomID == rand);
        }

        InitShopsAndQuests();

        SceneManager.LoadScene("MainMap");
    }

    private void InitShopsAndQuests() {
        AvailableQuests = new List<Quest>();
        LoadNewQuests();
        RenewShopInv();
    }

    public static void LoadNewQuests() {
        //Loads new quests for the quest keep

        for (int i = 0; i < 3; i++) {
            //Debug.Log("Initiating quests in world info!");
            //Debug.Log("Is there a problem with this? " + questCreator.returnQuest().QuestName);
            AvailableQuests.Add(questCreator.ReturnQuest());
        }
    }

    public static void RenewShopInv() {
        List<BaseWeapon> weaponArr = new List<BaseWeapon>();
        List<BasePotion> potionArr = new List<BasePotion>();
        List<BaseEquipment> equipmentArr = new List<BaseEquipment>();

        for (int i = 0; i < 5; i++) {
            weaponArr.Add(WeaponCreator.returnWeapon());
            potionArr.Add(PotionCreator.returnPotion());
            equipmentArr.Add(EquipmentCreator.returnEquipment());
        }

        shopInv.Equipment = equipmentArr;
        shopInv.Potions = potionArr;
        shopInv.Weapons = weaponArr;

    }
}
