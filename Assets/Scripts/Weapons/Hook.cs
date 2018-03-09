using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private Rigidbody2D rb;
    private BaseProjectile projectile;
    
    private RotateToMouse rot;
    public bool hitWall;
    public bool hookComplete;
    // Use this for initialization

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        projectile = GetComponent<BaseProjectile>();
        
        hookComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hookComplete && hitWall)
        {
            hitWall = false;
            Destroy(this.gameObject);
            hookComplete = false;
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            projectile.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            hitWall = true;

        }
        
    }
}