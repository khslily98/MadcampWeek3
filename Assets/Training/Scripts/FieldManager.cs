using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public Player player1, player2;
    void Awake() {

        Debug.Log(player1.ToString());

        player1.OnDeath += OnPlayer1Death;
        player2.OnDeath += OnPlayer2Death;
    }

    void OnPlayer1Death() {
        Debug.Log("P1 death");
    }

    void OnPlayer2Death() {
        Debug.Log("P2 death");
    }
}
