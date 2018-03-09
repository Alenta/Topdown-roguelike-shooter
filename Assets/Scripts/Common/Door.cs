using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    private SpriteRenderer sprite;
    public Sprite open;
    public Sprite closed;
    public bool isOpened;
    public BoxCollider2D col;
	// Use this for initialization
	void Awake () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	
    public void Open()
    {
        if (!isOpened)
        {
            if (sprite.sprite == open)
            {
                sprite.sprite = closed;
            }
            else
            {
                sprite.sprite = open;
            }
            isOpened = true;
            col.enabled = false;
        }
        else
        {
            if (sprite.sprite == open)
            {
                sprite.sprite = closed;
            }
            else
            {
                sprite.sprite = open;
            }
            isOpened = false;
            col.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if(collision.transform.position.x != this.transform.position.x) //Wall is to the right or left of door
            {
                
            }
            else
            {
                sprite.sprite = open;
                
            }

        }
    }
}
