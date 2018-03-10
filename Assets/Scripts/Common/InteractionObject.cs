using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour {

    public bool inventory;      //if true then this object can be stored in inventory
    public bool openable;       //if this object can be opened
    public bool locked;         //if this is true then object is locked
    public bool equippable;     //If this item is equippable
    public bool hasInventory;
    public bool shop;
    public GameObject itemContained;
    public bool hasAttributes;
    public bool hasAbilities;
    public bool ammo;
    public bool bombs;
    public bool key;
    public bool money;
    public bool restoreHealth; 
    public int ammoAmount;
    public int bombsAmount;
    public int keyAmount;
    public int moneyAmmount;
    public int restoreHealthAmount;
    public int price;
    private PlayerAbilities abilities;
    private Attributes stats;
    public GameObject ItemNeeded;       //item needed in order to interact with this item

    private void Start()
    {
        if (hasAttributes)
        {
            stats = GetComponent<Attributes>();
        }
        if (hasAbilities)
        {
            abilities = GetComponent<PlayerAbilities>();
        }
        if (shop)
        {
            //itemContained = transform.GetChild(0).gameObject;
            price = Random.Range(0, 100);
        }
    }

    void DoInteraction()
    {
        //''picked up''
        gameObject.SetActive(false);
    }
    void Open()
    {

    }
}
