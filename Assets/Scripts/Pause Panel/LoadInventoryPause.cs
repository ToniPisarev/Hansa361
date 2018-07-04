using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LoadInventoryPause : MonoBehaviour {

    public GameObject InvContent;
    public GameObject IDP;
    public GameObject weaponButton;
    private GameObject InvDetailPanel;
    public GameObject characterChoosePanel;

    // Use this for initialization
    void Start() {
        InvDetailPanel = IDP;
    }

    public void ShowAllWeapons() {
        weaponButton.transform.GetComponent<Button>().Select();

        // Destroy all contents first
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in InvContent.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        int height = -10;
        if (GameInformation.PlayerInventory.Weapons != null) {
            foreach (BaseWeapon w in GameInformation.PlayerInventory.Weapons) {
                ShowWeapon(w, height);
                height -= 100;
            }
        }

        RectTransform r = (RectTransform)InvContent.transform;
        r.sizeDelta = new Vector2(0, height * (-1) + 10);
    }

    public void ShowAllEquipments() {
        // Destroy all contents first
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in InvContent.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        int height = -10;
        foreach (BaseEquipment e in GameInformation.PlayerInventory.Equipment) {
            ShowEquipment(e, height);
            height -= 100;
        }

        RectTransform r = (RectTransform)InvContent.transform;
        r.sizeDelta = new Vector2(0, height * (-1) + 10);
    }

    public void ShowAllPotions() {
        // Destroy all contents first
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in InvContent.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        int height = -10;
        foreach (BasePotion p in GameInformation.PlayerInventory.Potions) {
            ShowPotion(p, height);
            height -= 100;
        }

        RectTransform r = (RectTransform)InvContent.transform;
        r.sizeDelta = new Vector2(0, height * (-1) + 10);
    }

    private void ShowWeapon(BaseWeapon weapon, int height) {
        GameObject button = (GameObject)Instantiate(Resources.Load("InventoryButton"));
        button.transform.SetParent(InvContent.transform);
        SetListenerWeapon(button.GetComponent<Button>(), weapon);

        RectTransform ButtonRect = (RectTransform)button.transform;
        ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
        ButtonRect.localScale = new Vector3(1, 1, 1);

        GameObject Name = button.transform.GetChild(0).gameObject; // name
        Text text = Name.GetComponent<Text>();
        text.text = weapon.ItemName + "\n" + weapon.ItemRarity;

        Name = button.transform.GetChild(1).gameObject; // price
        text = Name.GetComponent<Text>();
        text.text = "Price: $ " + weapon.Price;

        Name = button.transform.GetChild(2).gameObject; // stats
        text = Name.GetComponent<Text>();
        text.text = "Damage: " + weapon.Damage + "\nStrength: " + weapon.Strength + "\nIntellect: " + weapon.Intellect + "\nAgility : " + weapon.Agility + " \nDefense: " + weapon.Defense;

    }

    private void ShowEquipment(BaseEquipment equip, int height) {
        GameObject button = (GameObject)Instantiate(Resources.Load("InventoryButton"));
        button.transform.SetParent(InvContent.transform);
        SetListenerEquipment(button.GetComponent<Button>(), equip);

        RectTransform ButtonRect = (RectTransform)button.transform;
        ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
        ButtonRect.localScale = new Vector3(1, 1, 1);

        GameObject Name = button.transform.GetChild(0).gameObject; // name
        Text text = Name.GetComponent<Text>();
        text.text = equip.ItemName + "\n" + equip.ItemRarity;

        Name = button.transform.GetChild(1).gameObject; // price
        text = Name.GetComponent<Text>();
        text.text = "Price: $ " + equip.Price;

        Name = button.transform.GetChild(2).gameObject; // stats
        text = Name.GetComponent<Text>();
        text.text = "Resistance: " + equip.Resistance + "\nStrength: " + equip.Strength + "\nIntellect: " + equip.Intellect + "\nAgility : " + equip.Agility + " \nDefense: " + equip.Defense;

    }
    private void ShowPotion(BasePotion potion, int height) {
        GameObject button = (GameObject)Instantiate(Resources.Load("InventoryButton"));
        button.transform.SetParent(InvContent.transform);
        SetListenerPotion(button.GetComponent<Button>(), potion);

        RectTransform ButtonRect = (RectTransform)button.transform;
        ButtonRect.anchoredPosition3D = new Vector3(0, height, 0);
        ButtonRect.localScale = new Vector3(1, 1, 1);

        GameObject Name = button.transform.GetChild(0).gameObject; // name
        Text text = Name.GetComponent<Text>();
        text.text = potion.ItemName + "\n" + potion.ItemRarity;

        Name = button.transform.GetChild(1).gameObject; // price
        text = Name.GetComponent<Text>();
        text.text = "Price: $ " + potion.Price;

        Name = button.transform.GetChild(2).gameObject; // stat
        text = Name.GetComponent<Text>();
        text.text = "Effectiveness: " + potion.Effectiveness;

    }

    private void SetListenerPotion(Button B, BasePotion p) {

        B.onClick.AddListener(delegate { showInventoryDetailPotion(p); });

    }

    private void SetListenerEquipment(Button B, BaseEquipment p) {

        B.onClick.AddListener(delegate { showInventoryDetailEquipment(p); });

    }

    private void SetListenerWeapon(Button B, BaseWeapon p) {

        B.onClick.AddListener(delegate { showInventoryDetailWeapon(p); });

    }

    private void showInventoryDetailPotion(BasePotion p) {
        Image i = InvDetailPanel.transform.GetChild(0).gameObject.GetComponent<Image>();
        switch (p.PotionType) {
            case BasePotion.PotionTypes.Health:
                i.sprite = Resources.Load<Sprite>("Images/RedPotion");
                break;
            case BasePotion.PotionTypes.Mana:
                i.sprite = Resources.Load<Sprite>("Images/BluePotion");
                break;
            case BasePotion.PotionTypes.Speed:
                i.sprite = Resources.Load<Sprite>("Images/GreenPotion");
                break;
            case BasePotion.PotionTypes.Defense:
                i.sprite = Resources.Load<Sprite>("Images/PurplePotion2");
                break;
            case BasePotion.PotionTypes.Strength:
                i.sprite = Resources.Load<Sprite>("Images/BrownPotion");
                break;
            case BasePotion.PotionTypes.Agility:
                i.sprite = Resources.Load<Sprite>("Images/YellowPotion");
                break;
            case BasePotion.PotionTypes.Intellect:
                i.sprite = Resources.Load<Sprite>("Images/PurplePotion");
                break;
        }

        InvDetailPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = p.ItemName + "\n" + p.ItemRarity;
        InvDetailPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Price: $ " + p.Price;
        InvDetailPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = p.ItemDescription;
        InvDetailPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Effectiveness: " + p.Effectiveness;
        InvDetailPanel.transform.GetChild(6).gameObject.SetActive(false);

        GameObject B = InvDetailPanel.transform.GetChild(6).gameObject;                 // no equip button
        B.SetActive(false);
        InvDetailPanel.SetActive(true);
    }

    private void showInventoryDetailEquipment(BaseEquipment p) {
        Image i = InvDetailPanel.transform.GetChild(0).gameObject.GetComponent<Image>();
        switch (p.EquipmentType) {
            case BaseEquipment.EquipmentTypes.Armor:
                i.sprite = Resources.Load<Sprite>("Images/Armor");
                break;
            case BaseEquipment.EquipmentTypes.Gauntlets:
                i.sprite = Resources.Load<Sprite>("Images/Gauntlets");
                break;
            case BaseEquipment.EquipmentTypes.Grieves:
                i.sprite = Resources.Load<Sprite>("Images/Grieves");
                break;
            case BaseEquipment.EquipmentTypes.Helmet:
                i.sprite = Resources.Load<Sprite>("Images/Helmet");
                break;

        }


        InvDetailPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = p.ItemName + "\n" + p.ItemRarity;
        InvDetailPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Price: $ " + p.Price;
        InvDetailPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = p.ItemDescription;
        InvDetailPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Resistance: " + p.Resistance + "\nStrength: " + p.Strength + "\nIntellect: " + p.Intellect + "\nAgility : " + p.Agility + " \nDefense: " + p.Defense;

        GameObject B = InvDetailPanel.transform.GetChild(6).gameObject;
        B.SetActive(true);
        B.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Equip";
        InvDetailPanel.SetActive(true);
        B.GetComponent<Button>().onClick.RemoveAllListeners();
        B.GetComponent<Button>().onClick.AddListener(delegate { equipEquipment(p); });

    }

    private void showInventoryDetailWeapon(BaseWeapon p) {
        Image i = InvDetailPanel.transform.GetChild(0).gameObject.GetComponent<Image>();

        switch (p.WeaponType) {
            case BaseWeapon.WeaponTypes.Sword:
                switch (p.ItemRarity) {
                    case BaseStatItem.ItemRaritys.Legendary:
                        i.sprite = Resources.Load<Sprite>("Images/Diamond-Sword-Icon");
                        break;
                    case BaseStatItem.ItemRaritys.Flawless:
                        i.sprite = Resources.Load<Sprite>("Images/Gold-Sword-Icon");
                        break;
                    case BaseStatItem.ItemRaritys.Great:
                        i.sprite = Resources.Load<Sprite>("Images/Iron-Sword-Icon");
                        break;
                    case BaseStatItem.ItemRaritys.Common:
                        i.sprite = Resources.Load<Sprite>("Images/Stone-Sword-Icon");
                        break;
                    case BaseStatItem.ItemRaritys.Rusty:
                        i.sprite = Resources.Load<Sprite>("Images/Wooden-Sword-Icon");
                        break;
                }
                break;
            case BaseWeapon.WeaponTypes.Spear:
                i.sprite = Resources.Load<Sprite>("Images/Spear");
                break;
            case BaseWeapon.WeaponTypes.Bow:
                i.sprite = Resources.Load<Sprite>("Images/Bow");
                break;
            case BaseWeapon.WeaponTypes.Dagger:
                i.sprite = Resources.Load<Sprite>("Images/Dagger");
                break;
            case BaseWeapon.WeaponTypes.Tomb:
                i.sprite = Resources.Load<Sprite>("Images/Book");
                break;
        }

        InvDetailPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = p.ItemName + "\n" + p.ItemRarity;
        InvDetailPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Price: $ " + p.Price;
        InvDetailPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = p.ItemDescription;
        InvDetailPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Damage: " + p.Damage + "\nStrength: " + p.Strength + "\nIntellect: " + p.Intellect + "\nAgility : " + p.Agility + " \nDefense: " + p.Defense;

        InvDetailPanel.SetActive(true);
        GameObject B = InvDetailPanel.transform.GetChild(6).gameObject;
        B.SetActive(true);
        B.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Equip";
        B.GetComponent<Button>().onClick.RemoveAllListeners();
        B.GetComponent<Button>().onClick.AddListener(delegate { equipWeapon(p); });

    }
    private void equipWeaponOnChar(BaseCharacter c, BaseWeapon w) {
        if (c.Weapon != null) {
            GameInformation.PlayerInventory.Weapons.Add(c.Weapon);
        }
        c.Weapon = w;                                      // equip on c
        GameInformation.PlayerInventory.Weapons.Remove(w); // Remove w from inventory
        InvDetailPanel.SetActive(false);
        characterChoosePanel.SetActive(false);
        ShowAllWeapons();
    }

    private void equipEquipmentOnChar(BaseCharacter c, BaseEquipment w) {
        switch (w.EquipmentType)  // determine equipment type
        {
            case BaseEquipment.EquipmentTypes.Armor:
                if (c.Armor != null) // if theres an armor equipped
                {
                    GameInformation.PlayerInventory.Equipment.Add(c.Armor);
                }
                c.Armor = w;
                break;
            case BaseEquipment.EquipmentTypes.Gauntlets:
                if (c.Gauntlets != null) {
                    GameInformation.PlayerInventory.Equipment.Add(c.Gauntlets);
                }
                c.Gauntlets = w;
                break;
            case BaseEquipment.EquipmentTypes.Grieves:
                if (c.Grieves != null) {
                    GameInformation.PlayerInventory.Equipment.Add(c.Grieves);
                }
                c.Grieves = w;
                break;
            case BaseEquipment.EquipmentTypes.Helmet:
                if (c.Helmet != null) {
                    GameInformation.PlayerInventory.Equipment.Add(c.Helmet);
                }
                c.Helmet = w;
                break;

        }
        GameInformation.PlayerInventory.Equipment.Remove(w); // Remove w from inventory
        InvDetailPanel.SetActive(false);
        ShowAllEquipments();
    }

    private void equipWeapon(BaseWeapon w) {   // reload all character information

        characterChoosePanel.SetActive(true);

        BaseCharacter[] chars = new BaseCharacter[6];
        chars[0] = GameInformation.PlayerCharacter;

        if (GameInformation.SideCharacters != null) {
            for (int i = 0; i < GameInformation.SideCharacters.Length; i++) {
                chars[i + 1] = GameInformation.SideCharacters[i];
            }
        }

        for (int i = 0; i < 6; i++) {
            if (chars[i] != null) // if a character exists...
            {
                Transform B = characterChoosePanel.transform.GetChild(i); // all the char buttons
                B.GetChild(0).gameObject.GetComponent<Text>().text = chars[i].PlayerName; // set player name
                switch (w.WeaponType) // according to weapon type and class
                {
                    case BaseWeapon.WeaponTypes.Spear:
                    case BaseWeapon.WeaponTypes.Sword:
                        if (chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Paladin || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Knight || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Squire) {
                            Button Button = B.gameObject.GetComponent<Button>();
                            Button.interactable = true;
                            BaseCharacter bc = chars[i];
                            Button.onClick.RemoveAllListeners();
                            Button.onClick.AddListener(delegate { equipWeaponOnChar(bc, w); });
                        } else {
                            B.gameObject.GetComponent<Button>().interactable = false;
                        }
                        break;
                    case BaseWeapon.WeaponTypes.Bow:
                        if (chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Archer || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Sniper || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Hunter) {
                            Button Button = B.gameObject.GetComponent<Button>();
                            Button.interactable = true;
                            BaseCharacter bc = chars[i];
                            Button.onClick.RemoveAllListeners();
                            Button.onClick.AddListener(delegate { equipWeaponOnChar(bc, w); });
                        } else {
                            B.gameObject.GetComponent<Button>().interactable = false;
                        }
                        break;
                    case BaseWeapon.WeaponTypes.Dagger:
                        if (chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Thief || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Ninja || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Assassin) {
                            Button Button = B.gameObject.GetComponent<Button>();
                            Button.interactable = true;
                            BaseCharacter bc = chars[i];
                            Button.onClick.RemoveAllListeners();
                            Button.onClick.AddListener(delegate { equipWeaponOnChar(bc, w); });
                        } else {
                            B.gameObject.GetComponent<Button>().interactable = false;
                        }
                        break;
                    case BaseWeapon.WeaponTypes.Tomb:
                        if (chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Apprentice || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.ArchMage || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Mage) {
                            Button Button = B.gameObject.GetComponent<Button>();
                            Button.interactable = true;
                            BaseCharacter bc = chars[i];
                            Button.onClick.RemoveAllListeners();
                            Button.onClick.AddListener(delegate { equipWeaponOnChar(bc, w); });
                        } else {
                            B.gameObject.GetComponent<Button>().interactable = false;
                        }
                        break;
                }
            } else {
                Transform B = characterChoosePanel.transform.GetChild(i); // all the char buttons
                B.GetChild(0).gameObject.SetActive(false); // disable button
                B.gameObject.GetComponent<Button>().interactable = false;
            }
        }

    }

    private void equipEquipment(BaseEquipment w) {   // reload all character information

        BaseCharacter[] chars = new BaseCharacter[6];
        chars[0] = GameInformation.PlayerCharacter;

        if (GameInformation.SideCharacters != null) {
            for (int i = 0; i < GameInformation.SideCharacters.Length; i++) {
                chars[i + 1] = GameInformation.SideCharacters[i];
            }
        }

        characterChoosePanel.SetActive(true);

        for (int i = 0; i < 6; i++) {
            if (chars[i] != null) // if a character exists...
            {
                Transform B = characterChoosePanel.transform.GetChild(i); // all the char buttons
                B.GetChild(0).gameObject.GetComponent<Text>().text = chars[i].PlayerName; // set player name
                Button Button = B.gameObject.GetComponent<Button>();
                Button.interactable = true;
                BaseCharacter bc = chars[i];
                Button.onClick.RemoveAllListeners();
                Button.onClick.AddListener(delegate { equipEquipmentOnChar(bc, w); });

            } else {
                Transform B = characterChoosePanel.transform.GetChild(i); // all the char buttons
                B.GetChild(0).gameObject.SetActive(false); // disable button
                B.gameObject.GetComponent<Button>().interactable = false;
            }
        }

    }
}
