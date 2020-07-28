using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForItem : MonoBehaviour
{
    float timerforitem;
    float MAX = 10f;

    void Start()
    {
        timerforitem = 0;
        gameObject.transform.localPosition 
            = new Vector3(Random.Range(-21.0f, 26.0f), 
       -5f, Random.Range(-5.0f, -40.0f));
    }

    void Update()
    {
        timerforitem += Time.deltaTime;
        if (timerforitem >= MAX)
        {
            timerforitem = 0f;
            gameObject.transform.localPosition 
                = new Vector3(Random.Range(-21.0f, 26.0f), 
            -5f, Random.Range(-5.0f, -40.0f));
            
        }

    }



    void OnTriggerEnter (Collider collidedObject)
        {
        switch (collidedObject.tag) 
            {
            case "Duck":
                setItem();
                break;
            }
        }


    void setItem()
    {
       gameObject.transform.localPosition 
       = new Vector3(Random.Range(-21.0f, 26.0f), 
       -5f, Random.Range(-5.0f, -40.0f));
    }

}
