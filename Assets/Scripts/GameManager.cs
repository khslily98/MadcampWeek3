using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {
        get {
            if(m_instance == null){
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }
    
    public bool isGameover { get; private set; }
    private static GameManager m_instance;
    
    public int player1Lives = 3, player2Lives = 3;
    private Player player1, player2;
    public GameObject egg11, egg12, egg13;
    public GameObject egg21, egg22, egg23;
    void Awake() {
        if(instance != this) {
            Destroy(gameObject);
        }else {
            player1 = GameObject.Find("Duck 1").GetComponent<Player>();
            player2 = GameObject.Find("Duck 2").GetComponent<Player>();

            Debug.Log(player1.ToString());

            player1.OnDeath += OnPlayer1Death;
            player2.OnDeath += OnPlayer2Death;
        }
    }
    void Start() {
        if(Values.GameMode == 1) {
            // 1P Mode
        } else {
            // 2P Mode
        }
    }

    void OnPlayer1Death() {
        player1Lives -= 1;
        switch (player1Lives) {
            case 2:
                egg13.gameObject.SetActive(false);
                break;
            case 1:
                egg12.gameObject.SetActive(false);
                break;
            case 0:
                egg11.gameObject.SetActive(false);
                GameOver(1);
                break;
        }
    }

        void OnPlayer2Death() {
        player2Lives -= 1;
        switch (player2Lives) {
            case 2:
                egg23.gameObject.SetActive(false);
                break;
            case 1:
                egg22.gameObject.SetActive(false);
                break;
            case 0:
                egg21.gameObject.SetActive(false);
                GameOver(2);
                break;
        }
    }


    void GameOver(int loser) {

    }
}
