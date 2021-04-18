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
        talkData.Add(200, new string[] { "에너지 부스터가 들어있다... 챙겨보자." });

        talkData.Add(1000, new string[] { "....?:0", "아...네넵!:0" });
        talkData.Add(2000,new string[] { "...네?:0", "제가 좀 바빠서..:0"});
        talkData.Add(3000, new string[] { "아! 안녕하세요.:0", "뭐 재밌는 일 있나요? :0" });

        /*
         * quest talk 
         * quest는 10번부터 시작.
         */
        talkData.Add(10 + 2000, new string[] { "피트라씨?:0", "그러니까 Sindy 씨에게 문서 좀 받아서 빨리 B동으로 넘겨줘요. :0", " B동이 아딘지는 알죠?:0" });
        talkData.Add(11 + 3000, new string[] { "네~ 아 문서요?:0", "여기요~B동 Marin 씨에게 전달해주면 돼요.:0", "가는 길에 누구든 부딪히지 않게 조심해요!...다들 바빠서 신경질이 잔뜩 나있으니까요!:0" });

        talkData.Add(20 + 3000, new string[] { "B동 Marin 씨에게 전달하면 돼요!:0", "서두르는게 좋을거예요~:0" });
        talkData.Add(21 + 2000, new string[] { "문서는 잘 전달했어요?:0", "좋아. 이거 받아요:0", "(그가 에너지 부스터를 건넸다):0","서랍 속에 가끔 이게 굴러다니더라고. 감사인사는 이걸로.:0" });


        /*portrait data 생성*/
        portraitData.Add(1000+0, portraitArr[0]);
        portraitData.Add(2000+0, portraitArr[1]);
        portraitData.Add(3000+0, portraitArr[2]);
    }

    public string GetTalk (int id, int talkIndex) // .GetTalk(id + questTalkIndex, talkIndex) 이렇게 Game Manager에서쓰임
    {
        if (!talkData.ContainsKey(id))// 특정퀘스트의 대사를 만들어놓지 않은 npc에게 말을 건다면?
        {
            //해당 퀘스트 진행 순서 중 대사가 없을 때
            if (!talkData.ContainsKey(id - id % 10))
            {   //퀘스트의 맨 처음 대사마져 없을 때는
                //기본 대사를 가지고 온다.
                return GetTalk(id - id % 100, talkIndex);

                /*
                if (talkIndex == talkData[id - id % 100].Length) // 얘기가 모두 끝났을 때
                    return null;
                else
                    return talkData[id - id % 100][talkIndex];
                */
            }
            else
            {
                //해당 퀘스트 진행 순서 중 대사가 없을 때
                //퀘스트의 맨 처음 대사를 가지고 온다.
                return GetTalk(id - id % 10, talkIndex);

                /*
                if (talkIndex == talkData[id - id % 10].Length) // 얘기가 모두 끝났을 때
                    return null;
                else
                    return talkData[id - id % 10][talkIndex];
                */
            }
            

            
        }

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
