using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start() {
        
    }
    // void Update()
    // {
        
    // }
}
