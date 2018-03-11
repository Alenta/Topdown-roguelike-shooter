using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private ContactPoint2D[] contactPoints;

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


            // dmg health objects
            if (health != null)
            {
                health.TakeDamage(projectile.totalDamage);

            }
        }

        if (!projectile.piercingShot && !projectile.boomerangShot && !projectile.bouncing) //On impact modifiers
        {

            Destroy(this.gameObject);
            print("Destroy");



        }








        if (projectile.boomerangShot)                                        //Boomerang shot here...
        {
            //var newTarget = newtarget
            //projectile.Fire(newTarget, true, projectile.totalDamage, 1, false, projectile.piercingShot, false);
            
            
        }







    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!projectile.bouncing && !projectile.boomerangShot && collision.gameObject.tag != "Player")
        {
            Destroy(projectile.gameObject);
        }
    }


}