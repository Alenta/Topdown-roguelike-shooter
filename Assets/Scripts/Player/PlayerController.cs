using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
	private Rigidbody2D rb;
	public Vector2 thisVelocity;
    public Inventory inventory;
    private InventorySelect inventorySelect;
    private GameObject inventoryUI;
    public BaseWeapon weapon;
    public GameObject weaponSlot1;
    public GameObject weaponSlot2;
    public GameObject activeSlot;
    public GameObject bomb;
    private Canvas canvas;
    public bool hookFired;
    public bool hookTouching;
    private float throwForce;
    private bool rolling;
    private float rollTimer;
    public float hookSpeed = 1f;
    private PlayerAttributes playerStats;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerAttributes>();
        inventorySelect = GetComponentInChildren<InventorySelect>();
        
        inventoryUI = this.transform.GetChild(0).GetChild(0).gameObject;
        canvas = inventoryUI.GetComponent<Canvas>();
        inventory = this.transform.GetChild(0).GetComponent<Inventory>();
        activeSlot = weaponSlot1;
        weapon = activeSlot.transform.GetChild(0).GetComponent<BaseWeapon>(); 
        
    }
    private void Update()
    {
        if (Input.GetButtonDown("Inventory") && canvas.enabled == false)
        {
            //inventoryUI.SetActive(true);
            inventorySelect.EnableInventory();
            inventory.UpdateActiveSlots();
            canvas.enabled = true;
            Time.timeScale = 0;
        }
        else if (Input.GetButtonDown("Inventory") && canvas.enabled == true)
        {
            //inventoryUI.SetActive(false);
            inventorySelect.DisableInventory();
            canvas.enabled = false;
            Time.timeScale = 1;
        }
    }

    void FixedUpdate() {
        Movement();
        BetterMovement();
        Weapon();
        
        if (Input.GetButtonDown("Change Weapons")) {
            if (activeSlot == weaponSlot1)
            {
                activeSlot = weaponSlot2;
                weaponSlot1.SetActive(false);
                weaponSlot2.SetActive(true);
                weapon = activeSlot.transform.GetChild(0).GetComponent<BaseWeapon>();

            }    
            else
            {
                activeSlot = weaponSlot1;
                weaponSlot2.SetActive(false);
                weaponSlot1.SetActive(true);
                weapon = activeSlot.transform.GetChild(0).GetComponent<BaseWeapon>();
            }
        }
    }

    void Movement() {
        
        if (Input.GetButton("Bomb") && inventory.bombs > 0)
        {
            if (throwForce < 2)
            {
                throwForce += Time.deltaTime;
            }
            
            
        }
        if (Input.GetButtonUp("Bomb") && inventory.bombs > 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var direction = (mousePos - (Vector3)transform.position);
            GameObject bo = Instantiate(bomb, transform.position, Quaternion.identity);
            bo.GetComponent<Rigidbody2D>().AddForce(direction * throwForce, ForceMode2D.Impulse);
            inventory.bombs -= 1;
            throwForce = 0;
        }
        


    }
    

    void BetterMovement()
    {
        if (rolling)
        {
            rollTimer += Time.deltaTime;
        }
        
        if (rollTimer > 0.5f)
        {
            rollTimer = 0;
            rolling = false;

        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        


        if (!rolling && !hookFired)
        {
            rb.velocity = (movement * playerStats.moveSpeed);
            if (Input.GetKey("w"))
            {
                rb.velocity = (movement * playerStats.moveSpeed);

            }

            if (Input.GetKey("s"))
            {
                rb.velocity = (movement * playerStats.moveSpeed);
            }

            if (Input.GetKey("a"))
            {
                rb.velocity = (movement * playerStats.moveSpeed);
            }

            if (Input.GetKey("d"))
            {
                rb.velocity = (movement * playerStats.moveSpeed);
            }
        }

        if (Input.GetButtonDown("Roll"))
        {
            if (!rolling)
            {
                rb.velocity = rb.velocity * 2;
                rolling = true;
            }
        }
      

    }


    
    //Aim and fire weapon on mouse 0
    void Weapon() {
        if (Input.GetMouseButton(0))
        {
            if(activeSlot != null)
            {
                if (weapon != null)
                {
                    if (inventory.ammo > 0)
                    {
                        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        mousePos.z = 0;
                        
                        weapon.Fire(mousePos, playerStats.damage);


                    }
                }

                
            }
            else
            {
                
                if (weaponSlot1.activeSelf == true)
                {
                    activeSlot = weaponSlot1;
                    weapon = activeSlot.transform.GetChild(0).GetComponent<BaseWeapon>();
                }
                else
                {
                    activeSlot = weaponSlot2;
                    weapon = activeSlot.transform.GetChild(0).GetComponent<BaseWeapon>();
                }
            }
        }
        


        
    }
    void UpdateAttributes()
    {
        
    }
   
    
}
    

