using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Barrel : MonoBehaviour {

	private Transform myTransform;

	public int health = 25;

    public float damage = 35f;

	public GameObject barrelExplosion;

	public GameObject explosionSound;

    List<GameObject> objectsInRange;

	// Use this for initialization
	void Start () {
		myTransform = this.transform;
        objectsInRange = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
		checkHealth ();
	}

	void checkHealth(){
		if (health <= 0) {
			Instantiate (barrelExplosion, myTransform.position, myTransform.rotation);
			Instantiate (explosionSound, myTransform.position, myTransform.rotation);
            foreach (GameObject objectInRange in objectsInRange) {
                if (objectInRange == null) {
                    break;
                }
                objectInRange.transform.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
            Destroy (this.gameObject);
		}
	}

	void TakeDamage(int dmg){
		health -= dmg;
	}

    void OnTriggerEnter(Collider collidingObject) {
        if (health > 0) {
            objectsInRange.Add(collidingObject.gameObject);
        }
    }

    void OnTriggerExit(Collider collidingObject) {
        if (health > 0) {
            objectsInRange.Remove(collidingObject.gameObject);
        }
    }
}
