using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpriteUpdater : MonoBehaviour {
    public Sprite[] wallSprites;
    private SpriteRenderer sprite;
    private float xPos;
    private float yPos;
    private float xPosN;
    private float yPosN;
    private bool right;
    private bool left;
    private bool up;
    private bool down;
    private bool luDia;
    private bool ruDia;
    private bool lbDia;
    private bool rbDia;
    private bool dia;
    private bool spritesSet = false;
    private Rigidbody2D rb;
    //private WallSpriteUpdater updater;
    private CircleCollider2D col;
    
	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        xPos = this.transform.position.x;
        yPos = this.transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;
        //updater = GetComponent<WallSpriteUpdater>();
        col = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (spritesSet)
        {
            //updater.enabled = false;
            col.enabled = false;
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (collision.gameObject.tag == "Wall")
        {
            yPosN = collision.gameObject.transform.position.y;
            xPosN = collision.gameObject.transform.position.x;
            if (xPos < xPosN && yPos == yPosN) //right
            {
                right = true;
            }
            if (xPos > xPosN && yPos == yPosN) //left
            {
                left = true;
            }
            if (yPos < yPosN && xPos == xPosN) //Up
            {
                up = true;
            }
            if (yPos > yPosN && xPos == xPosN) // down
            {
                down = true;
            }
            if (xPos < xPosN && yPos < yPosN) //ruDia
            {
                ruDia = true;
            }
            if (xPos > xPosN && yPos < yPosN) //luDia
            {
                luDia = true;
            }
            if (xPos < xPosN && yPos > yPosN) //rbDia
            {
                rbDia = true;
            }
            if (xPos > xPosN && yPos > yPosN) //lbDia
            {
                lbDia = true;
            }
            if (ruDia || luDia || rbDia || lbDia)
            {
                dia = true;
                
            }

            if (right && !left && !up && !down)
            {
                sprite.sprite = wallSprites[1];
            }
            if (!right && left && !up && !down)
            {
                sprite.sprite = wallSprites[2];
            }
            if (!right && !left && up && !down)
            {
                sprite.sprite = wallSprites[3];
            }
            if (!right && !left && !up && down)
            {
                sprite.sprite = wallSprites[4];
            }
            if (!right && left && !up && down)
            {
                sprite.sprite = wallSprites[5];
            }
            if (right && !left && !up && down)
            {
                sprite.sprite = wallSprites[6];
            }
            if (!right && left && up && !down)
            {
                sprite.sprite = wallSprites[7];
            }

            if (right && !left && up && !down)
            {
                sprite.sprite = wallSprites[8];
            }
            if (!right && !left && up && down)
            {
                sprite.sprite = wallSprites[9];
            }
            if (right && left && !up && !down)
            {
                sprite.sprite = wallSprites[10];
            }
            if (right && left && up && !down && !dia)
            {
                sprite.sprite = wallSprites[11];
            }
            if (right && left && !up && down && !dia)
            {
                sprite.sprite = wallSprites[12];
            }
            if (!right && left && up && down && !dia)
            {
                sprite.sprite = wallSprites[13];
            }
            if (right && !left && up && down && !dia)
            {
                sprite.sprite = wallSprites[14];
            }
            if (right && left && up && down && !dia)
            {
                sprite.sprite = wallSprites[15];
            }
            if (!right && left && !up && down && lbDia && !ruDia && !luDia && !rbDia)
            {
                sprite.sprite = wallSprites[16];
            }
            if (right && !left && !up && down && rbDia && !lbDia && !ruDia && !luDia)
            {
                sprite.sprite = wallSprites[17];
            }
            if (right && !left && up && down && ruDia && rbDia && !lbDia && luDia)
            {
                sprite.sprite = wallSprites[18];
            }
            if (!right && left && up && down && luDia && lbDia && !rbDia && !ruDia)
            {
                sprite.sprite = wallSprites[19];
            }
            if (right && !left && up && !down && ruDia && !lbDia && !rbDia && !luDia)
            {
                sprite.sprite = wallSprites[20];
            }
            if (!right && left && up && !down && luDia && !ruDia && !lbDia && !rbDia)
            {
                sprite.sprite = wallSprites[21];
            }
            if (!right && !left && up && down)
            {
                sprite.sprite = wallSprites[22];
            }
            if (right && left && !up && down)
            {
                sprite.sprite = wallSprites[23];
            }
            if (right && left && up && down && ruDia && luDia && rbDia && lbDia)
            {
                sprite.sprite = wallSprites[24];
            }
            if (!right && !left && !up && !down)
            {
                sprite.sprite = wallSprites[0];
            }
            spritesSet = true;
        }
        




    }
}
