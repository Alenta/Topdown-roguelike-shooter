using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public GameObject itemSlot;
    public Inventory inventory;
    private Image sprite;
	// Use this for initialization
	void Start () {
        sprite = GetComponent<Image>();
        

    }
	
	// Update is called once per frame
	void Update () {
        itemSlot = inventory.inventory[0]; //flytt denne til en funksjon som kan calles.
        if (itemSlot != null)
        {
            sprite.sprite = inventory.inventory[0].GetComponent<SpriteRenderer>().sprite;
            sprite.enabled = true;
        }
    }
}
