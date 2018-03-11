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

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        projectile = GetComponent<BaseProjectile>();

        hookComplete = false;
    }

    void FixedUpdate()
    {
        if (hookComplete && hitWall)
        {
            hitWall = false;
            hookComplete = false;
            Destroy(this.gameObject);

        }
        if (!hitWall)
        {
            hookComplete = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (projectile.playerOwned)
        {
            // player owned ignore collisions with players

            if (collider.GetComponent<PlayerController>() != null) return;

            if (collider.transform.parent != null &&
                collider.transform.parent.GetComponent<PlayerController>() != null) return;
        }
        else
        {
            // enemy owned, ingore all other than player
            if (collider.GetComponent<EnemyMovement>() != null) return;
            if (collider.transform.parent != null &&
                collider.transform.parent.GetComponent<EnemyMovement>() != null) return;
        }

        if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Player" || collider.gameObject.tag == "BreakableObject")
        {
            Health health = collider.GetComponent<Health>();

            print(health);
            // dmg health objects
            if (collider.gameObject.tag == "BreakableObject")
            {
                var x = health.currentHealth;
                health.TakeDamage(x);
                
            }
            if (health != null && collider.gameObject.tag != "BreakableObject")
            {
                health.TakeDamage(projectile.totalDamage);

            }
        }

        








        if (projectile.boomerangShot)                                        //Boomerang shot here...
        {
            //var newTarget = newtarget
            //projectile.Fire(newTarget, true, projectile.totalDamage, 1, false, projectile.piercingShot, false);


        }


        if (collider.gameObject.tag == "Wall")
        {
            projectile.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            hitWall = true;

        }




    }
   
}