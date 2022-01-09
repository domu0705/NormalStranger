// -------------------------------------------------------------------------------------------------
// quest 하나의 정보를 관리 ( 퀘스트 이름, 퀘스트 충족을 위해 필요한 npc의 id(번호))
// -------------------------------------------------------------------------------------------------
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
