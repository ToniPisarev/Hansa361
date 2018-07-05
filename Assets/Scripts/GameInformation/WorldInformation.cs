using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Linq;
using LitJson;

//Stores all the world information -- This is what is going to get loaded from online
public class WorldInformation : MonoBehaviour {

    public static int CurrentArea { get; set; }
    public static string UserID { get; set; }
    public static int KingdomCount { get; set; }
    public static string CurrentWorldID { get; set; }

    private static readonly string[] AreaNames = new string[34] { "Alnerwick", "Bardford", "Holden", "Ashborne", "Aramore", "Gilramore", "Tarmsworth", "Lancaster", "Shadowfen", "Jueht Fields", "Kiahs Grassland", "Cloud Prairie", "Strotyl Plateau", "Boa Valley", "Giant's Expanse", "Grasshopper Plains", "Mutolm Meadow", "Kicalt Fields", "Great Meadow", "Great Plains", "Sacred Grasslands", "Abandoned Fields", "Ruehn's Expanse", "Lazy Foot Gardens", "Gilivore Prairie", "Knife Range", "The Parched Fields", "The Angry Wilds", "The Red Sea", "Desert of Akrid", "Unresting Barrens", "Desolated Savanna", "The Sea of Fire", "The Wasteland" };
    //0-8 are cities, 9-25 are plains, 26-33 are deserts
    private static readonly int[] AreaIDs = new int[34] { 1, 32, 19, 29, 14, 16, 23, 7, 28, 2, 3, 4, 5, 11, 12, 15, 17, 18, 20, 22, 24, 27, 30, 31, 33, 34, 21, 6, 8, 9, 10, 13, 25, 26 };

    private static readonly string[] KingNames = new string[10] { "Toni", "Dan", "Kimothy III", "Wolfgang", "Alexander", "Vybihal", "Queen Narges", "Trottier", "Tom", "Marty" };

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
        CurrentArea = 1;

        for (int i = 0; i < 34; i++) {
            int rand = UnityEngine.Random.Range(0, KingdomCount - 1);
            Areas[i].DefendingKingdom = Kingdoms.Find(x => x.KingdomID == rand);
        }

        InitShopsAndQuests();

        SceneManager.LoadScene("MainMap");
    }

    public static void InitShopsAndQuests() {
        AvailableQuests = new List<Quest>();
        LoadNewQuests();
        RenewShopInventory();
    }

    public static void LoadNewQuests() {
        //Loads new quests for the quest keep
        for (int i = 0; i < 3; i++) {
            AvailableQuests.Add(questCreator.ReturnQuest());
        }
    }

    public static void RenewShopInventory() {
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
