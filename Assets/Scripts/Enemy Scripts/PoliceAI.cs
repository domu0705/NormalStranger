using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAI : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public Transform playerTrans;
    public int speed;
    public int maxSpeed;
    public bool sideWalking;
    public float detectDist;

    float blockingCheck;
    float clock;
    Vector2 moveVec;

    Transform prevPosition;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        prevPosition = this.transform;

        blockingCheck = 1;

        
    }
    void Update()
    {
        float distance = (playerTrans.position - this.transform.position).sqrMagnitude;
        Debug.Log("지금 거리는 : " + distance);
        if (detectDist >= distance)
            changeDir();
        else
        {
            Debug.Log("멈춤");
            StopCoroutine("changeDir");
            rigid.velocity = new Vector2(0, 0);

        }
            
    }
    void changeDir()
    {
        moveVec = (playerTrans.position - this.transform.position).normalized;

        if (Mathf.Abs(moveVec.x) >= Mathf.Abs(moveVec.y))
        {
            if (!sideWalking) // 이전에 앞을 보고있었다면 anim을 바꿔줌
            {
                sideWalking = true;
                anim.SetBool("isFrontWalking", false);

                /*anim 왼.오른쪽에 맞춰 뒤집기*/
                spriteRenderer.flipX = (moveVec.x < 0);
            }

        }
        else
        {
            if (sideWalking)// 이전에 옆을 보고있었다면 anim을 바꿔줌
            {
                sideWalking = false;
                anim.SetBool("isFrontWalking", true);
            }
        }
        rigid.velocity = moveVec * speed;

        if (rigid.velocity.x >= maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, 0);
        if (rigid.velocity.x <= -maxSpeed)
            rigid.velocity = new Vector2(-maxSpeed, 0);
        if (rigid.velocity.y >= maxSpeed)
            rigid.velocity = new Vector2(0, maxSpeed);
        if (rigid.velocity.y <= -maxSpeed)
            rigid.velocity = new Vector2(0, -maxSpeed);


        Invoke("changeDir", 0.3f);
    }


    

}
