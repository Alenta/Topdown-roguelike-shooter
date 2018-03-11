using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private ContactPoint2D[] contactPoints;
    public bool bullet;
    public bool rocket;
    public bool pellet;
    public bool particle;
    public bool energy;
    public bool grenade;

    public BaseProjectile projectile;

    private void Awake()
    {
        projectile = GetComponent<BaseProjectile>();
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
            if (health != null)
            {
                health.TakeDamage(projectile.totalDamage);

            }
        }

        








        if (projectile.boomerangShot)                                        //Boomerang shot here...
        {
            //var newTarget = newtarget
            //projectile.Fire(newTarget, true, projectile.totalDamage, 1, false, projectile.piercingShot, false);
            
            
        }







    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (projectile.playerOwned)
        {
            // player owned ignore collisions with players

            if (collision.gameObject.GetComponent<PlayerController>() != null) return;

            if (collision.transform.parent != null &&
                collision.transform.parent.GetComponent<PlayerController>() != null) return;
        }
        
        else
        {
            // enemy owned, ingore all other than player
            if (collision.gameObject.GetComponent<EnemyMovement>() != null) return;
            if (collision.transform.parent != null &&
                collision.transform.parent.GetComponent<EnemyMovement>() != null) return;
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "BreakableObject")
        {
            Health health = collision.gameObject.GetComponent<Health>();

            
            // dmg health objects
            if (health != null)
            {
                health.TakeDamage(projectile.totalDamage);
                print("damage");
                if (!projectile.bouncing || !projectile.boomerangShot || !projectile.piercingShot)
                {
                    Destroy(projectile.gameObject);
                }
            }
        }
        if (!projectile.bouncing && !projectile.boomerangShot && collision.gameObject.tag == "Wall")
        {
            Destroy(projectile.gameObject);

        }
    }


}