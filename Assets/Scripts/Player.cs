using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (ShotController))]
public class Player : MonoBehaviour, IDamageable
{
    public bool trainingMode = false;
    public bool isAI = false;
    private PlayerController controller;
    private ShotController shotController;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private KeyCode[] shotKey = {KeyCode.Z, KeyCode.Slash};
    public int playerIndex;

    private HealthBarController healthBar;
    public float startingHealth = 10f;
	protected float health;
    public event System.Action OnDeath;
    public event System.Action OnHit;
    public Transform spawnArea;
    public Rigidbody rBody;

    void Start()
    {
        if(!trainingMode){
            if(!isAI) controller = gameObject.GetComponent<PlayerController>();
            
            healthBar = GameObject.Find(string.Format("Health Bar {0}", playerIndex)).GetComponent<HealthBarController>();
            healthBar.SetMaxHP(startingHealth);
        }
        shotController = gameObject.GetComponent<ShotController>();
        rBody = GetComponent<Rigidbody>();
        Respawn();
    }

    void Update()
    {
        if(!trainingMode){
            if (!isAI && Input.GetKey(shotKey[playerIndex-1])) {
			    shotController.Shoot();
            }
            
            //Health Bar Update
            healthBar.SetHP(health);
		}
    }

    public void TakeHit(float damage, RaycastHit hit) {
		if(OnHit != null) {
            OnHit();
        }
        health -= damage;
		if (health <= 0) {
			Die();
		}
	}

    protected void Die()
    {
        if(OnDeath != null){
            OnDeath();
        }
        Respawn();
    }

    public void Respawn()
    {
        health = startingHealth;
        transform.position = spawnArea.position;
    }

    void OnTriggerEnter (Collider collidedObject)
    {
    Debug.Log("Collision?");
    switch (collidedObject.tag) 
        {
        case "DeathTrigger":
            Debug.Log("Death Trigger");
            Die();
            break;

        case "Car":
            Debug.Log("Car");
            Die();
            break;
        }
    }
}


