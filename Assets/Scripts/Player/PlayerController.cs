using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 3F;
	private Rigidbody2D rb;

	
	public float cooldownDash = 1F;
    public float readyDash = 0f;
    public float dashingdash;
	public Vector2 thisVelocity;
    public Inventory inventory;
    public GameObject thing;


    private BaseWeapon weapon;
    public GameObject weaponSlot1;
    public GameObject weaponSlot2;
    public GameObject activeSlot;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
        
        activeSlot = weaponSlot1;
        weapon = activeSlot.transform.GetChild(0).GetComponent<BaseWeapon>(); 
    }

    void Update() {
        //Movement();
        BetterMovement();
        
        Weapon();
        

        if (Input.GetButtonDown("Jump")){
            
            

        }

        
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
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                weapon.Fire(mousePos);
                
            }
        }


        
    }
    
}
    

