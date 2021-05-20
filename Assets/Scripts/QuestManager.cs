using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId; // 지금 진행중인 quest id
    public int questActionIndex; // 현재 quest 중에서 이야기의 순서에 맞게 진행되게 하기 위한 변수
    public GameObject[] questObject;
    public GameManager manager;
    public PlayerMove player;
    public GameObject playerObject;
    public autoMove autoMoving;
    public int count;
    public GameObject marin;
    public GameObject green; // 1층 로비에 있는 green
    public GameObject green2; //2층에 있는 green 
    public GameObject fitraDesk;
    public GameObject talkTriggerLine;
    public Vector3 MarinPos;


    /*피트라의 자동이동을 위한 변수*/
    Vector3 targetPos;


    /*캐릭터 자동 이동에 사용되는 변수*/
    bool isMarinMovingToFitra;
    bool isMarinReturning;
    bool isFitraMovingToHumanDoorY;
    bool isFitraMovingToHumanDoorX;

    Dictionary <int, QuestData> questList;
    
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }


    private void Update()
    {

        if (isMarinMovingToFitra)//마린을 피트라 옆으로 옮기기
            moveMarinToFitra();

        if (isMarinReturning)//마린을 원위치로 되돌려놓기
            returnMarin();


        /*피트라를 1층의 Human Door 앞으로 이동시키기*/
        if (isFitraMovingToHumanDoorX)
            StartCoroutine("moveFitraToHumanDoorX");// ();
        if (isFitraMovingToHumanDoorY)
            moveFitraToHumanDoorY();
    }
    void GenerateData()
    {
        questList.Add(10, new QuestData("Sindy에게 서류를 받자", new int[] { 2000, 3000 }));
        questList.Add(20, new QuestData("서류를 그린씨에게 전달하자.", new int[] { 4000,2000 }));
        questList.Add(30, new QuestData("피트라의 자리로 가자.", new int[] { 300,300 }));
        questList.Add(40, new QuestData("1층으로 나가 퇴근하자.", new int[] { 800,4000, 900 })); //800, 4000은 TALK 함수 강제 호출하면 될듯
        questList.Add(50, new QuestData("출근 - 피트라의 자리로 가자.", new int[] { 300 }));
        questList.Add(60, new QuestData("정전", new int[] { 300 }));
        questList.Add(70, new QuestData("Game Clear.", new int[] { 0 }));
        questList.Add(80, new QuestData("Game Clear.", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id) //quest번호를 반환
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }

        ControlObject(); 
        if (questActionIndex == questList[questId].npcId.Length)
        {
            ControlObject();
            NextQuest();
        }
        
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        return questList[questId].questName;
    }

    public void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }


    /*Quest와 관련된 object들을 관리하는 함수
     * 한 talkData 안에 여러 스트링이 있잖아. 
     * 그 중 몇번쨰 string에 특정 일이 일어날때를 관리하기 위해서 talkIndex를 받아옴.
     */
    public void ControlObject() 
    {
        switch (questId)
        {
            case 10://Sindy에게 서류를 받자
                Debug.Log("case 10에 들어옴");
                if (questActionIndex == 2)
                {
                    questObject[1].SetActive(true);
                    Debug.Log("문서 보이게 함");
                }
                break;

            case 20://서류를 그린씨에게 전달하자
                Debug.Log("case 20에 들어옴");
                if (questActionIndex == 1)
                {
                    questObject[1].SetActive(false);
                    Debug.Log("문서 꺼");
                }
                break;

            case 30://피트라의 자리로 가자.
                if (manager.talkIndex == 4)//마린을 피트라 옆으로 이동시키기
                {
                    /*bool값을 바꿔줌으로써 moveMarinToFitra 함수를 호출*/
                    isMarinMovingToFitra = true;

                    manager.objData.isNpc = true;//피트라의 첫 대화상대가 npc가 아닌 자기 책상이기 때문에 portrait가 안나옴. portrait의 등장을 위해 isnpc를 true로 바꿔줌.
                }
                if (manager.talkIndex == 13)//마린을 피트라 옆으로 이동시키기
                {
                    manager.objData.isNpc = false;//피트라의 첫 대화상대가 npc가 아닌 자기 책상이기때문에 (portrait의 등장을 위해 isnpc를 true로 바꿨던 것을) 다시 false로 돌려놓아줌

                    /*bool값을 바꿔줌으로써 returnMarin 함수를 호출*/
                    isMarinReturning = true;
                }
                manager.checkControlState = true;
                break;

            case 40://1층으로 나가 퇴근하자
                /*2층의 그린을 없애기*/
                green2.SetActive(false);
                /*quest를 위해 처음 문 근처로 왔을때 자동으로 HUMAN Door로 player을 옮겨줌 */
                if (player.scanObject && player.scanObject.gameObject.tag == "Anim Trigger Line")
                {
                    manager.checkControlState = false;//playerMove의 fixedupdate에서 계속 controlObject를 부르지 않도록 다시 변수를 false로 바꿈
                    ObjectData scanObj = player.scanObject.GetComponent<ObjectData>();
                    if (scanObj.isChecked)
                        break;
                    else
                    {
                        scanObj.isChecked = true;//이후로 이 길을 지나갈때는 이 에니메이션이 진행되지 않음.
                        manager.isAction = true;//player의 키 입력에 의해 피트라의 자동 움직임이 방해되지 않도록 피트라의 움직임 입력받기를 정지

                        /*피트라가 왼쪽으로 걸어가는 에니메이션으로 바꿔줌*/
                        manager.isAutoMoving = true;
                        player.anim.SetBool("isChange", true);
                        player.anim.SetInteger("hAxisRaw", -1);

                        /*목표 위치 설정*/
                        targetPos = new Vector3(26.1f, player.transform.position.y, player.transform.position.z);

                        /*bool값을 바꿔줌으로써 moveFitraToHumanDoor 함수를 호출*/
                        isFitraMovingToHumanDoorX = true;
                    }
                }

                /*문과 말할때 피트라의 portrait를 보이게 하기 위해 잠시 문의 isNpc를 true로 바꿈*/
                if(manager.objData && questActionIndex == 0 && manager.talkIndex == 1)
                    manager.objData.isNpc = true;
                if (manager.objData && questActionIndex == 0 && manager.talkIndex == 2)
                    manager.objData.isNpc = false;


                /*그린씨가 피트라에게 말걸기*/
                if (questActionIndex == 1)//문과의 대화가 끝났다면 questActionIndex가 1로 바뀜.
                {
                    manager.isAction = true;//player의 키 입력에 의해 그린이 말걸기 전에 다른 곳으로 플레이어가 이동하지 않도록 정지시킴.
                    ObjectData greenScript = green.GetComponent<ObjectData>();
                    if (!greenScript.isArrived)
                    {
                        autoMoving.startAutoMove(green, new Vector3(25.4f, green.transform.position.y, green.transform.position.z));
                        manager.checkControlState = true;//그린이 피트라 옆에 가면 playermove script에서 controlState함수를 불러주기 위함.
                    }
                    if (greenScript.isArrived && manager.checkControlState) // 그린이 피트라 옆에 도착한다면
                    {
                        /*피트라가 그린에게 말걸도록 왼쪽을 보게 함*/
                        player.dirRayVec = Vector3.left;

                        manager.checkControlState = false;
                        autoMoving.arrivedToDest = false;
                        manager.Action(green);

                    }
                }

                /*AI door 로 나감*/
                if (questActionIndex == 3 && manager.talkIndex == 1)
                {
                    Debug.Log("어두워져라 얍");
                    manager.ScreenLightDarken("Day 2");
                    manager.checkControlState = true;
                }
                break;

            case 50: /*출근. 피트라의 자리로 가기*/
                /*1층 로비의 그린을 없애고 2층 그린을 보여주기*/
                green.SetActive(false);
                green2.SetActive(true);

                /*피트라의 자리로 가자는 talkbubble 띄우기*/
                if (player.scanObject && player.scanObject.gameObject.tag == "Talk Trigger Line")
                {
                    manager.checkControlState = false;//playerMove의 fixedupdate에서 계속 controlObject를 부르지 않도록 다시 변수를 false로 바꿈
                    ObjectData scanObj = player.scanObject.GetComponent<ObjectData>();
                    if (scanObj.isChecked)
                        break;
                    else
                    {
                        scanObj.isChecked = true;
                        manager.Action(talkTriggerLine);
                    }
                }

                /*정전 구현*/
                if (questActionIndex == 1 && !manager.isBlackOut) // 정전이 한번 실행되면 이 if문 안으로 다시 안들어가도록 하기 위해 지금 벌써 정전중인지도 확인.
                {
                    Debug.Log("정전 시작");
                    manager.isBlackOut = true;
                    manager.blackout();
                    manager.Action(fitraDesk);
                }
                break;
            case 60:

                break;

        }
    } 


    /*마린을 피트라 옆으로 이동시키는 함수*/
    void moveMarinToFitra()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x + 0.5f, player.transform.position.y, player.transform.position.z);
        marin.transform.position = Vector3.MoveTowards(marin.transform.position, targetPos, 1.5f * Time.deltaTime);
        Debug.Log("마린 등장");

        if (marin.transform.position == targetPos)
            isMarinMovingToFitra = false;
    }



    /*마린을 원위치로 되돌려놓는 함수*/
    void returnMarin()
    {
        marin.transform.position = Vector3.MoveTowards(marin.transform.position, MarinPos, 1.5f * Time.deltaTime);
        Debug.Log("마린 원래자리로가는 함수에 옴");

        if (marin.transform.position == MarinPos)
            isMarinReturning = false;
    }

    /*피트라가 문으로 가는 x축 이동 함수*/
    IEnumerator moveFitraToHumanDoorX()
    {
        /*anim이 계속 새로 시작하는 것 방지용*/
        player.anim.SetBool("isChange", false);
        player.anim.SetInteger("hAxisRaw", -1);

        player.transform.position = Vector3.MoveTowards(player.transform.position, targetPos, 2.5f * Time.deltaTime);

        if (player.transform.position == targetPos)
        {
            /*moveFitraToHumanDoorX 함수의 실행을 멈추기*/
            isFitraMovingToHumanDoorX = false;
            
            /*X축 이동이 끝났으니 이제 Y축으로 이동시키기*/
            targetPos = new Vector3(player.transform.position.x, 14.7f, player.transform.position.z);

            player.mustAnimBack = true;// 강제로 playerMovescript에서  anim을 조절하는 변수들의 값을 뱐화시키는 것을 막는 변수임.

            /*뒤로 걷는 anim을 위한 변수 설정*/
            player.anim.SetInteger("vAxisRaw", 1);
            player.anim.SetBool("isHorizonMove", false);
            player.anim.SetBool("isChange", true);

            yield return new WaitForSeconds(0.2f); // ischange가 너무 빨리 false로 바뀌어서 anim이 안바뀌는 현상을 방지.

            /*moveFitraToHumanDoorY 함수 시작*/
            isFitraMovingToHumanDoorY = true;

        }
    }


    /*피트라가 문으로 가는 y축 이동 함수*/
    void moveFitraToHumanDoorY()
    {
        /*anim 이 무한루프로 시작하지 않게 유지용*/
        player.anim.SetBool("isChange", false);

        player.transform.position = Vector3.MoveTowards(player.transform.position, targetPos, 2.5f * Time.deltaTime);

        if (player.transform.position == targetPos)
        {
            /*자동으로 뒤를 보게 했기떄문에 player의 direction ray도 자동으로 맞춰놓기*/
            player.dirRayVec = Vector3.up;

            /*moveFitraToHumanDoorY함수의 실행을 멈추기*/
            isFitraMovingToHumanDoorY = false;



            /*바로 문에 말걸도록 함*/
            Invoke("autoTalkToHumanDoor", 0.5f);

            /*피트라의 움직임을 다시 player의 키 입력으로 조절함*/
            //manager.isAction = false;
            player.mustAnimBack = false;
            manager.isAutoMoving = false;

        }
    }


    void autoTalkToHumanDoor()
    {
        manager.Action(player.scanObject);
    }
    
}
