﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float maxHP = 100f;
    public float currentHP;
    public enum Status { walk, taunt, attack, hit, die };
    private Status currentStatus;
    private GameObject player;
    public AudioClip deathSound;

    // Use this for initialization
    void Start () {
        currentHP = maxHP;
        currentStatus = Status.walk;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	    if (currentStatus == Status.die) {
            player.transform.SendMessage("GetKill", SendMessageOptions.DontRequireReceiver);
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage) {
        currentHP -= damage;
        if (currentHP <= 0) {
            currentStatus = Status.die;
        }
        else {
            currentStatus = Status.hit;
        }
        //Play animation
        //GetComponent<Animation>().Play(currentStatus.ToString());
    }
}
