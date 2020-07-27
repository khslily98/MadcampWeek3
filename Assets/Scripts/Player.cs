using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (ShotController))]
public class Player : MonoBehaviour, IDamageable
{
    private PlayerController controller;
    private ShotController shotController;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private KeyCode[] shotKey = {KeyCode.Z, KeyCode.Slash};
    public int playerIndex;
    public Collider collidedObject;

    private HealthBarController healthBar;
    public float startingHealth = 10f;
	protected float health;
    public event System.Action OnDeath;
    public Transform spawnArea;

    void Start()
    {
        controller = gameObject.GetComponent<PlayerController>();
        shotController = gameObject.GetComponent<ShotController>();

        healthBar = GameObject.Find(string.Format("Health Bar {0}", playerIndex)).GetComponent<HealthBarController>();
        healthBar.SetMaxHP(startingHealth);

        Respawn();
    }

    void Update()
    {
        
        //Weapon Control
        if (Input.GetKey(shotKey[playerIndex-1])) {
			shotController.Shoot();
		}

        //Health Bar Update
        healthBar.SetHP(health);
    }

    public void TakeHit(float damage, RaycastHit hit) {
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

    private void Respawn()
    {
        health = startingHealth;
        transform.position = spawnArea.position;
    }

    void OnCollisionEnter3D (Collider collidedObject)
    {
    switch (collidedObject.tag) 
        {
        case "DeathTrigger":
            Die();
            break;
        case "Car":
            Die();
            break;
            
        }
    }
}


