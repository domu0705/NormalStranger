using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    Vector2 rayVec;

    public int nextMove;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Think();
        nextMove = 1;
    }
    // Update is called once per frame
    void Update()
    {
        //move
        rigid.velocity = new Vector2( 0, nextMove*2);

        //벽 충돌시 방향 바꾸기
        avoidCollision();
    }

    void Think()
    {
        //enemy의 움직임을 random하게 조절
        nextMove = Random.Range(-1, 2);

        //enemy의 animation을 조정
        ControlAnim();

        Invoke("Think", 2);
    }


    void avoidCollision()//벽 충돌시 방향 바꾸기
    {
        Vector2 frontVec = new Vector2(rigid.position.x, rigid.position.y);

        if (nextMove == 1)
            rayVec = Vector2.up;
        else
            rayVec = Vector2.down;

        Debug.DrawRay(frontVec, rayVec, new Color(0, 0.5f, 0));//에디터 상에서만 ray를 그려주는 함수
        RaycastHit2D rayHit1 = Physics2D.Raycast(frontVec, rayVec, 0.5f, LayerMask.GetMask("Boundary"));//실제로 ray를 쏘는 함수
        RaycastHit2D rayHit2 = Physics2D.Raycast(frontVec, rayVec, 0.5f, LayerMask.GetMask("InspectObject"));
        if ((rayHit1.collider != null) || (rayHit2.collider != null))
        {//enemy가 벽에 부딪혔다면
            nextMove *= -1;
            ControlAnim();
        }
    }

    void ControlAnim()
    {
        if (nextMove == -1)
        {
            anim.SetBool("isWalkingFront", true);
        }
        if (nextMove == 1)
        {
            anim.SetBool("isWalkingFront", false);
        }
    }

}
