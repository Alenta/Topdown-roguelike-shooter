using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public GameObject itemSlot;
    public Inventory inventory;
    public int slotNr;
    public bool equipmentSlot;
    private Image sprite;
	// Use this for initialization
	void Start () {
        sprite = GetComponent<Image>();
        inventory = GetComponentInParent<Inventory>();
        itemSlot = inventory.inventory[slotNr];
    }
	
    public void UpdateItemSlot()
    {
        if (!equipmentSlot)
        {
            itemSlot = inventory.inventory[slotNr];
            
            if (itemSlot != null)
            {
                sprite.sprite = itemSlot.GetComponent<SpriteRenderer>().sprite;
                sprite.enabled = true;
            }
        }
        else
        {
            itemSlot = inventory.activeSlots[slotNr];

            if (itemSlot != null)
            {

                if(itemSlot.GetComponentInChildren<SpriteRenderer>().sprite != null)
                {
                    sprite.sprite = itemSlot.GetComponentInChildren<SpriteRenderer>().sprite;
                    sprite.enabled = true;
                }
                else
                {
                    print("No sprite");
                }
            }
        }
        
    }
}
