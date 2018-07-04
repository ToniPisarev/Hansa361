using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.UI;
using System.Collections.Generic;

//Display store inventory - allows players to buy and sell items
public class GetStoreInventory : MonoBehaviour {

    public GameObject shopPanel;
    public GameObject shopContent;
    public GameObject storeOwner;
    public GameObject title;
    public GameObject shopBack;
    public GameObject buyButton;
    public GameObject sellButton;
    public GameObject shopKeepPanel;

    private int height;
    private int index = 0;
    private int switcher = 0;

    void Start() {       
        LoadInformation.LoadAllInformation();
        height = -20;
    }

    public void ExitShop() {

        PlayerPrefs.SetInt("GOLD", GameInformation.Gold);
        if (GameInformation.PlayerInventory != null) { PlayerPref.Save("PLAYERINVENTORY", GameInformation.PlayerInventory); }

        shopPanel.SetActive(false);
        storeOwner.SetActive(true);
        shopBack.SetActive(false);
        shopKeepPanel.SetActive(true);
    }

    public void DisplayStoreInventory() {

        height = -20;
        index = 0;
        switcher = 0;

        shopKeepPanel.SetActive(false);
        shopPanel.SetActive(true);
        shopBack.SetActive(true);
        storeOwner.SetActive(false);
        sellButton.SetActive(true);
        buyButton.SetActive(false);

        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in shopContent.transform) { children.Add(child.gameObject); }
        children.ForEach(child => Destroy(child));

        Text text = title.GetComponent<Text>();
        text.text = "Shop Inventory";

