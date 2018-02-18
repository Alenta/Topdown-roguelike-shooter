using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

    public GameObject currentInterObj = null;
    public InteractionObject currentInterObjScript = null;
    
    public PlayerController player;
    public Inventory inventory;
    private GameObject weapon;
    private GameObject oldWeapon;
    private bool isColliding;
    private GameObject weaponPickup;
    private WeaponReference weaponReference;
    private bool weaponPickedUp;
    private void Start()
    {
        player = GetComponent<PlayerController>();
        weaponReference = player.activeSlot.transform.GetChild(0).GetComponent<WeaponReference>();
    }

    private void Update()
    {
        weaponPickedUp = false;

        isColliding = false;
        if (Input.GetButtonDown("Interact") && currentInterObj)
        {
            //check to see if this object is to be stored in inventory
            if (currentInterObjScript.inventory)
            {
                inventory.AddItem(currentInterObj);
            }
            if (currentInterObjScript.equippable)
            {

                oldWeapon = player.activeSlot.transform.GetChild(0).GetComponent<WeaponReference>().pickupReference;
                GameObject unequippedWeapon = Instantiate(oldWeapon, player.transform.position, Quaternion.identity);

                inventory.AddItem(weapon);
                Destroy(player.activeSlot.transform.GetChild(0).gameObject);
                weaponPickedUp = true;


            }

            //check to see if this object can be opened
            if (currentInterObjScript.openable)
            {
                //check to see if the object is locked
                if (currentInterObjScript.locked)
                {
                    //check to see if we have the object needed to unlock
                    //search our inventory for item needed, if found , unlock object
                    if (inventory.FindItem(currentInterObjScript.ItemNeeded))
                    {
                        //we found the item needed
                        currentInterObjScript.locked = false;
                        Debug.Log(currentInterObj.name + " was unlocked");
                    }
                    else
                    {
                        Debug.Log(currentInterObj.name + (" is locked"));
                    }
                }
                else
                {
                    //object is not locked - open the object
                    Debug.Log(currentInterObj.name + (" is open"));

                }
            }
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (isColliding) return;
        isColliding = true;
        if (other.CompareTag("InteractableObject"))
        {
            
            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();
        }
        if (other.gameObject.tag == "Weapon")
        {
            
            if (player.activeSlot.transform.GetChild(0).tag == "Unarmed")
            {
                weapon = other.gameObject.GetComponent<WeaponReference>().gunReference;
                weaponPickup = other.gameObject.GetComponent<WeaponReference>().pickupReference;
                
                
                inventory.AddItem(weapon);
                Destroy(player.activeSlot.transform.GetChild(0).gameObject);
                Destroy(other.gameObject);



            }

            if (player.activeSlot.transform.GetChild(0).tag == "Weapon")
            {
                currentInterObj = other.gameObject;
                currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();
                weapon = other.gameObject.GetComponent<WeaponReference>().gunReference;
                weaponPickup = other.gameObject.GetComponent<WeaponReference>().pickupReference;
                

            }


        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            if (weaponPickedUp)
            {
                Destroy(other.gameObject);
                print("Destroy one gun");
            }
        }
            



    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InteractableObject"))
        {
            if (other.gameObject == currentInterObj)
            {

                currentInterObj = null;
            }
        }
    }
}
