using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Slider healthBar;
    private float currentHP = 100;
    void Start()
    {
        healthBar = GetComponent<Slider>();
    }

    void Update()
    {
        if(currentHP <= 0)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
        
        healthBar.value = currentHP;
    }

    public void SetMaxHP(float maxHP)
    {
        healthBar.maxValue = maxHP;
    }
    public void SetHP(float newHP)
    {
        currentHP = newHP;
    }
}
