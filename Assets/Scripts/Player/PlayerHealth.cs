using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 10;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColor = new Color (1f, 0f, 0f, 0.1f);

	PlayerController playerController;
	bool isDamaged;
	bool isDead;

	void Start () {
		playerController = GetComponent<PlayerController> ();
		currentHealth = startingHealth;
	}

	void Update () {
		if(damaged) {
			damageImage.color = flashColor;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	// Uh oh!
	public void TakeDamage (int amount) {
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;
		if(currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death ();
        }
	}

	void Death () {
		isDead = true;
	}
}
