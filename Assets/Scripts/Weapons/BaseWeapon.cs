using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour {
    
    public GameObject projectile;
    public GameObject bulletsOut;
    public GameObject reloadingIndicator;
    private PlayerAbilities playerAbilities;
    private PlayerAttributes playerAttributes;
    public int baseDamage;
    private int totalDamage;
    public int projectileCountMax = 1;
    public int projectileCountMin = 1;
    public float reloadTime = 1;
    public float shootInterval = 0.2f;
    public float clipSize = 10;
    private float totalShootInterval;
    private Inventory playerInv;
    internal float reloadProgress = 1000;
    private float shootIntervalProgress = 1000;
    internal float ammoInClip = 10;
    public float totalAmmo;
    private Vector3 bulletsOutSplit;
    private Quaternion rotate;
    private AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public Quaternion offset;

    // Use this for initialization
    void Start () {
        ammoInClip = clipSize;
        shootIntervalProgress = shootInterval + 1;
        reloadProgress = reloadTime + 1;
        playerInv = this.gameObject.transform.parent.parent.GetComponentInChildren<Inventory>();
        playerAbilities = GetComponentInParent<PlayerAbilities>();
        playerAttributes = GetComponentInParent<PlayerAttributes>();
        audioSource = gameObject.GetComponent<AudioSource>();
        
        
    }
	

	// Update is called once per frame
	void FixedUpdate ()
    {

        totalAmmo = playerInv.ammo;
        if (reloadProgress <= reloadTime)
        {
            PerformReload();
        }
        else if (Input.GetButtonDown("Reload") && ammoInClip < clipSize)
        {
            StartReload();
        }

        // Shooting cooldown
        if (shootIntervalProgress <= shootInterval) shootIntervalProgress += Time.deltaTime;
    }
    
    
    public virtual void Fire(Vector2 target, int damage)
    {
        totalShootInterval = shootInterval / (playerAttributes.attackSpeed / 10); //player attackspeed divides interval by one tenth
        if (ammoInClip <= 0 || reloadProgress < reloadTime || shootIntervalProgress < totalShootInterval)
        {
            // reloading or waiting
            return;
        }
        
        totalDamage = (damage / 10) * baseDamage;
        ammoInClip -= 1;
        playerInv.ammo -= 1;
        audioSource.PlayOneShot(fireSound);
        if(playerAbilities.angleShot >= 2)
        {
            
            //StartCoroutine(FireBullets(target, totalDamage));
        }
        
        StartCoroutine(FireBullets(target, totalDamage));
        
        shootIntervalProgress = 0;
        if (ammoInClip <= 0) StartReload();
    }
    IEnumerator FireBullets(Vector2 target, int damage)
    {
        int projectileCount = playerAbilities.multiShot;
        for (int i = 0; i < projectileCount; i++)
        {
            var projectileGO = Instantiate(projectile, bulletsOut.transform.position, new Quaternion() * offset, null);
            projectileGO.SetActive(true);

            var newProjectile = projectileGO.GetComponent<BaseProjectile>();
            newProjectile.Fire(target, transform.parent.parent.GetComponent<PlayerController>() != null, totalDamage, playerAbilities.splitShot, playerAbilities.boomerangShot,playerAbilities.piercingShot);
            yield return new WaitForSeconds(0.01f);

        }
    }

    public virtual void StartReload()
    {
        audioSource.PlayOneShot(reloadSound);

        reloadProgress = 0;
        if (reloadingIndicator != null) reloadingIndicator.SetActive(true);
    }

    private void PerformReload()
    {
        reloadProgress += Time.deltaTime;

        // Complete
        if (reloadProgress > reloadTime)
        {
            ammoInClip = clipSize;
            if (reloadingIndicator != null) reloadingIndicator.SetActive(false);
        }
    }

}
