using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (ShotController))]
public class Player : LivingEntity
{
    private PlayerController controller;
    private ShotController shotController;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 5.0f;
    private float jumpHeight = 1.0f;
    private KeyCode[] shotKey = {KeyCode.Z, KeyCode.Slash};
    public int playerIndex;
    public Collider collidedObject;

    float timerforitem = 0f;
    float MAX = 10f;

    protected override void Start()  
    {
        base.Start();
        controller = gameObject.GetComponent<PlayerController>();
        shotController = gameObject.GetComponent<ShotController>();
    }

    void Update()
    {
        timerforitem = timerforitem + Time.deltaTime;
        
        if (timerforitem >= MAX)
        {
            eatGrape(false);
        }
        
        //Weapon Control
        if (Input.GetKey(shotKey[playerIndex-1])) {
			shotController.Shoot();
		}
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

    void OnTriggerEnter (Collider collidedObject)
    {
    switch (collidedObject.tag) 
        {
        case "Grape":
            eatGrape(true);
            break;
        }

    }

    void eatGrape(bool boolen)
    {
        if (boolen == true)
        {
            Debug.Log("포도와 충돌");
            playerSpeed = 15f;
            timerforitem = 0;
        }

        else
        {
            playerSpeed = 5f;
        }
    }
}


