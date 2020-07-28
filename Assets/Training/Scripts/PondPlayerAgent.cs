using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

[RequireComponent (typeof (ShotController))]
public class PondPlayerAgent : Agent
{
    Rigidbody rBody;
    ShotController shotController;
    void Start() {
        rBody = GetComponent<Rigidbody>();
        shotController = gameObject.GetComponent<ShotController>();
        _groundChecker = transform.GetChild(0);
    }

    public Target target;
    private Target currentTarget;
    public override void OnEpisodeBegin()
    {
        //Debug.Log("Episode Begins");

        transform.localPosition = new Vector3(Random.Range(-21.0f, 26.0f),-5f, Random.Range(-5.0f, -40.0f));

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        // if(currentTarget.dead){
        //     Debug.Log("Already Dead");
        //     sensor.AddObservation(new Vector3(0, 0, 0));
        // }else{
        //     sensor.AddObservation(currentTarget.transform.localPosition);
        // }
        // sensor.AddObservation(this.transform.localPosition);

        // // Agent velocity
        // sensor.AddObservation(rBody.velocity);
        // sensor.AddObservation(rBody.transform.forward);
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

        // if ( (jumpPrev == 0 && jumpRaw == 1 ) && _isGrounded)
        // {
        //     rBody.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        // }
        // jumpPrev = jumpRaw;

        if (shootRaw == 1) {
            reward += 0.0001f;
            shotController.Shoot();
		}

        // Check episode
        if(currentTarget.dead){
            SetReward(1.0f);
            EndEpisode();
        }

        if (this.transform.localPosition.y < 0 || this.transform.localPosition.y > 8)
        {
            currentTarget.Die();
            SetReward(-1.0f);
            EndEpisode();
        }

        SetReward(reward-0.005f);
    }

    // private void OnCollisionEnter(Collision other) {
    //     if(other.gameObject.CompareTag("wall")){
    //         Debug.Log("Collision with wall");
    //         AddReward(-0.0001f);
    //     }
    // }


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
