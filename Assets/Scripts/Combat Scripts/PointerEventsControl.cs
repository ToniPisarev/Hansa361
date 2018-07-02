using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerEventsControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private AudioClip audioclip;
    private Transform abilityPanel;
    public CharController charControl;
    public BaseCharacter myCharacter;
    public int i;

    public void Start() {
        audioclip = Resources.Load<AudioClip>("button_hover_sound");
        gameObject.AddComponent<AudioSource>();
        abilityPanel = transform.parent.parent.Find("Ability Description");
    }

    public void OnPointerEnter(PointerEventData eventData) {
        abilityPanel.gameObject.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(audioclip);

        i = int.Parse(gameObject.name.Substring(2, 1));

        Text ability = abilityPanel.Find("Ability").GetComponent<Text>();
        Text range = abilityPanel.Find("Range").GetComponent<Text>();
        Text damage = abilityPanel.Find("Damage").GetComponent<Text>();
        Text weapon = abilityPanel.Find("Weapon").GetComponent<Text>();

        ability.text = myCharacter.skills[i - 1].description;
        damage.text = "Damage: " + myCharacter.skills[i - 1].damage;
        range.text = "Range: " + myCharacter.skills[i - 1].minRange + "-" + myCharacter.skills[i - 1].maxRange;
        weapon.text = "Weapon: " + charControl.weapon;
    }

    public void OnPointerExit(PointerEventData eventData) {
        abilityPanel.gameObject.SetActive(false);
    }

}