        GameObject.Find("PlayerGold").GetComponent<Text>().text = "Gold: " + GameInformation.Gold;
        WorldInformation.shopInv.Weapons.ForEach(ShowWeaponInStore);
        WorldInformation.shopInv.Equipment.ForEach(ShowEquipmentInStore);
        WorldInformation.shopInv.Potions.ForEach(ShowPotionInStore);
        ResizeView();
    }

    public void DisplayPlayerInventory() {

        PlayerPrefs.SetInt("GOLD", GameInformation.Gold);
        if (GameInformation.PlayerInventory != null) { PlayerPref.Save("PLAYERINVENTORY", GameInformation.PlayerInventory); }

        height = -20;
        switcher = 1;
        index = 0;

        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in shopContent.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
       
        sellButton.SetActive(false);
        buyButton.SetActive(true);

        Text text = title.GetComponent<Text>();
        text.text = "Your Inventory";

        GameObject.Find("PlayerGold").GetComponent<Text>().text = "Gold: " + GameInformation.Gold;
        GameInformation.PlayerInventory.Weapons.ForEach(ShowWeaponInStore);
        GameInformation.PlayerInventory.Equipment.ForEach(ShowEquipmentInStore);
        GameInformation.PlayerInventory.Potions.ForEach(ShowPotionInStore);
        ResizeView();
    }

    private void ResizeView() {
        RectTransform ViewRect = (RectTransform)shopContent.transform;
        ViewRect.sizeDelta = new Vector2(0, height * (-1));
    }

    private void SetListener(Button button) {
        int i = index;
        button.onClick.AddListener(delegate { Buy(i); });
    }

    public void ShowWeaponInStore(BaseWeapon weapon) {
        GameObject button = (GameObject)Instantiate(Resources.Load("WeaponButton"));
        button.transform.SetParent(shopContent.transform);
        SetListener(button.GetComponent<Button>());
        index++;

        RectTransform ButtonRect = (RectTransform)button.transform;
        ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
        ButtonRect.localScale = new Vector3(1, 1, 1);
        height -= 110;

        GameObject Name = button.transform.GetChild(0).gameObject;
        Text text = Name.GetComponent<Text>();
        text.text = weapon.ItemName;

        Name = button.transform.GetChild(1).gameObject;
        text = Name.GetComponent<Text>();
        if (switcher == 0) { text.text = "Price: " + weapon.Price; } else { text.text = "Sell: " + weapon.Price; }

        Name = button.transform.GetChild(2).gameObject;
        text = Name.GetComponent<Text>();
        text.text = "Damage: " + weapon.Damage + "\nStrength: " + weapon.Strength + "\nIntellect: " + weapon.Intellect + "\nAgility : " + weapon.Agility + " \nDefense: " + weapon.Defense;

    }

    public void ShowEquipmentInStore(BaseEquipment equip) {

        GameObject button = (GameObject)Instantiate(Resources.Load("WeaponButton"));
        button.transform.SetParent(shopContent.transform);
        SetListener(button.GetComponent<Button>());
        index++;

        RectTransform ButtonRect = (RectTransform)button.transform;
        ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
        ButtonRect.localScale = new Vector3(1, 1, 1);
        height -= 110;

        GameObject Name = button.transform.GetChild(0).gameObject;
        Text text = Name.GetComponent<Text>();
        text.text = equip.ItemName;

        Name = button.transform.GetChild(1).gameObject;
        text = Name.GetComponent<Text>();
        if (switcher == 0) { text.text = "Price: " + equip.Price; } else { text.text = "Sell: " + equip.Price; }

        Name = button.transform.GetChild(2).gameObject;
        text = Name.GetComponent<Text>();
        text.text = "Resistance: " + equip.Resistance + "\nStrength: " + equip.Strength + "\nIntellect: " + equip.Intellect + "\nAgility : " + equip.Agility + " \nDefense: " + equip.Defense;

    }
    public void ShowPotionInStore(BasePotion potion) {
        GameObject button = (GameObject)Instantiate(Resources.Load("WeaponButton"));
        button.transform.SetParent(shopContent.transform);
        SetListener(button.GetComponent<Button>());
        index++;

        RectTransform ButtonRect = (RectTransform)button.transform;
        ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
        ButtonRect.localScale = new Vector3(1, 1, 1);
        height -= 110;

        GameObject Name = button.transform.GetChild(0).gameObject;
        Text text = Name.GetComponent<Text>();
        text.text = potion.ItemName;

        Name = button.transform.GetChild(1).gameObject;
        text = Name.GetComponent<Text>();
        if (switcher == 0) { text.text = "Price: " + potion.Price; } else { text.text = "Sell: " + potion.Price; }

        Name = button.transform.GetChild(2).gameObject;
        text = Name.GetComponent<Text>();
        text.text = "Effectiveness: " + potion.Effectiveness;

    }

    public void Buy(int itemIndex) {
        Inventory TargetInv = new Inventory();
        Inventory OtherInv = new Inventory();
        string option = "";
        int price = 0;

        if (switcher == 0) {
            TargetInv = WorldInformation.shopInv;
            OtherInv = GameInformation.PlayerInventory;
            option = "bought";
            price = 1;
        } else {
            TargetInv = GameInformation.PlayerInventory;
            OtherInv = WorldInformation.shopInv;
            option = "sold";
            price = -1;
        }

        //Debug.Log(itemIndex);
        if (itemIndex < TargetInv.Weapons.Count) {
            BaseWeapon BoughtItem = TargetInv.Weapons[itemIndex];
            Debug.Log("You just " + option + ": " + BoughtItem.ItemName);
            GameInformation.Gold = GameInformation.Gold - BoughtItem.Price * price;
            OtherInv.Weapons.Add(BoughtItem);
            TargetInv.Weapons.Remove(BoughtItem);

        } else if (itemIndex - TargetInv.Weapons.Count < TargetInv.Equipment.Count) {
            BaseEquipment BoughtItem = TargetInv.Equipment[itemIndex - TargetInv.Weapons.Count];
            Debug.Log("You just " + option + ": " + BoughtItem.ItemName);
            GameInformation.Gold = GameInformation.Gold - BoughtItem.Price * price;
            OtherInv.Equipment.Add(BoughtItem);
            TargetInv.Equipment.Remove(BoughtItem);

        } else {
            BasePotion BoughtItem = TargetInv.Potions[itemIndex - TargetInv.Weapons.Count - TargetInv.Equipment.Count];
            Debug.Log("You just " + option + ": " + BoughtItem.ItemName);
            GameInformation.Gold = GameInformation.Gold - BoughtItem.Price * price;
            OtherInv.Potions.Add(BoughtItem);
            TargetInv.Potions.Remove(BoughtItem);
        }

        Debug.Log("YOUR INVENTORY");
        GameInformation.PlayerInventory.printInventory();
        //Debug.Log("SHOP INVENTORY");
        //WorldInformation.shopInv.printInventory();

        if (switcher == 0) { DisplayStoreInventory(); } else { DisplayPlayerInventory(); }
    }

}
