using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public GameObject player1, player2;
    private PondPlayerAgent agent1, agent2;
    void Awake() {

        Debug.Log(player1.ToString());

        player1.GetComponent<Player>().OnDeath += OnPlayer1Death;
        player2.GetComponent<Player>().OnDeath += OnPlayer2Death;
        agent1 = player1.GetComponent<PondPlayerAgent>();
        agent2 = player2.GetComponent<PondPlayerAgent>();
    }

    void OnPlayer1Death() {
        Debug.Log("P1 death");

        agent1.SetReward(-1.0f);
        //agent2.SetReward(1.0f);

        agent1.EndEpisode();
        agent2.EndEpisode();
    }

    void OnPlayer2Death() {
        Debug.Log("P2 death");

        //agent1.SetReward(1.0f);
        agent2.SetReward(-1.0f);

        agent1.EndEpisode();
        agent2.EndEpisode();
    }

    // 총쏘면 +0.0001 총맞추면 +1 총맞으면 -0.01 숨만쉬면 -0.0001 아이템 먹으면 +0.05
}
