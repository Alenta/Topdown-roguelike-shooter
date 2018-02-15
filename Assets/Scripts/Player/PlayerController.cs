using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 3F;
	private Rigidbody2D rb;

	public DashState dashState;
	public float cooldownDash = 1F;
    public float readyDash = 0f;
    public float dashingdash;
	public Vector2 thisVelocity;
    public InventoryTest inventory;
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
        Dash();
        Weapon();
        CooldownDash();
        if (Input.GetButtonDown("Jump")){
            if (activeSlot.transform.GetChild(0).tag == "Unarmed")
            {
                Destroy(activeSlot.transform.GetChild(0).gameObject);
                inventory.AddItem(thing);
                
            }
            else if (activeSlot.transform.GetChild(0).tag == "Weapon")
            {
                Destroy(activeSlot.transform.GetChild(0).gameObject);
                inventory.AddItem(thing);
                GameObject weapon = Instantiate(thing, thing.transform);
                

            }
            

        }

        if (dashState == DashState.Dashing)
        {
            Debug.Log("fixthis");
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


    void Dash()
    {
        if (Input.GetMouseButtonDown(1) && Input.GetKey("a") && (dashState == DashState.Ready))
        {
            Debug.Log("alert");
            //thisVelocity = rigidbody.velocity
            rb.velocity = new Vector2(rb.velocity.x * 15f, rb.velocity.y);
            dashState = DashState.Dashing;
            CooldownDash();

        }

        else if (Input.GetMouseButtonDown(1) && Input.GetKey("d") && (dashState == DashState.Ready))
        {
            Debug.Log("alert");
            //thisVelocity = rigidbody.velocity
            rb.velocity = new Vector2(rb.velocity.x * 15f, rb.velocity.y);
            dashState = DashState.Dashing;
            CooldownDash();

        }

        else if (Input.GetMouseButtonDown(1) && Input.GetKey("w") && (dashState == DashState.Ready))
        {
            Debug.Log("alert");
            //thisVelocity = rigidbody.velocity
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 15f);
            dashState = DashState.Dashing;
            CooldownDash();

        }

        else if (Input.GetMouseButtonDown(1) && Input.GetKey("s") && (dashState == DashState.Ready))
        {
            Debug.Log("alert");
            //thisVelocity = rigidbody.velocity
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 15f);
            dashState = DashState.Dashing;
            CooldownDash();
            print(new Vector2(1, 2) - new Vector2(3, 2));
    
        }
    }
    void CooldownDash () {
        if (dashState == DashState.Dashing)
        {
            dashState = DashState.Cooldown;
            cooldownDash = 1f;
        }
        if (dashState == DashState.Cooldown)
        {
            cooldownDash -= Time.deltaTime;
        }
        if (cooldownDash <= readyDash)
        {
            dashState = DashState.Ready;
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
    
	public enum DashState {
		Ready,
		Dashing,
		Cooldown
	}

