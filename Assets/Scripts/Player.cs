using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (ShotController))]
public class Player : LivingEntity
{
    private PlayerController controller;
    private ShotController shotController;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private KeyCode[] shotKey = {KeyCode.Z, KeyCode.Slash};
    public int playerIndex;

    private HealthBarController healthBar;

    protected override void Start()  
    {
        base.Start();
        controller = gameObject.GetComponent<PlayerController>();
        shotController = gameObject.GetComponent<ShotController>();
        healthBar = GameObject.Find(string.Format("Health Bar {0}", playerIndex)).GetComponent<HealthBarController>();
        healthBar.SetMaxHP(startingHealth);
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

    protected override void Die()
    {
        healthBar.SetHP(0);
        base.Die();
    }
}
