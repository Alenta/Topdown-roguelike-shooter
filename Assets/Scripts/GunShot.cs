using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour {
	public float speed;
	private Vector3 target;

	// Use this for initialization
	void Start () {
		target = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
           target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           target.z = transform.position.z;
       }
       transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
	}
}
