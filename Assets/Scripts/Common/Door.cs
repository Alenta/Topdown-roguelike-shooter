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
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Open()
    {
        if (!isOpened)
        {
            sprite.sprite = open;
            isOpened = true;
            col.enabled = false;
        }
        else
        {
            sprite.sprite = closed;
            isOpened = false;
            col.enabled = true;
        }
    }
}
