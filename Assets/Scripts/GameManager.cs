using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

            isGameover = false;
        }
    }

    void OnPlayer1Death() {
        if(isGameover) return;
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
                GameOver(2);
                break;
        }
    }

    void OnPlayer2Death() {
        if(isGameover) return;
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
                GameOver(1);
                break;
        }
    }

    public Image fadePlane;
    public GameObject gameOverUI;
    public Text gameOverText;
    IEnumerator Fade(Color from, Color to, float time) {
		float speed = 1 / time;
		float percent = 0;

		while (percent < 0.5) {
			percent += Time.deltaTime * speed;
			fadePlane.color = Color.Lerp(from,to,percent);
			yield return null;
		}
	}

    void GameOver(int winner) {
        isGameover = true;
        StartCoroutine(Fade(Color.clear, Color.black, 1));
        gameOverText.text = string.Format("Player {0} wins!", winner);
        gameOverUI.SetActive(true);
        
    }

    public void GoToMain() {
        Debug.Log("GoToMain");
        SceneManager.LoadScene(0);
    }
}
