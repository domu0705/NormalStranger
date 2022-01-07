// -------------------------------------------------------------------------------------------------
// quest 하나의 정보를 관리 ( 퀘스트 이름, 퀘스트 충족 조건)
// -------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;

public class QuestData
{
    public string questName;
    public int[] npcId;

    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
