using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public GameObject[] inventory = new GameObject[10];
    private PlayerController player;
    public GameObject equippedWeapon;
    public float ammo = 20;
    public float bombs;
    public float keys;
    public float money;
    // Use this for initialization
    void Start()
    {
        player = this.gameObject.GetComponentInParent<PlayerController>();
        
    }
    public void AddItem(GameObject item)
    {
        bool itemAdded = false;
        if (item.tag == "Weapon")
        {
            GameObject weapon = Instantiate(item, player.activeSlot.transform);
            equippedWeapon = weapon;
            
            print("Make one gun");
        }
        else
        {
            for (int i = 0; i < inventory.Length; i++)
            {

                if (inventory[i] == null)
                {
                    inventory[i] = item;
                    Debug.Log(item.name + " was added!");
                    itemAdded = true;
                    //do something with the object
                    item.SendMessage("DoInteraction");
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
}
