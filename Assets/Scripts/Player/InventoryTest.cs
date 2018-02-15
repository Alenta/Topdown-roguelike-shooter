using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour {
    private List<GameObject> items;
    private PlayerController player;
	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerController>();
        List<GameObject> items = new List<GameObject>();
    }
	
	// Update is called once per frame
	public void AddItem(GameObject item)
    {
        
        if(item.tag == "Weapon")
        {
            GameObject Weapon = Instantiate(item, player.activeSlot.transform);
            print("Making ONE gun");
        }
        else
        {
            items.Add(new GameObject(item.name));
        }
    }
    public void DropItem(GameObject item)
    {
        items.Remove(item);
    }
}
