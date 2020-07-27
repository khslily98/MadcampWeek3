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
    void Awake() {
        if(instance != this) {
            Destroy(gameObject);
        }
    }
    public Player player;
    public int player1Lives = 3;
    public int player2Lives = 3;
    private Player player1;
    private CinemachineVirtualCameraBase camera1;
    private CinemachineVirtualCamera camera2;
    private Player player2;
    
    void Start() {
        // Set camera
        if(Values.GameMode == 1) {
            // 1P Mode
        } else {
            // 2P Mode
            LoadPlayer(1);
            LoadPlayer(2);
        }

    }

    private void LoadPlayer(int playerIndex){
        switch(playerIndex){
            case 1:
                // Need to set positions
                player1 = Instantiate(player);
                player1.playerIndex = 1;
                break;
            case 2:
                // Need to set positions
                player2 = Instantiate(player);
                player2.playerIndex = 2;
                break;
        }
    }
}
