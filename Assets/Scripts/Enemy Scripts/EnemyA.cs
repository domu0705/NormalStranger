using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
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

        Think();
    }
    // Update is called once per frame
    void Update()
    {
        //move
        rigid.velocity = new Vector2(nextMove, 0);

        //벽 충돌시 방향 바꾸기
        avoidCollision();
    }

    void Think()
    {
        //enemy의 움직임을 random하게 조절
        nextMove = Random.Range(-1, 2);

        //enemy의 animation을 조정
        ControlAnim();

        //enemy가 좌,우로 움직일 때 이미지 좌우반전
        if (nextMove != 0)
            FlipAnimation();

        Invoke("Think", 2);
    }


    void avoidCollision()//벽 충돌시 방향 바꾸기
    {
        Vector2 frontVec = new Vector2(rigid.position.x, rigid.position.y);

        if (nextMove == 1)
            rayVec = Vector2.right;
        else
            rayVec = Vector2.left;

        Debug.DrawRay(frontVec, rayVec, new Color(0, 1, 0));//에디터 상에서만 ray를 그려주는 함수
        RaycastHit2D rayHit1 = Physics2D.Raycast(frontVec, rayVec, 0.5f, LayerMask.GetMask("Boundary"));//실제로 ray를 쏘는 함수
        RaycastHit2D rayHit2 = Physics2D.Raycast(frontVec, rayVec, 0.5f, LayerMask.GetMask("InspectObject"));
        if ((rayHit1.collider != null) || (rayHit2.collider != null))
        {//enemy가 벽에 부딪혔다면
            nextMove *= -1;
            FlipAnimation();
        }
    }


    void FlipAnimation()
    {
        spriteRenderer.flipX = (nextMove == -1);
    }

    void ControlAnim()
    {
        if (nextMove != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
            anim.SetBool("isWalking", false);
    }



}

/*
 * ENEMY
 *  man A,B : 2초 단위로 랜덤하게 좌우 이동.
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */
