using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* gameObject를 원ㅇ하는 위치로 이동시켜주는 함수*/
public class autoMove : MonoBehaviour
{
    GameObject movingObj;
    public Vector3 targetPos;
    public bool isAutoMoving;
    public bool arrivedToDest;
    public float autoSpeed;

    // Update is called once per frame
    void Update()
    {
        if (isAutoMoving)
        {
            float speed = autoSpeed * Time.deltaTime;
            movingObj.transform.position = Vector3.MoveTowards(movingObj.transform.position, targetPos, speed);

            if (movingObj && (movingObj.transform.position == targetPos))
            {                                                               // movingObj 를 if 조건문 안에 넣은 이유 
                isAutoMoving = false;                                       //: movingObj가 지정되지 않은 상황에서 나오는 error방지를 위해 
                arrivedToDest = true;                                       //  먼저 movingObj가 있는지부터 검사해주는 것임 
                ObjectData objectScript = movingObj.GetComponent<ObjectData>();
                objectScript.isArrived = true;

            }

        }
    }


    public void startAutoMove(GameObject movingObject, Vector3 targetPosition,float speed)
    {
        //arrivedToDest = false;
        autoSpeed = speed;
        movingObj = movingObject;
        targetPos = targetPosition;
        isAutoMoving = true;
    }
}
