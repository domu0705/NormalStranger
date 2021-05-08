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

        talkData.Add(2000,new string[] { "...네?:6", "제가 좀 바빠서..:7"});
        talkData.Add(3000, new string[] { "아! 안녕하세요.:8", "뭐 재밌는 일 있나요? :9" });

        /*
         * quest talk 
         * quest는 10번부터 시작.
         */
        talkData.Add(10 + 2000, new string[] { "......씨?:6", "피트라씨?:7", 
                                                ".....:2",
                                                "피.트.라.씨?:7",
                                                "....네??:4",
                                                "그러니까 Sindy 씨에게 맡겨놓은 문서 처리 좀 부탁해요. :5", 
                                                ".....예....? :4",
                                                "부탁해요. 최대한 빨리.:5",
                                                ".....아?.. 네네. 알겠습니다.:2",
                                                "(...방금 전까지 내가 뭐하고 있었더라):3" ,
                                                "(신디씨...? 빨간 머리였던 것 같은데..):3",
                                              });
        talkData.Add(11 + 2000, new string[] { "문서는 Sindy 씨에게 맡겨뒀어요. 부탁해요.:5" });
        talkData.Add(11 + 3000, new string[] { "안녕하세요~?:8", "아 문서요? 여기요~:9", "옆 사무실 B동 의 그린씨에게 전달 부탁해요~:8",
                                               "(그린씨… 밝은 갈색머리..):3", "(정신은 없어도 기억력은 좋군):1",
                                               "아! 사람들과 부딪히지 않게 조심하세요~:9", "다들 늘 화가 잔뜩 나있으니까요~!:8",
                                               "(문서를 얻었다.):0",
                                             });

        talkData.Add(20 + 3000, new string[] { "B동 그린씨에게 전달하면 돼요!:9", "서두르는게 좋을거예요~:8" });
        talkData.Add(21 + 2000, new string[] { "문서는 잘 전달했어요?:6", "좋아. 이거 받아요:5", "(그가 에너지 부스터를 건넸다):5","서랍 속에 가끔 이게 굴러다니더라고. 감사인사는 이걸로.:5" });


        /*portrait data 생성
         * Fitra : 1000
         * Johnny : 2000
         * Sindy : 3000
         
         */
        portraitData.Add(0, portraitArr[0]);
        portraitData.Add(1, portraitArr[1]);
        portraitData.Add(2, portraitArr[2]);
        portraitData.Add(3, portraitArr[3]);
        portraitData.Add(4, portraitArr[4]);
        portraitData.Add(5, portraitArr[5]);
        portraitData.Add(6, portraitArr[6]);
        portraitData.Add(7, portraitArr[7]);
        portraitData.Add(8, portraitArr[8]);
        portraitData.Add(9, portraitArr[9]);
    }

    public string GetTalk (int id, int talkIndex) // .GetTalk(id + questTalkIndex, talkIndex) 이렇게 Game Manager에서쓰임
    {
        Debug.Log("id:" + id + "talkINdex: " + talkIndex);
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

        if (talkIndex == talkData[id].Length)
        {// 얘기가 모두 끝났을 때
            Debug.Log("한사람과 얘기 끝");
            return null;
        }
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[portraitIndex];
    }

}
