using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }



    /*
        100 : johnny's big locker
     */
    void GenerateData()
    {
        /*talk data 생성*/
        talkData.Add(100, new string[] { "...잠겨있다." });
        talkData.Add(100, new string[] { "...잠겨있다." });

        talkData.Add(1000, new string[] { "....?:0", "아...네넵!:0" });
        talkData.Add(2000,new string[] { "피트라씨?:0", "그러니까 이문서를 빨리 B동으로 넘겨줘요. B동이 아딘지는 알죠?:0"});
        talkData.Add(3000, new string[] { "아! 안녕하세요.:0", "뭐 재밌는 일 있나요? :0" });

        /*portrait data 생성*/
        portraitData.Add(1000+0, portraitArr[0]);
        portraitData.Add(2000+0, portraitArr[1]);
        portraitData.Add(3000+0, portraitArr[2]);
    }

    public string GetTalk (int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length) // 얘기가 모두 끝났을 때
            return null;
        else 
            return talkData[id][talkIndex];
    }
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
