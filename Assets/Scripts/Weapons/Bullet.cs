using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    

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
        if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Player")
        {
            Health health = collider.GetComponent<Health>();


            // dmg health objects
            if (health != null)
            {
                health.TakeDamage(projectile.totalDamage);

            }
        }




        if (!projectile.piercingShot) //On impact modifiers
        {
            if (!projectile.boomerangShot)
            {
                Destroy(this.gameObject);
            }
            else
            {

            }

        }



        if (collider.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);

        }

        else if (projectile.boomerangShot)                                        //Bouncing shot here...
        {
            var newTarget = GameObject.Find("Player").transform.position;
            
            //Change this to actual bouncing, not following the player
        }







    }


}