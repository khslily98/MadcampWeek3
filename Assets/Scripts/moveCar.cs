using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCar : MonoBehaviour
{
    Vector3 pos;
    float MIN = 2.0f; //좌로 이동가능한 (x)최대값
    float MAX = 48.0f; //우로 이동가능한 (x)최대값
    float currentPosition; //현재 위치(x) 저장
    float currentPosition_y;
    float currentPosition_z; //현재 위치(y) 저장
    float direction = 5f; //이동속도+방향

    void Start()
    {
        pos = transform.position;
        currentPosition = transform.position.x;
        currentPosition_y = transform.position.y;
        currentPosition_z = transform.position.z;
        transform.position = new Vector3(currentPosition, 0, currentPosition_z);
    }


    void Update()
    {
     currentPosition = currentPosition + Time.deltaTime * direction;
    
    if (currentPosition >= MAX)
    {
        direction *= -1;
        currentPosition = MAX;
    }



    else if (currentPosition <= MIN)
    {
        direction *= -1;
        currentPosition = MIN;
    }

    transform.position = new Vector3(currentPosition, currentPosition_y, currentPosition_z);

    }
}
