using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlot : MonoBehaviour {

    public GameObject[] shopPool;

    public void Start()
    {
        GameObject item = Instantiate(shopPool[Random.Range(0, shopPool.Length - 1)],transform.position,Quaternion.identity);
        item.transform.parent = this.transform;
    }
}
