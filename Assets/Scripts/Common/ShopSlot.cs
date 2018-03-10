using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlot : MonoBehaviour {
    public InteractionObject itemSlot;
    public GameObject[] shopPool;
    private BoxCollider2D itemCollider;

    public void Start()
    {
        int x = Random.Range(0, shopPool.Length - 1);
        itemSlot = GetComponent<InteractionObject>();
        var itemCopy = shopPool[x];
        var cloneItem = Instantiate(itemCopy,transform.position,Quaternion.identity);
        itemSlot.itemContained = shopPool[Random.Range(0, shopPool.Length-1)];
        itemCollider = shopPool[x].GetComponent<BoxCollider2D>();
        print(itemCollider.name);
        //itemCollider.enabled = false;
        cloneItem.transform.parent = this.transform;
    }
}
