using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

[RequireComponent (typeof (ShotController))]
public class PlayerAgent : Agent
{
    Rigidbody rBody;
    ShotController shotController;
    void Start() {
        rBody = GetComponent<Rigidbody>();
        shotController = gameObject.GetComponent<ShotController>();
        _groundChecker = transform.GetChild(0);
    }

    private int iterations;
    public Target target;
    private Target currentTarget;
    public override void OnEpisodeBegin()
    {
        Debug.Log("Episode Begins");
        
        iterations = 0;

        transform.localPosition = new Vector3(Random.value * 16f - 8f, 2f, Random.value * 16f - 8f);

        currentTarget = Instantiate(target);
        currentTarget.transform.SetParent(transform.parent);

        if (this.transform.localPosition.y < 0)
        {
            // If the Agent fell, zero its momentum
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3( 0, 2f, 0);
        }

        // Move the target to a new spot
        currentTarget.transform.localPosition = new Vector3(Random.value * 8 - 4,
                                           1f,
                                           Random.value * 8 - 4);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        if(currentTarget.dead){
            Debug.Log("Already Dead");
            sensor.AddObservation(new Vector3(0, 0, 0));
        }else{
            sensor.AddObservation(currentTarget.transform.localPosition);
        }
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rBody.velocity);
        sensor.AddObservation(rBody.transform.forward);
    }
    public int playerIndex = 1;
    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float GroundDistance = 1.5f;
    public LayerMask Ground;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    private Vector3 _inputs = Vector3.zero;
    private string[] horizontalAxis = {"Horizontal1", "Horizontal2"};
    private string[] verticalAxis = {"Vertical1", "Vertical2"};
    private string[] jumpButton = {"Jump1", "Jump2"};
    private KeyCode[] shotKey = {KeyCode.Z, KeyCode.Slash};
    private int horizontalRaw;
    private int verticalRaw;
    private int jumpPrev = 0;
    public override void OnActionReceived(float[] act)
    {   
        ++iterations;
        float reward = 0;
        // Make action
        horizontalRaw = Mathf.FloorToInt(act[0]) - 1;
        verticalRaw = Mathf.FloorToInt(act[1]) - 1;
        int jumpRaw = Mathf.FloorToInt(act[2]);
        int shootRaw = Mathf.FloorToInt(act[3]);
        
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);


        _inputs = SmoothInput(horizontalRaw, verticalRaw);
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

        rBody.MovePosition(rBody.position + _inputs * Speed * Time.deltaTime);

        if ( (jumpPrev == 0 && jumpRaw == 1 ) && _isGrounded)
        {
            rBody.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
        jumpPrev = jumpRaw;

        if (shootRaw == 1) {
            reward += 0.001f;
            shotController.Shoot();
		}

        // Check episode
        if(currentTarget == null && currentTarget.dead){
            SetReward(1.0f);
            EndEpisode();
        }

        if(iterations > 300){
            currentTarget.Die();
            EndEpisode();
        }

        if (this.transform.localPosition.y < 0)
        {
            currentTarget.Die();
            SetReward(-1.0f);
            EndEpisode();
        }

        SetReward(reward);
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

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxisRaw(horizontalAxis[playerIndex-1])+1;
        actionsOut[1] = Input.GetAxisRaw(verticalAxis[playerIndex-1])+1;
        actionsOut[2] = Input.GetButton(jumpButton[playerIndex-1])?1:0;
        actionsOut[3] = Input.GetKey(shotKey[playerIndex-1])?1:0;
    }
}
