﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LoadInventoryPause : MonoBehaviour {

    public GameObject InvContent;
    public GameObject IDP; //inventory detail panel
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
        text.text = weapon.ItemName;

        Name = button.transform.GetChild(1).gameObject; // price
        text = Name.GetComponent<Text>();
        text.text = "Price: $ " + weapon.Price;

        Name = button.transform.GetChild(2).gameObject; // stats
        text = Name.GetComponent<Text>();
        text.text = "Strength: " + weapon.Strength + "\nIntellect: " + weapon.Intellect + "\nAgility : " + weapon.Agility + " \nDefense: " + weapon.Defense;

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
        text.text = equip.ItemName;

        Name = button.transform.GetChild(1).gameObject; // price
        text = Name.GetComponent<Text>();
        text.text = "Price: $ " + equip.Price;

        Name = button.transform.GetChild(2).gameObject; // stats
        text = Name.GetComponent<Text>();
        text.text = "Strength: " + equip.Strength + "\nIntellect: " + equip.Intellect + "\nAgility : " + equip.Agility + " \nDefense: " + equip.Defense;
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
        text.text = potion.ItemName;

        Name = button.transform.GetChild(1).gameObject; // price
        text = Name.GetComponent<Text>();
        text.text = "Price: $ " + potion.Price;

        Name = button.transform.GetChild(2).gameObject; // stat
        text = Name.GetComponent<Text>();
        text.text = "Effectiveness: " + potion.Effectiveness;

    }

    private void SetListenerPotion(Button B, BasePotion p) {

        B.onClick.AddListener(delegate { ShowInventoryDetailPotion(p); });

    }

    private void SetListenerEquipment(Button B, BaseEquipment p) {

        B.onClick.AddListener(delegate { ShowInventoryDetailEquipment(p); });

    }

    private void SetListenerWeapon(Button B, BaseWeapon p) {

        B.onClick.AddListener(delegate { ShowInventoryDetailWeapon(p); });

    }

    private void ShowInventoryDetailPotion(BasePotion p) {
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

        InvDetailPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = p.ItemName;
        InvDetailPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Price: $ " + p.Price;
        InvDetailPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = p.ItemDescription;
        InvDetailPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Effectiveness: " + p.Effectiveness;
        InvDetailPanel.transform.GetChild(6).gameObject.SetActive(false);

        GameObject B = InvDetailPanel.transform.GetChild(6).gameObject;                 // no equip button
        B.SetActive(false);
        InvDetailPanel.SetActive(true);
    }

    private void ShowInventoryDetailEquipment(BaseEquipment p) {
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

        InvDetailPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = p.ItemName;
        InvDetailPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Price: $ " + p.Price;
        InvDetailPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = p.ItemDescription;
        InvDetailPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Strength: " + p.Strength + "\nIntellect: " + p.Intellect + "\nAgility : " + p.Agility + " \nDefense: " + p.Defense;

        GameObject B = InvDetailPanel.transform.GetChild(6).gameObject;
        B.SetActive(true);
        B.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Equip";
        InvDetailPanel.SetActive(true);
        B.GetComponent<Button>().onClick.RemoveAllListeners();
        B.GetComponent<Button>().onClick.AddListener(delegate { EquipEquipment(p); });

    }

    private void ShowInventoryDetailWeapon(BaseWeapon p) {
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

        InvDetailPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = p.ItemName;
        InvDetailPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Price: $ " + p.Price;
        InvDetailPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = p.ItemDescription;
        InvDetailPanel.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Strength: " + p.Strength + "\nIntellect: " + p.Intellect + "\nAgility : " + p.Agility + " \nDefense: " + p.Defense;

        InvDetailPanel.SetActive(true);
        GameObject button = InvDetailPanel.transform.GetChild(6).gameObject;
        button.SetActive(true);
        button.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Equip";
        button.GetComponent<Button>().onClick.RemoveAllListeners();
        button.GetComponent<Button>().onClick.AddListener(delegate { EquipWeapon(p); });

    }
   
    private void EquipWeaponOnChar(BaseCharacter c, BaseWeapon w) {
        //unequip current weapon if there is one
        if (c.Weapon != null) {
            GameInformation.PlayerInventory.Weapons.Add(c.Weapon);
            GameInformation.PlayerCharacter.Intellect -= c.Weapon.Intellect;
            GameInformation.PlayerCharacter.Strength -= c.Weapon.Strength;
            GameInformation.PlayerCharacter.Agility -= c.Weapon.Agility;
            GameInformation.PlayerCharacter.Defense -= c.Weapon.Defense;
        }

        //equip weapon
        c.Weapon = w;
        GameInformation.PlayerInventory.Weapons.Remove(c.Weapon);
        GameInformation.PlayerCharacter.Intellect += c.Weapon.Intellect;
        GameInformation.PlayerCharacter.Strength += c.Weapon.Strength;
        GameInformation.PlayerCharacter.Agility += c.Weapon.Agility;
        GameInformation.PlayerCharacter.Defense += c.Weapon.Defense;

        InvDetailPanel.SetActive(false);
        characterChoosePanel.SetActive(false);
        ShowAllWeapons();
    }

    private void EquipEquipmentOnChar(BaseCharacter c, BaseEquipment w) {
        switch (w.EquipmentType)  // determine equipment type
        {
            case BaseEquipment.EquipmentTypes.Armor:
                //unequip current armor if there is one
                if (c.Armor != null) {
                    GameInformation.PlayerCharacter.Intellect -= w.Intellect;
                    GameInformation.PlayerCharacter.Strength -= w.Strength;
                    GameInformation.PlayerCharacter.Agility -= w.Agility;
                    GameInformation.PlayerCharacter.Defense -= w.Defense;
                    GameInformation.PlayerInventory.Equipment.Add(c.Armor);
                }
                c.Armor = w;
                break;
            case BaseEquipment.EquipmentTypes.Gauntlets:
                //unequip current gauntlets if there is one
                if (c.Armor != null) {
                    GameInformation.PlayerInventory.Equipment.Add(c.Gauntlets);
                    GameInformation.PlayerCharacter.Intellect -= c.Gauntlets.Intellect;
                    GameInformation.PlayerCharacter.Strength -= c.Gauntlets.Strength;
                    GameInformation.PlayerCharacter.Agility -= c.Gauntlets.Agility;
                    GameInformation.PlayerCharacter.Defense -= c.Gauntlets.Defense;
                }
                c.Gauntlets = w;
                break;
            case BaseEquipment.EquipmentTypes.Grieves:
                //unequip current Grieves if there is one
                if (c.Armor != null) {
                    GameInformation.PlayerInventory.Equipment.Add(c.Grieves);
                    GameInformation.PlayerCharacter.Intellect -= c.Grieves.Intellect;
                    GameInformation.PlayerCharacter.Strength -= c.Grieves.Strength;
                    GameInformation.PlayerCharacter.Agility -= c.Grieves.Agility;
                    GameInformation.PlayerCharacter.Defense -= c.Grieves.Defense;
                }
                c.Grieves = w;
                break;
            case BaseEquipment.EquipmentTypes.Helmet:
                //unequip current Helmet if there is one
                if (c.Armor != null) {
                    GameInformation.PlayerInventory.Equipment.Add(c.Helmet);
                    GameInformation.PlayerCharacter.Intellect -= c.Helmet.Intellect;
                    GameInformation.PlayerCharacter.Strength -= c.Helmet.Strength;
                    GameInformation.PlayerCharacter.Agility -= c.Helmet.Agility;
                    GameInformation.PlayerCharacter.Defense -= c.Helmet.Defense;
                }
                c.Helmet = w;
                break;

        }

        //equip item
        GameInformation.PlayerInventory.Equipment.Remove(w);
        GameInformation.PlayerCharacter.Intellect += w.Strength;
        GameInformation.PlayerCharacter.Agility += w.Agility;
        GameInformation.PlayerCharacter.Defense += w.Defense;

        InvDetailPanel.SetActive(false);
        characterChoosePanel.SetActive(false);
        ShowAllEquipments();
    }

    private void EquipWeapon(BaseWeapon w) {   // reload all character information

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
                Transform characterButtons = characterChoosePanel.transform.GetChild(i); // all the char buttons
                characterButtons.GetChild(0).gameObject.GetComponent<Text>().text = chars[i].PlayerName; // set player name
                switch (w.WeaponType) // according to weapon type and class
                {
                    case BaseWeapon.WeaponTypes.Spear:
                    case BaseWeapon.WeaponTypes.Sword:
                        if (chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Paladin || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Knight || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Squire) {
                            Button Button = characterButtons.gameObject.GetComponent<Button>();
                            Button.interactable = true;
                            BaseCharacter bc = chars[i];
                            Button.onClick.RemoveAllListeners();
                            Button.onClick.AddListener(delegate { EquipWeaponOnChar(bc, w); });
                        } else {
                            characterButtons.gameObject.GetComponent<Button>().interactable = false;
                        }
                        break;
                    case BaseWeapon.WeaponTypes.Bow:
                        if (chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Archer || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Sniper || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Hunter) {
                            Button Button = characterButtons.gameObject.GetComponent<Button>();
                            Button.interactable = true;
                            BaseCharacter bc = chars[i];
                            Button.onClick.RemoveAllListeners();
                            Button.onClick.AddListener(delegate { EquipWeaponOnChar(bc, w); });
                        } else {
                            characterButtons.gameObject.GetComponent<Button>().interactable = false;
                        }
                        break;
                    case BaseWeapon.WeaponTypes.Dagger:
                        if (chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Thief || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Ninja || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Assassin) {
                            Button Button = characterButtons.gameObject.GetComponent<Button>();
                            Button.interactable = true;
                            BaseCharacter bc = chars[i];
                            Button.onClick.RemoveAllListeners();
                            Button.onClick.AddListener(delegate { EquipWeaponOnChar(bc, w); });
                        } else {
                            characterButtons.gameObject.GetComponent<Button>().interactable = false;
                        }
                        break;
                    case BaseWeapon.WeaponTypes.Tomb:
                        if (chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Apprentice || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.ArchMage || chars[i].PlayerClass == BaseCharacterClass.CharacterClasses.Mage) {
                            Button Button = characterButtons.gameObject.GetComponent<Button>();
                            Button.interactable = true;
                            BaseCharacter bc = chars[i];
                            Button.onClick.RemoveAllListeners();
                            Button.onClick.AddListener(delegate { EquipWeaponOnChar(bc, w); });
                        } else {
                            characterButtons.gameObject.GetComponent<Button>().interactable = false;
                        }
                        break;
                }
            } else {
                Transform characterButtons = characterChoosePanel.transform.GetChild(i); // all the char buttons
                characterButtons.GetChild(0).gameObject.SetActive(false); // disable button
                characterButtons.gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    private void EquipEquipment(BaseEquipment w) {   // reload all character information

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
                Button.onClick.AddListener(delegate { EquipEquipmentOnChar(bc, w); });

            } else {
                Transform characterButtons = characterChoosePanel.transform.GetChild(i); // all the char buttons
                characterButtons.GetChild(0).gameObject.SetActive(false); // disable button
                characterButtons.gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }
}
