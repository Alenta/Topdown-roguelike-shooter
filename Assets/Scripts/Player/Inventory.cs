using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject[] inventory = new GameObject[10];
    public GameObject[] activeSlots = new GameObject[3];
    private PlayerController player;
    public GameObject cursor;
    private GameObject equippedWeapon;
    private GameObject weaponSlot1;
    private GameObject weaponSlot2;
    private GameObject itemSlot1;
    private Vector2 cursorPos;
    public float ammo = 20;
    public float bombs = 0;
    public float keys = 0;
    public float money = 0;
    private Text ammoText;
    private Text bombsText;
    private Text keysText;
    private Text moneyText;
    private InventorySlot inventorySlot;
    
    // Use this for initialization
    void Start()
    {
        player = this.gameObject.GetComponentInParent<PlayerController>();

        weaponSlot1 = player.transform.GetChild(1).GetChild(0).gameObject;

        weaponSlot2 = player.transform.GetChild(2).GetChild(0).gameObject;
        itemSlot1 = player.transform.GetChild(3).GetChild(0).gameObject;

        activeSlots[0] = weaponSlot1;
        activeSlots[1] = weaponSlot2;
        activeSlots[2] = itemSlot1;
        ammoText = transform.GetChild(0).GetChild(0).GetChild(4).GetChild(1).GetComponent<Text>();
        bombsText = transform.GetChild(0).GetChild(0).GetChild(4).GetChild(2).GetComponent<Text>();
        keysText = transform.GetChild(0).GetChild(0).GetChild(4).GetChild(3).GetComponent<Text>();
        moneyText = transform.GetChild(0).GetChild(0).GetChild(4).GetChild(4).GetComponent<Text>();
    }
    public void AddItem(GameObject item)
    {
        bool itemAdded = false;
        if (item.tag == "Weapon")
        {
            GameObject weapon = Instantiate(item, player.activeSlot.transform);
            equippedWeapon = weapon;
            
        }
        else
        {
            for (int i = 0; i < inventory.Length; i++)
            {

                if (inventory[i] == null)
                {
                    inventory[i] = item;
                    Debug.Log(item.name + " was added! " + i);
                    itemAdded = true;
                    //do something with the object
                    item.SendMessage("DoInteraction");
                    inventorySlot = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetComponent<InventorySlot>();
                    inventorySlot.UpdateItemSlot();
                    break;
                }

                //inventory was full

                if (!itemAdded)
                {
                    Debug.Log("inventory full");
                }


            }
        }
            //find first open slot in inventory
           
    }
    public bool FindItem(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
            {
                //we found the item
                return true;
            }
        }
        //item not found
        return false;
    }
    public GameObject FindItemByType(string itemType)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            //if (inventory[i].GetComponent<InteractionObject>().itemType == itemType)
            //    {   
            //        //we found an item of the type we were looking for
            //        return inventory[i];
            //    }
            
        }
        //item of type not founnd
        return null;
    }
    public void RemoveItem(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
                {
                    //we found an item - remove it
                    inventory[i] = null;
                    Debug.Log(item.name + (" was removed from inventory"));
                    break;
                }
        }
        
    
    }
    public void UpdateActiveSlots()
    {
        weaponSlot1 = player.transform.GetChild(1).GetChild(0).gameObject;
        weaponSlot2 = player.transform.GetChild(2).GetChild(0).gameObject;
        itemSlot1 = player.transform.GetChild(3).GetChild(0).gameObject;
        activeSlots[0] = weaponSlot1;
        activeSlots[1] = weaponSlot2;
        activeSlots[2] = itemSlot1;
        ammoText.text = "Ammo: " + ammo;
        bombsText.text = "Bombs: " + bombs;
        keysText.text = "Keys: " + keys;
        moneyText.text = "Money: " + money;
        for (int i = 0; i < 4; i++)
        {
            if( transform.GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetComponent<InventorySlot>() != null)
            {
                inventorySlot = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(i).GetComponent<InventorySlot>();
                inventorySlot.UpdateItemSlot();
            }
            
        }
        for (int i = 1; i < 3; i++)
        {
            if (transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<InventorySlot>() != null)
            {
                inventorySlot = transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<InventorySlot>();
                inventorySlot.UpdateItemSlot();
            }

        }

    }
}
