using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject mainMenuHolder;
    public GameObject optionsMenuHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Play1P()
    {
        SceneManager.LoadScene("AIPondGame");
    }

    public void Play2P()
    {
        SceneManager.LoadScene("PondGame");
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
