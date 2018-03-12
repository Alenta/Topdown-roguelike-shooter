using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelect : MonoBehaviour {
    private GameObject activeInventory;
    private Image activeImage;
    private Canvas canvas;
    public Transform[] inventorySlotsUI;
    public Transform[] equipmentSlotsUI;
    public bool inventoryEnabled;
    private int counter;
    private bool equipmentActive = true;
    private GameObject infoBox;
    private Image infoImage;
    private Text infoText;
    private InventorySlot inventorySlot;
    

	// Use this for initialization
	void Start () {
        activeInventory = GameObject.FindGameObjectWithTag("ActiveInventory");
        inventorySlot = GetComponent<InventorySlot>();
        activeImage = activeInventory.GetComponent<Image>();
        inventorySlotsUI = new Transform[8];
        infoBox = transform.GetChild(0).GetChild(0).GetChild(6).gameObject;
        infoText = infoBox.GetComponentInChildren<Text>();
        counter = 0;
        for (int x = 0; x < 8; x++)
        {
            inventorySlotsUI[x] = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(x).GetComponent<Transform>();
        }
        equipmentSlotsUI = new Transform[3];
        for (int y = 0; y < 3; y++)
        {
            equipmentSlotsUI[y] = transform.GetChild(0).GetChild(0).GetChild(y+2).GetComponent<Transform>();
        }
        activeInventory.SetActive(true);
        
        

    }
	
	// Update is called once per frame
	void Update () {
        if (inventoryEnabled)
        {
            
            if (equipmentActive)
            {
                //print(equipmentSlotsUI[2].GetComponent<InventorySlot>().itemSlot.name);
                if (equipmentSlotsUI[counter].GetComponent<InventorySlot>().itemSlot.name != null)
                {
                    infoText.text = equipmentSlotsUI[counter].GetComponent<InventorySlot>().itemSlot.name + " " + equipmentSlotsUI[counter].GetComponent<InventorySlot>().itemSlot.GetComponent<ItemInfo>().price;

                }
                else
                {
                    infoText.text = "Empty";
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (activeInventory.activeSelf == false)
                    {
                        activeInventory.SetActive(true);
                    }
                    if (counter < 2)
                    {
                        counter += 1;
                    }
                    else
                    {
                        counter = 0;
                    }

                    activeInventory.transform.position = equipmentSlotsUI[counter].transform.position;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (activeInventory.activeSelf == false)
                    {
                        activeInventory.SetActive(true);
                    }
                    if (counter > 0)
                    {
                        counter -= 1;
                    }
                    else
                    {
                        counter = 2;
                    }

                    activeInventory.transform.position = equipmentSlotsUI[counter].transform.position;
                }
            }
            else
            {
                if (inventorySlotsUI[counter].GetComponent<InventorySlot>().itemSlot != null)
                {
                    infoText.text = inventorySlotsUI[counter].GetComponent<InventorySlot>().itemSlot.name;
                }
                else
                {
                    infoText.text = "Empty";
                }

                //infoText.text = inventorySlotsUI[counter].GetComponent<InventorySlot>().itemSlot.name + " " + inventorySlotsUI[counter].GetComponent<InventorySlot>().itemSlot.GetComponent<ItemInfo>().price;

                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (activeInventory.activeSelf == false)
                    {
                        activeInventory.SetActive(true);
                    }
                    if (counter < 7)
                    {
                        counter += 1;
                    }
                    else
                    {
                        counter = 0;
                    }

                    activeInventory.transform.position = inventorySlotsUI[counter].transform.position;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (activeInventory.activeSelf == false)
                    {
                        activeInventory.SetActive(true);
                    }
                    if (counter > 0)
                    {
                        counter -= 1;
                    }
                    else
                    {
                        counter = 7;
                    }

                    activeInventory.transform.position = inventorySlotsUI[counter].transform.position;
                }
            }
        
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
        {
            if (equipmentActive)
            {
                equipmentActive = false;
                activeInventory.transform.position = inventorySlotsUI[0].transform.position;
                counter = 0;
            }
            else
            {
                equipmentActive = true;
                activeInventory.transform.position = equipmentSlotsUI[0].transform.position;
                counter = 0;
            }
        }

    }
    public void EnableInventory()
    {
        inventoryEnabled = true;
    }
    public void DisableInventory()
    {
        activeInventory.SetActive(false);
        inventoryEnabled = false;
    }
}
