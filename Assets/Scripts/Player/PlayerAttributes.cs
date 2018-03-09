using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour {
    
    public int health;
    public int damage;
    public int attackSpeed;
    public int moveSpeed;
    public int range;
    public int shotSpeed;
    public int luck;
    private int xp;
    private int level;
    private int xpNextLevel;

    public void StatChange(Attributes attributeMods)
    {
        health = health + attributeMods.health;
        damage = damage + attributeMods.damage;
        attackSpeed = attackSpeed + attributeMods.attackSpeed;
        moveSpeed = moveSpeed + attributeMods.moveSpeed;
        range = range + attributeMods.range;
        shotSpeed = shotSpeed + attributeMods.shotSpeed;
        luck = luck + attributeMods.luck;
        
    }
}
