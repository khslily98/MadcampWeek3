﻿using System.Collections;
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
    public float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private KeyCode[] shotKey = {KeyCode.Z, KeyCode.Slash};
    public int playerIndex;

    protected override void Start()  
    {
        base.Start();
        controller = gameObject.GetComponent<PlayerController>();
        shotController = gameObject.GetComponent<ShotController>();
    }

    void Update()
    {
        
        //Weapon Control
        if (Input.GetKey(shotKey[playerIndex-1])) {
			shotController.Shoot();
		}
    }

}
