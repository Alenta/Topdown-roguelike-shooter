using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
    private GameObject inventoryUI;
    private bool playerFound;

    // Use this for initialization
    void Start () {
        inventoryUI = this.transform.GetChild(0).GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Open()
    {
        if (inventoryUI.activeSelf == false)
        {
            inventoryUI.SetActive(true);
        }
        else if (inventoryUI.activeSelf == true)
        {
            inventoryUI.SetActive(false);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            inventoryUI.SetActive(false);
        }
    }
}
