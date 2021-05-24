using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/*
 *  OnCollisionEnter2D : 부딪히는 놈 2개다 isTrigger가 꺼져 있으면 호출=>물리적 접촉시,
 *  OnTriggerEnter2D : 둘중 하나라도 isTrigger가 켜져 있으면 호출 => 물리적 접촉이 아닌 통과될때
 */
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    public Vector3 dirRayVec;//플레이어 앞방향으로 나가는 탐지용 ray

    public GameManager manager;
    public QuestManager questManager;
    public EnergyBooster energyBooster;
    public GameObject scanObject;
    public Animator anim;


    public bool mustAnimBack;//강제로 anim이 정해져있을 때, playermove script에서 맘대로 anim 조건들을 못바꾸게 하기 위해 사용하는 변수.(플레이어 자동 이동 시 무조건 뒤를보는 anim으로 만들어주기 위함임)
    public bool isHorizonMove;
    public bool isInvincible;//한번 체력 줄면 2초동안 무적
    public bool isDead; // 플레이어가 죽었다면 플레이어의 동작 멈추기 위한 변수
    public float speed;
    public float h;
    public float v;
    public int heart;

    public bool isFitraMonologing;//혼잣말을 할 떄는 scanobj에 무조건 fitra자신을 넣어주는 변수
    /* item 선택 버튼*/
    public bool press1;
    bool press2;
    bool press3;
    bool press4;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (!isDead )
        {
            if (!manager.isPlayerPause)
            {
                getInput();

                useItem();

                //플레이어의 이동 밎 ray 쏘기
                playerMoveAndRay();
            
                //플레이어의 animation을 변경
                changeAnim();
            }
            

            //물체 검사 (스페이스바를 누를 시 함수 내부가 실행됨)
            scanObj();
        }
    }


    void FixedUpdate()
    {
        if (!isDead)
        {
            //캐릭터 이동하기
            if (!manager.isPlayerPause)
            {
                Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
                rigid.velocity = moveVec * speed;
            }
            else
            {
                rigid.velocity = new Vector2(0, 0);
            }
            

            //캐릭터 ray
            //(플레이어도 collider가 있기 때문에(플레이어가 스스로의 collider을 감지하지 않게하기위해) 조사 가능한 것들을 모두 다 다른 layer(inspectObject)로 설정해주기)
            Debug.DrawRay(rigid.position, dirRayVec * 0.7f, new Color(0, 1, 0)); // 진짜 ray쏘는 것이 아닌 확인을 위해 그려주는 것
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirRayVec, 0.7f,LayerMask.GetMask("InspectObject"));

            if (rayHit.collider != null)//부딪힌 사물이 있다면
            {
                scanObject = rayHit.collider.gameObject;
                if (manager.checkControlState)
                {
                    questManager.ControlObject();//보통 대화중에만 controlobject함수를 불러줌. 그래서 대화중이 아닐때 1st floor의 anim trigger선을 넘을 때를 확인해주기 위해 함수를 호출.
                }
            }
            else
                scanObject = null;
        }
        
    }



    /*item 사용하려는 버튼 입력을 받음*/
    void getInput()
    {
        press1 = Input.GetButtonDown("Press1");
        press2 = Input.GetButtonDown("Press2");
        press3 = Input.GetButtonDown("Press3");
        press4 = Input.GetButtonDown("Press4");
    }


    /*받은 입력에 따라 item 을 사용*/

    void useItem()
    {
        if (press1 && (heart < 5) )
        {
            heart++;
            manager.heartChanged();
            energyBooster.useBooster();
        }
    }


    void playerMoveAndRay()
    {
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");//getbuttonDown은 누를때 한번만 true가 됨
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        if (!manager.isAutoMoving)
        {
            if (hDown)                  //누르는 순서 상관없이 마지막에 누르는 방향으로 움직이게 되는 로직임
                    isHorizonMove = true;
            else if (vDown)
                    isHorizonMove = false;
            else if (hUp || vUp)         // 왼,오른쪽 키를 동시에 눌렀다 한쪽만 떼어도 ishorizontal은 true여야 하는 상황을 고려한 조건
                    isHorizonMove = h != 0; // 현재 속도(axisRaw)값에 따라 판단하여 해결.(왼쪽키 떼어도 오른쪽으로 움직이고 있으면 isHorizonMove는 true. )

            anim.SetBool("isHorizonMove", isHorizonMove); //animation에서 위키를 안떼고 오른쪽키을 추가로 누를 때,에니메이션의 변경 우선순위가 '위'가 먼저인듯.
                                                              //animation에서 어쨌든 isVertical도 0보다 크니까 자꾸 위보는 에니메이션을 실행시키는 듯. (anystate에서 위로도 갈수있고 오른쪽으로도 갈수 있게 되는데 그냥 위로 가는듯.)
                                                              //에니메이션이 계속 위로 가는 에니메이션에 멈춰있는 것 막기 위함. 
        }
        

        setDirRay(hDown, vDown, hUp, vUp);// 플레이어가 보는 앞 방향으로 물체 탐지용 ray를 그려주는 함수
    }

    void changeAnim()
    {
        if (manager.isAction && !(manager.isAutoMoving)) // player가 말하는 중이라면 움직이지 않게 한다.
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
        if (isInvincible) // 공격당하는 모션 중에는 다른 모션으로 안넘어감
            return;

        if (  (anim.GetInteger("hAxisRaw") != h )&& !mustAnimBack) 
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }

        else if ((anim.GetInteger("vAxisRaw") != v) && !mustAnimBack ) 
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);

        }
        else if(!mustAnimBack)
        {

            anim.SetBool("isChange", false);
        }

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

    

    /* 스페이스바 누른다면*/
    public void scanObj()
    {
        if (Input.GetButtonDown("Jump") && scanObject != null && manager.canPressSpace)
        {
            if (isFitraMonologing)//혼잣말
            {
                manager.Action(this.gameObject);
            }
            else
            {
                Debug.Log("조사해");
                manager.Action(scanObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)// OnCollisionEnter은 collider/rigidbody에 다른 collider/rigidbody가 닿을 때 호출됨.
    {
        if(other.gameObject.tag == "Enemy")
        {
            damaged(other.transform.position);
        }
    }


    void damaged(Vector2 targetPos) { 
        //무적이라면 heart 감소하지 않음
        if (isInvincible)
            return;
        isInvincible = true;
        Debug.Log("damaged 실행");

        

        heart--;
        if(heart > 0)
        {
            //다치는 animation
            anim.SetTrigger("isDamaged");

            //투명도 올려줌
            //spriteRenderer.color = new Color(0.8f, 0.3f, 0.18f, 0.4f);
            spriteRenderer.color = new Color(1, 0, 0, 0.4f);

            //튕겨나가는 효과
            int direction = transform.position.x > targetPos.x ? 1 : -1;
            rigid.AddForce(new Vector2(direction, 1) * 100000, ForceMode2D.Impulse);

            //heart UI개수 변경, 무적 해제
            manager.heartChanged();
            Invoke("offInvincible", 1.5f);
        }
        else // player 사망
        {
            gameObject.layer = 14; //layer을 바꿔서 죽었을 때 enemy가 player을 밀지 않도록 함
            rigid.velocity = new Vector2(0,0);
            anim.SetTrigger("doDie");
            isDead = true;
            manager.heartChanged();
            manager.gameOver();

        }
        
    }


    //플레이어의 무적 효과를 해제하는 함수
    void offInvincible()
    {
        //무적 해제
        isInvincible = false;

        //플레이어의 투명도 원상복구하기
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
