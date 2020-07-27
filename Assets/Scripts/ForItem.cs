using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForItem : MonoBehaviour
{

    void Start()
    {
               gameObject.transform.localPosition 
       = new Vector3(Random.Range(-21.0f, 26.0f), 
       -5f, Random.Range(-5.0f, -40.0f));
    }

    void update()
    {

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
