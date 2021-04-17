using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Vector3 dirRayVec;//플레이어 앞방향으로 나가는 탐지용 ray
    GameObject scanObject;

    public GameManager manager;
    public bool isHorizonMove;
    public float speed;
    public float h;
    public float v;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        h =  manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v =  manager.isAction ? 0 : Input.GetAxisRaw("Vertical");
        
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");//getbuttonDown은 누를때 한번만 true가 됨
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        if (hDown)                  //누르는 순서 상관없이 마지막에 누르는 방향으로 움직이게 되는 로직임
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if(hUp || vUp)         // 왼,오른쪽 키를 동시에 눌렀다 한쪽만 떼어도 ishorizontal은 true여야 하는 상황을 고려한 조건
            isHorizonMove =  h !=0; // 현재 속도(axisRaw)값에 따라 판단하여 해결.(왼쪽키 떼어도 오른쪽으로 움직이고 있으면 isHorizonMove는 true. )

        changeAnim();
        setDirRay(hDown, vDown,hUp,vUp);

        //물체 검사
        if (Input.GetButtonDown("Jump") && scanObject != null)//스페이스바 누른다면
        {
            manager.Action(scanObject);
        }


    }


    void FixedUpdate()
    {
        //캐릭터 이동하기
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;

        //캐릭터 ray
        //(플레이어도 collider가 있기 때문에 조사 가능한 것들을 모두 다 다른 layer(inspectObject)로 설정해주기)
        Debug.DrawRay(rigid.position, dirRayVec * 0.7f, new Color(0, 1, 0)); // 진짜 ray쏘는 것이 아닌 확인을 위해 그려주는 것
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirRayVec, 0.7f,LayerMask.GetMask("InspectObject"));

        if (rayHit.collider != null)//부딪힌 사물이 있다면
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;
    }


    void changeAnim()
    {
        if (manager.isAction) // player가 말하는 중이라면 움직이지 않게 한다.
        {
            h = 0;
            v = 0;
        }
        //이미지의 방향 전환
        if (Input.GetButton("Horizontal") && !manager.isAction) // manager.isAction중에는 좌우 뒤집히지 않도록
            spriteRenderer.flipX = (Input.GetAxisRaw("Horizontal") == -1);


        /* animation
             h값이 변하지 않을 때도 계속 setinteger을 하면 에니메이션이 계속 처음부터 반복 실행되면서 에니메이션이 재생되지 못함. 
             그래서 h값이 변할대만 setinteger을 해줌
        */ 
        if(anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
            
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false);

    }

    void setDirRay(bool hDown, bool vDown, bool hUp, bool vUp) // 플레이어가 보는 앞 방향으로 물체 탐지용 ray를 그려주는 함수
    {
        if (vDown && v == 1) // vDown 이면서 v == 1
            dirRayVec = Vector3.up;
        else if (vDown && v == -1 )
            dirRayVec = Vector3.down;
        else if (hDown && h == 1)
            dirRayVec = Vector3.right;
        else if (hDown && h == -1)
            dirRayVec = Vector3.left;
   }

}
