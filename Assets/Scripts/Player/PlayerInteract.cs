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
    private Chest chest;
    private bool ammoCollected;
    private float ammo;
    private float money;
    private float bombs;
    private float keys;
    private WeaponReference weaponReference;
    private bool weaponPickedUp;
    public Door door;
    private PlayerAttributes playerAttributes;
    private Attributes invAttributes;
    private PlayerAbilities playerAbilities;
    private PlayerAbilities invAbilities;
    private void Start()
    {
        player = GetComponent<PlayerController>();
        playerAbilities = GetComponent<PlayerAbilities>();
        playerAttributes = GetComponent<PlayerAttributes>();
        
    }

    private void FixedUpdate()
    {
        weaponPickedUp = false;
        ammoCollected = false;
        isColliding = false;
        if (Input.GetButtonDown("Interact") && currentInterObj)
        {
            //check to see if this object is to be stored in inventory

            if (currentInterObjScript.inventory)
            {
                inventory.AddItem(currentInterObj);
            }
            if (currentInterObjScript.equippable && currentInterObj != null)
            {

                oldWeapon = player.activeSlot.transform.GetChild(0).GetComponent<WeaponReference>().pickupReference;
                Instantiate(oldWeapon, player.transform.position + Random.insideUnitSphere, Quaternion.identity); //Instantiate gammelt våpen på bakken

                inventory.AddItem(weapon);
                Destroy(player.activeSlot.transform.GetChild(0).gameObject);
                weaponPickedUp = true;
                currentInterObj = null;


            }
            if (currentInterObjScript.hasAttributes)
            {
                playerAttributes.StatChange(currentInterObj.GetComponent<Attributes>());
                Destroy(currentInterObjScript.gameObject);
            }
            if (currentInterObjScript.hasAbilities)
            {
                print(currentInterObjScript.GetComponent<Abilities>().name);
                playerAbilities.AbilityChange(currentInterObj.GetComponent<Abilities>());
                Destroy(currentInterObjScript.gameObject);
            }
            if (currentInterObjScript.shop)
            {
                GameObject item = currentInterObjScript.itemContained;
                ItemInfo itemInfo = currentInterObjScript.itemContained.GetComponent<ItemInfo>();
                var totalPrice = currentInterObjScript.price + itemInfo.price;
                print(totalPrice);
                if(inventory.money - totalPrice >= 0)
                {
                    inventory.money = inventory.money - totalPrice;
                    if(item.tag == "Weapon")
                    {
                        if (player.activeSlot.transform.GetChild(0).tag == "Unarmed")
                        {
                            weapon = item.GetComponent<WeaponReference>().gunReference;
                            inventory.AddItem(weapon);
                            Destroy(player.activeSlot.transform.GetChild(0).gameObject);
                            
                            player.weapon = player.activeSlot.transform.GetChild(0).GetComponent<BaseWeapon>();
                            Destroy(currentInterObj.gameObject);
                        }

                        if (player.activeSlot.transform.GetChild(0).tag == "Weapon")
                        {
                           
                            weapon = item.gameObject.GetComponent<WeaponReference>().gunReference;
                            player.weapon = player.activeSlot.transform.GetChild(0).GetComponent<BaseWeapon>();
                            oldWeapon = player.activeSlot.transform.GetChild(0).GetComponent<WeaponReference>().pickupReference;
                            Instantiate(oldWeapon, player.transform.position + Random.insideUnitSphere, Quaternion.identity); //Instantiate gammelt våpen på bakken

                            inventory.AddItem(weapon);
                            Destroy(player.activeSlot.transform.GetChild(0).gameObject);
                            currentInterObj.gameObject.SetActive(false);
                            
                            weaponPickedUp = true;
                            currentInterObj = null;
                            


                        }
                    }
                    //Destroy(currentInterObjScript.gameObject);


                }
                
                
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
                    
                    door = currentInterObj.GetComponent<Door>();
                    door.Open();
                    
                    
                    
                    
                }
            }
            if (currentInterObjScript.hasInventory)
            {
                
                chest = currentInterObj.GetComponent<Chest>();
                chest.Open();
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
            if (currentInterObjScript.bombs)
            {
                inventory.bombs = inventory.bombs + currentInterObjScript.bombsAmount;
                Destroy(currentInterObjScript.gameObject);
                currentInterObj = null;
            }
            if (currentInterObjScript.key)
            {
                inventory.keys = inventory.keys + currentInterObjScript.keyAmount;
                Destroy(currentInterObjScript.gameObject);
                currentInterObj = null;
            }
            if (currentInterObjScript.money)
            {
                inventory.money = inventory.money + currentInterObjScript.moneyAmmount;
                Destroy(currentInterObjScript.gameObject);
                currentInterObj = null;
            }
            if (currentInterObjScript.restoreHealth)
            {
                playerAttributes.health = playerAttributes.health + currentInterObjScript.restoreHealthAmount;
                Destroy(currentInterObjScript.gameObject);
                currentInterObj = null;
            }
            if (currentInterObjScript.ammo)
            {
                inventory.ammo += 20;
                Destroy(currentInterObjScript.gameObject);
                ammoCollected = true;
                currentInterObj = null;
            }
        }
        else if (other.gameObject.tag == "Weapon")
        {

            if (player.activeSlot.transform.GetChild(0).tag == "Unarmed")
            {
                weapon = other.gameObject.GetComponent<WeaponReference>().gunReference;
                //weaponPickup = other.gameObject.GetComponent<WeaponReference>().pickupReference;
                
                
                inventory.AddItem(weapon);
                
                Destroy(player.activeSlot.transform.GetChild(0).gameObject);
                player.weapon = player.activeSlot.transform.GetChild(0).GetComponent<BaseWeapon>();

                Destroy(other.gameObject);



            }

            if (player.activeSlot.transform.GetChild(0).tag == "Weapon")
            {
                currentInterObj = other.gameObject;
                currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();
                weapon = other.gameObject.GetComponent<WeaponReference>().gunReference;
                //weaponPickup = other.gameObject.GetComponent<WeaponReference>().pickupReference;
                

            }


        }
        else
        {
            currentInterObj = null;
        }

        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            if (weaponPickedUp)
            {
                Destroy(other.gameObject);
                
            }
        }
        if (other.CompareTag("InteractableObject"))
        {

            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();
            if (ammoCollected)
            {
                Destroy(other.gameObject);
            }
        }




    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InteractableObject") || other.CompareTag("Weapon") || other.CompareTag("Door"))
        {
            if (other.gameObject == currentInterObj)
            {
                
                currentInterObj = null;
            }
        }
    }
}
