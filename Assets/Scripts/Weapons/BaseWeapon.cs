using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour {

    public GameObject projectile;
    public GameObject bulletsOut;
    public GameObject reloadingIndicator;
    
    public int projectileCountMax = 1;
    public int projectileCountMin = 1;
    public float reloadTime = 1;
    public float shootInterval = 0.2f;
    public float clipSize = 10;
    private Inventory playerInv;
    internal float reloadProgress = 1000;
    private float shootIntervalProgress = 1000;
    internal float ammoInClip = 10;
    public float totalAmmo;
    private AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip reloadSound;

    // Use this for initialization
    void Start () {
        ammoInClip = clipSize;
        shootIntervalProgress = shootInterval + 1;
        reloadProgress = reloadTime + 1;
        playerInv = this.gameObject.transform.parent.parent.GetComponentInChildren<Inventory>();

        audioSource = gameObject.GetComponent<AudioSource>();

        
    }
	
	// Update is called once per frame
	void Update ()
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
    
    
    public virtual void Fire(Vector2 target)
    {
        if (ammoInClip <= 0 || reloadProgress < reloadTime || shootIntervalProgress < shootInterval)
        {
            // reloading or waiting
            return;
        }

        ammoInClip -= 1;
        playerInv.ammo -= 1;
        audioSource.PlayOneShot(fireSound);

        int projectileCount = Random.Range(projectileCountMin, projectileCountMax);
        for(int i = 0; i < projectileCount; i++)
        {
            var projectileGO = Instantiate(projectile, bulletsOut.transform.position, new Quaternion(), null);
            projectileGO.SetActive(true);
            var newProjectile = projectileGO.GetComponent<BaseProjectile>();
            newProjectile.Fire(target, transform.parent.parent.GetComponent<PlayerController>() != null);
        }

        shootIntervalProgress = 0;
        if (ammoInClip <= 0) StartReload();
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
