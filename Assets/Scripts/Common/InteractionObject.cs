using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour {

    public bool inventory;      //if true then this object can be stored in inventory
    public bool openable;       //if this object can be opened
    public bool locked;         //if this is true then object is locked
    public bool equippable;     //If this item is equippable
    public GameObject ItemNeeded;       //item needed in order to interact with this item


	void DoInteraction()
    {
        //''picked up''
        gameObject.SetActive(false);
    }
}
