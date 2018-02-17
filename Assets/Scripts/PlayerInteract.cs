using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

    public GameObject currentInterObj = null;
    public InteractionObject currentInterObjScript = null;
    public Inventory inventory;

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && currentInterObj)
        {
            //check to see if this object is to be stored in inventory
            if (currentInterObjScript.inventory)
            {
                inventory.AddItem(currentInterObj);
            }

            //check to see if this object can be opened
            if(currentInterObjScript.openable)
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
        if(other.CompareTag("InteractableObject"))
        {
            Debug.Log(other.name);
            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();
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
