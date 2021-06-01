using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAI : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public Transform playerTrans;
    public float speed;
    public int maxSpeed;
    public bool sideWalking;
    public bool startChasing; // Police AI가 나타나도 말이 끝나고 추격을 시작해야함. 이걸 관리하는 변수.
    public float detectDist; //특정 거리를 나타내는 변수. 플레이어와 일정거리 이하가 되면 쫒아가기위함.
    public bool powerOff;
    //float blockingCheck;
    float clock;
    Vector2 moveVec;


   
    Transform prevPosition;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        prevPosition = this.transform;

    }
    void Update()
    {
        if (!startChasing || powerOff)//전원켜지기 전이나 전원꺼버렸을떄는 아예 멈추기
        {
            StopCoroutine("changeDir");
            rigid.velocity = new Vector2(0, 0);
            this.gameObject.layer = 18;
            return;
        }
        else
        {
            this.gameObject.layer = 16;
        }  
        float distance = (playerTrans.position - this.transform.position).sqrMagnitude;
        if (detectDist >= distance) // 거리가 가까우면 따라가기
        {
            changeDir();
        }
            
        else // 거리가 멀면 멈추기 
        {
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


        //Invoke("changeDir", 0.3f);
    }

    public void policePowerOff()
    {
        spriteRenderer.color = new Color(0.21f, 0.18f, 0.245f, 0.9f);
    }
    

}
