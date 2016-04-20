using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    //Player status
    public enum Status { idle, walk, run, charge, attack, idlebattle, die, victory };
    public Status currentStatus;

    public GameObject focusPoint;
	public int currentAmmo = 30;

    public float maxHP = 5f;
    private float currentHP;

    //Object that represents HP
    public Slider HP;

    //Object that represents an image which appears when the player loses health
    public Color damageColor;
    public Image damageImage;
    public float damageFade = 5f;

    //Player score
    private int kills = 0;
    public Text scoreText;

    //Death objects
    public Text respawnText;
    public float respawnTime = 5f;
    private GameObject respawn;

    //Sounds
    public AudioClip winSound;
    public AudioClip deathSound;
    public AudioClip hitSound;

    // Use this for initialization
    void Start () {
        respawn = GameObject.FindGameObjectWithTag("Respawn");
        respawn.SetActive(false);
        currentHP = maxHP;
        currentStatus = Status.walk;
        scoreText.text = "0";
        damageColor.a = 0;        
    }
	
	// Update is called once per frame
	void Update () {

        //Fade damageImage
        if (damageImage.color.a > 0f) {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, damageFade * Time.deltaTime);
        }

        if (currentStatus != Status.die) {
            if (currentHP <= 0) {
                lose();
            }            
        }
        else {
            if (respawnTime <= 0f) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else {
                respawnText.text = "" + Mathf.Ceil(respawnTime);
                respawnTime -= Time.deltaTime;
            }
        }        
    }

    //Called by external game objects
    void TakeDamage(float damage) {
        if (currentStatus != Status.die) {
            currentHP -= damage;
            HP.value = currentHP / maxHP;
            damageImage.color = new Vector4(damageColor.r, damageColor.g, damageColor.b, 1f);
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }
    }

    //Called by external game objects
    public void GetKill() {
        scoreText.text = "" + ++kills;
    }

    public void lose() {
        currentStatus = Status.die;
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag != "MainCamera") {
                Destroy(child);
            }
        }
        this.GetComponent<CharacterController>().enabled = false;
        respawn.SetActive(true);
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
    }
}
