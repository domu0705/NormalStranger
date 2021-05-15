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
    public GameObject marin;

    public Vector3 MarinPos;

    bool isMarinMovingToFitra;
    bool isMarinReturning;
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
    }
    void GenerateData()
    {
        questList.Add(10, new QuestData("Sindy에게 서류를 받자", new int[] { 2000, 3000 }));
        questList.Add(20, new QuestData("서류를 그린씨에게 전달하자.", new int[] { 4000,2000 }));
        questList.Add(30, new QuestData("피트라의 자리로 가자.", new int[] { 300 }));
        questList.Add(40, new QuestData("Game Clear.", new int[] { 0 }));
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
                if (manager.talkIndex == 3)//마린을 피트라 옆으로 이동시키기
                {
                    isMarinMovingToFitra = true;
                    manager.objData.isNpc = true;
                }
                if (manager.talkIndex == 12)//마린을 피트라 옆으로 이동시키기
                {
                    Debug.Log("마린 제자리로 가");
                    manager.objData.isNpc = false;
                    isMarinReturning = true;
                }
                break;
            case 40://자기 자리로 가기
                break;
            case 50:
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
}
