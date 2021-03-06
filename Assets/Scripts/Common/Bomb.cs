﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;
    public float fuse = 1f;
    
    private float timer;
    private float timer2;
    private bool exploded;
    private GameObject exp;
    private SpriteRenderer sprite;
    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        if (timer > fuse)
        {
            timer2 += Time.deltaTime;
            if (!exploded)
            {
                GameObject explo = Instantiate(explosion, transform.position, Quaternion.identity);
                exp = explo;
                exploded = true;
            }
            sprite.enabled = false;
            if (timer2 > 0.2f)
            {
                Destroy(exp.gameObject);
                Destroy(this.gameObject);
            }

        }

    }
    
}
