using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacterMainMap : MonoBehaviour {
    public GameObject HudContent;
    public GameObject ShopButton;
    public GameObject AreaText;

    // Use this for initialization
    void Start () {
        FirstPerson.SpawnPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
