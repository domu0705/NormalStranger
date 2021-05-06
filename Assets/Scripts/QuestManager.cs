using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId; // 지금 진행중인 quest id
    public int questActionIndex; // 현재 quest 중에서 이야기의 순서에 맞게 진행되게 하기 위한 변수
    public GameObject[] questObject;
    
    Dictionary <int, QuestData> questList;
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("Sindy에게 서류를 받자", new int[] { 2000, 3000 }));
        questList.Add(20, new QuestData("서류를 B동으로 가져가자.", new int[] { 3000,2000 }));
        questList.Add(30, new QuestData("Game Clear.", new int[] { 0 }));
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


    /*Quest와 관련된 object들을 관리하는 함수*/
    void ControlObject()
    {
        switch (questId)
        {
            case 10://문서 받기
                Debug.Log("case 10에 들어옴");
                if (questActionIndex == 2)
                {
                    questObject[1].SetActive(true);
                    Debug.Log("문서 보이게 함");
                }
                    
                break;
            case 20://문서 전달
                Debug.Log("case 20에 들어옴");
                if (questActionIndex == 1)
                {
                    questObject[0].SetActive(false);
                    Debug.Log("문서 꺼");
                }
                break;
            case 30://johnny에게 가기
                break;
            case 40://자기 자리로 가기
                break;
            case 50:
                break;
        }
    } 
}
