using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {
    public int multiShot;
    public int angleShot;
    public int splitShot;
    public int damageMultiplier;
    public int damageBoost;
    public int damageResist;
    public int speedBoost;
    public int healthRegen;
    public int explosionRadius;
    public int shotSize;
    public bool piercingShot;
    
    public bool shotRotate;
    public bool burstFire;
    public bool bouncingShot;
    
    public bool boomerangShot;
    public bool zigZagShot;
    public bool spiralShot;
    public bool invulnerability;

    public void AbilityChange(Abilities abilityMods)
    {
        multiShot = multiShot + abilityMods.multiShot;
        angleShot = angleShot + abilityMods.angleShot;
        splitShot = splitShot + abilityMods.splitShot;
        damageMultiplier = damageMultiplier + abilityMods.damageMultiplier;
        damageBoost = damageBoost + abilityMods.damageBoost;
        damageResist = damageResist + abilityMods.damageResist;
        speedBoost = speedBoost + abilityMods.speedBoost;
        healthRegen = healthRegen + abilityMods.healthRegen;
        explosionRadius = explosionRadius + abilityMods.explosionRadius;
        shotSize = shotSize + abilityMods.shotSize;
        if(piercingShot || abilityMods.piercingShot)
        {
            piercingShot = true;
        }
        if(shotRotate || abilityMods.shotRotate)
        {
            shotRotate = true;
        }
        if(burstFire || abilityMods.burstFire)
        {
            burstFire = true;
        }
        if(bouncingShot || abilityMods.bouncingShot)
        {
            bouncingShot = true;
        }
        if(boomerangShot || abilityMods.boomerangShot)
        {
            boomerangShot = true;
        }
        if(zigZagShot || abilityMods.zigZagShot)
        {
            zigZagShot = true;
        }
        if(spiralShot || abilityMods.spiralShot)
        {
            spiralShot = true;
        }
        if(invulnerability || abilityMods.invulnerability)
        {
            invulnerability = true;
        }

    }

}
