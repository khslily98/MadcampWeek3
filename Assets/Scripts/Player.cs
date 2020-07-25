using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (ShotController))]
public class Player : LivingEntity
{
    private CharacterController controller;
    private ShotController shotController;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public int playerIndex;
    private string[] horizontalAxis = {"Horizontal1", "Horizontal2"};
    private string[] verticalAxis = {"Vertical1", "Vertical2"};
    private string[] jumpKey = {"Jump1", "Jump2"};
    private KeyCode[] shotKey = {KeyCode.Slash, KeyCode.Z};

    protected override void Start()  
    {
        base.Start();
        controller = gameObject.GetComponent<CharacterController>();
        shotController = gameObject.GetComponent<ShotController>();
    }

    void Update()
    {
        // Movement Control
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis(horizontalAxis[playerIndex-1]), 0, Input.GetAxis(verticalAxis[playerIndex-1]));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown(jumpKey[playerIndex-1]) && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //Weapon Control
        if (Input.GetKey(shotKey[playerIndex-1])) {
			shotController.Shoot();
		}
    }

}
