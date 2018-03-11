using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controls characters heath. Could be expanded to have things like regen effects
public class Health : MonoBehaviour {
    public int health;
    public int currentHealth;
    public bool isDead;
    public bool hasHealthBar;
    public RectTransform healthBar;
    public bool dropsItems;
    public AudioClip deathClip;
    public AudioClip damageTaken;
    private PlayerAttributes playerStats;
    private Attributes stats;
    private Inventory inventory;

    private void Start()
    {
        if(this.gameObject.tag == "Player")
        {
            playerStats = GetComponent<PlayerAttributes>();
            health = playerStats.health;
        }
        else
        {
            //stats = GetComponent<Attributes>();
            //health = stats.health;
        }
        if (dropsItems)
        {
            inventory = GetComponent<Inventory>();
        }
        

        currentHealth = health;
        if (gameObject.tag == "Player")
        {
            healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<RectTransform>();
        }
    }
   


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "PickupHealth")
        {
            //Destroy(col.gameObject);
            currentHealth = currentHealth + 5;
        }
    }


    public void TakeDamage(int damage) {

        currentHealth -= damage;

        if (currentHealth <= 0 && !isDead) {
            Death();
        }
        else if(damageTaken != null)
        {
            AudioSource.PlayClipAtPoint(damageTaken, transform.position);
        }
        if (hasHealthBar)
        {
            healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        }
        
    }

    void Death() {
        if(deathClip != null)
        {
            AudioSource.PlayClipAtPoint(deathClip, transform.position);
        }

        isDead = true;
        //FindObjectOfType<Exit>().CountEnemies();

        var animators = GetComponentsInChildren<Animator>();

        foreach(var animator in animators)
        {
            animator.SetBool("isDead", true);
            animator.transform.parent = null;
            MonoBehaviour animationController = animator.gameObject.GetComponent<PlayerAnimations>();
            if(animationController == null) animationController = animator.gameObject.GetComponent<SlurgfuckAnimations>();
            if (animationController != null) Destroy(animationController);
        }

        if(gameObject.tag == "Player")
        {
            //FindObjectOfType<GameOver>().EndGame();
        }
        if (dropsItems)
        {
            foreach (GameObject GO in inventory.inventory)
            {
                if(Random.Range(1,100) > 90)
                {
                    Instantiate(GO, transform.position + Random.insideUnitSphere, Quaternion.identity);
                }
                
            }
        }
        gameObject.SetActive(false);
    }
}
