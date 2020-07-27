using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public float Speed = 2f;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public LayerMask Ground;
    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    public int playerIndex;
    private string[] horizontalAxis = {"Horizontal1", "Horizontal2"};
    private string[] verticalAxis = {"Vertical1", "Vertical2"};
    private string[] jumpButton = {"Jump1", "Jump2"};

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);


        // _inputs = Vector3.zero;
        // _inputs.x = Input.GetAxis(horizontalAxis[playerIndex - 1]);
        // _inputs.z = Input.GetAxis(verticalAxis[playerIndex - 1]);


        _inputs = SmoothInput(Input.GetAxisRaw(horizontalAxis[playerIndex - 1]), Input.GetAxisRaw(verticalAxis[playerIndex - 1]));

        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

        if (Input.GetButtonDown(jumpButton[playerIndex - 1]) && _isGrounded)
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
        // if (Input.GetButtonDown("Dash"))
        // {
        //     Vector3 dashVelocity = Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));
        //     _body.AddForce(dashVelocity, ForceMode.VelocityChange);
        // }
    }


    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }

    private float slidingH;
    private float slidingV;
    private Vector3 SmoothInput(float targetH, float targetV)
    {
        float sensitivity = 3f;
        float deadZone = 0.001f;

        slidingH = Mathf.MoveTowards(slidingH,
                    targetH, sensitivity * Time.deltaTime);

        slidingV = Mathf.MoveTowards(slidingV ,
                    targetV, sensitivity * Time.deltaTime);

        return new Vector3(
            (Mathf.Abs(slidingH) < deadZone) ? 0f : slidingH ,
            0,
            (Mathf.Abs(slidingV) < deadZone) ? 0f : slidingV );
    }
}