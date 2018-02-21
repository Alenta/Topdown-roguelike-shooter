using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 3F;
	private Rigidbody2D rb;
	public Vector2 thisVelocity;
    public Inventory inventory;
    private GameObject inventoryUI;
    private BaseWeapon weapon;
    public GameObject weaponSlot1;
    public GameObject weaponSlot2;
    public GameObject activeSlot;
    public GameObject bomb;
    private float throwForce;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
        inventoryUI = this.transform.GetChild(0).GetChild(0).gameObject;
        inventory = this.transform.GetChild(0).GetComponent<Inventory>();
        activeSlot = weaponSlot1;
        weapon = activeSlot.transform.GetChild(0).GetComponent<BaseWeapon>(); 
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
        if (Input.GetButtonDown("Inventory") && inventoryUI.activeSelf == false) 
        {
            inventoryUI.SetActive(true);
        }
        else if (Input.GetButtonDown("Inventory") && inventoryUI.activeSelf == true)
        {
            inventoryUI.SetActive(false);
        }
        if (Input.GetButton("Bomb") && inventory.bombs > -1)
        {
            if (throwForce < 2)
            {
                throwForce += Time.deltaTime;
            }
            
            
        }
        if (Input.GetButtonUp("Bomb") && inventory.bombs > -1)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var direction = (mousePos - (Vector3)transform.position);
            GameObject bo = Instantiate(bomb, transform.position, Quaternion.identity);
            bo.GetComponent<Rigidbody2D>().AddForce(direction * throwForce, ForceMode2D.Impulse);
            throwForce = 0;
        }


    }
    

    void BetterMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        rb.velocity = (movement * speed);


        if (Input.GetKey("w"))
        {
            rb.velocity = (movement * speed);
        }

        if (Input.GetKey("s")) { 
            rb.velocity = (movement * speed);
        }

        if (Input.GetKey("a")) { 
            rb.velocity = (movement * speed);
        }

        if (Input.GetKey("d")) {
            rb.velocity = (movement * speed);
        }
        



    }


    
    //Aim and fire weapon on mouse 0
    void Weapon() { 
        if(activeSlot.GetComponentInChildren<BaseWeapon>() != null)
        {
            if (Input.GetMouseButton(0) && inventory.ammo > 0)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                weapon.Fire(mousePos);
                
            }
        }


        
    }
    
}
    

