using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float maxHP = 100f;
    public float currentHP;
    public enum Status { walk, taunt, attack, hit, die };
    private Status currentStatus;
    private GameObject player;
    public AudioClip deathSound;
    private Transform startTransform;

    // Use this for initialization
    void Start () {
        currentHP = maxHP;
        currentStatus = Status.walk;
        player = GameObject.FindGameObjectWithTag("Player");
        startTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
	    if (currentStatus == Status.die) {
            player.transform.SendMessage("GetKill", SendMessageOptions.DontRequireReceiver);
            AudioSource.PlayClipAtPoint(deathSound, player.transform.position);
            GameObject clone = Instantiate(Resources.Load(this.name, typeof(GameObject))) as GameObject;
            clone.transform.position = startTransform.position;
            clone.transform.rotation = startTransform.rotation;
            clone.GetComponent<EnemyAI>().patrolPoints = this.GetComponent<EnemyAI>().patrolPoints;
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
    }
}
