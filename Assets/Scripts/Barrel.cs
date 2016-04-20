﻿using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {

	private Transform myTransform;

	public int health = 25;

	public GameObject barrelExplosion;

	public GameObject explosionSound;

	// Use this for initialization
	void Start () {
		myTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		checkHealth ();
	}

	void checkHealth(){
		if (health <= 0) {
			Instantiate (barrelExplosion, myTransform.position, myTransform.rotation);
			Instantiate (explosionSound, myTransform.position, myTransform.rotation);
			Destroy (this.gameObject);
		}
	}

	void TakeDamage(int dmg){
		health -= dmg;
	}
}
