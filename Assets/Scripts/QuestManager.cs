using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId; // 지금 진행중인 quest id
    public int questActionIndex; // 현재 quest 중에서 이야기의 순서에 맞게 진행되게 하기 위한 변수
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
}
