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
        GenerateTalkData();
        GeneratePortraitData();
    }



    /*
        100 : johnny's big locker
        200 : 에너지 부스터가 들어있는 locker
        300 : fitra desk
        400 : 
        500 : 
        600 : quest50 출근하자에 쓰이는 talk trigger Line
        700 : quest 40 에 쓰이는 Anim Trigger Line(1st floor)
        800 : Human Door (1st floor)
        900 : AI Door (1st floor)
        9999: 아무것도 없는 물건.
        
    
        portrait data 생성
         * Fitra : 1000
         * Johnny : 2000
         * Sindy : 3000
         * Green : 4000
         * marin : 5000
         
    */
    void GeneratePortraitData()
    {
        /* portrait data 생성 */
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
        portraitData.Add(10, portraitArr[10]);
        portraitData.Add(11, portraitArr[11]);
        portraitData.Add(12, portraitArr[12]);
        portraitData.Add(13, portraitArr[13]);
        portraitData.Add(14, portraitArr[14]);
        portraitData.Add(15, portraitArr[15]);
        portraitData.Add(16, portraitArr[16]);
        portraitData.Add(17, portraitArr[17]);
    }


    void GenerateTalkData()
    {
        /*디폴트 talk data 생성*/
        talkData.Add(2000, new string[] { "[조니]" + "\n" + "\n" + "...네?:6",
                                          "[조니]" + "\n" + "\n" +"제가 좀 바빠서.. 미안해요~:7" });
        talkData.Add(3000, new string[] { "[신디]" + "\n" + "\n" +"아! 안녕하세요.:8",
                                          "[신디]" + "\n" + "\n" +"뭐 재밌는 일 있나요? :9" });
        talkData.Add(4000, new string[] { "[그린]" + "\n" + "\n" +"아. 피트라씨 안녕하세요.:12",
                                          "[그린]" + "\n" + "\n" +"좋은 하루 보내요 :13" });
        talkData.Add(5000, new string[] { "[마린]" + "\n" + "\n" +"나 바빠요.:15"  });

        /*locker talk data 생성*/
        talkData.Add(100, new string[] { "\n" + "\n" + "...잠겨있다.:9999" });
        talkData.Add(200, new string[] { "\n" + "\n" + "에너지 부스터가 들어있다... 챙겨보자." + "\n" + "(에너지 부스터를 얻었다.):200" });
        talkData.Add(300, new string[] { "\n" + "\n" + "내 자리. 좀 더럽다...:0" });
        talkData.Add(700, new string[] { "\n" + "\n" + "선이다!.:0" });
        talkData.Add(800, new string[] { "[문]" + "\n" + "\n" + "사용 권한이 없습니다.:0" });
        talkData.Add(900, new string[] { "[문]" + "\n" + "\n" + "사용가능한 시간이 아닙니다.:0" });



        /* quest talk (quest는 10번부터 시작) */


        /* quest 10 - Sindy에게 서류를 받자 */
        talkData.Add(10 + 2000, new string[] { "[???]"+"\n"+"\n"+"......씨?:6", "[???]"+"\n"+"\n"+"피트라씨?:7",
                                                "[피트라]"+"\n"+"\n"+".....:2",
                                                "[???]"+"\n"+"\n"+"피.트.라.씨?:7",
                                                "[피트라]"+"\n"+"\n"+"....네??:4",
                                                "[조니]"+"\n"+"\n"+"그러니까 Sindy 씨에게 맡겨놓은 문서 처리 좀 부탁해요. :5",
                                                "[피트라]"+"\n"+"\n"+".....예....? :4",
                                                "[조니]"+"\n"+"\n"+"부탁해요. 최대한 빨리.:5",
                                                "[피트라]"+"\n"+"\n"+".....아?.. 네네. 알겠습니다.:2",
                                                "[피트라]"+"\n"+"\n"+"(...방금 전까지 내가 뭐하고 있었더라):3" ,
                                                "[피트라]"+"\n"+"\n"+"(신디씨...? 빨간 머리였던 것 같은데..):3",
                                              });
        talkData.Add(11 + 2000, new string[] { "[조니]" + "\n" + "\n" + "문서는 [신디] 씨에게 맡겨뒀어요. 부탁해요.:5" });
        talkData.Add(11 + 3000, new string[] {  "[신디]"+"\n"+"\n"+"안녕하세요~?:8",
                                                "[신디]"+"\n"+"\n"+"아 문서요? 여기요~:9",
                                                "[신디]"+"\n"+"\n"+"옆 사무실 B동 의 그린씨에게 전달 부탁해요~:8",
                                                "[피트라]"+"\n"+"\n"+"(그린씨… 밝은 갈색머리..):3",
                                                "[피트라]"+"\n"+"\n"+"(정신은 없어도 기억력은 좋군):1",
                                                "[신디]"+"\n"+"\n"+"아! 사람들과 부딪히지 않게 조심하세요~:9",
                                                "[신디]"+"\n"+"\n"+"다들 늘 화가 잔뜩 나있으니까요~!:8",
                                                "\n"+"\n"+"(문서를 얻었다.):0",
                                             });


        /*quest 20 - 서류를 B동으로 가져가자. */
        /*20 - 주요 대사들*/
        talkData.Add(20 + 4000, new string[] { "[그린]" + "\n" + "\n" + "피트라씨!!!:11",
                                               "[피트라]" + "\n" + "\n" + "예…?:4 ",
                                               "[피트라]" + "\n" + "\n" + "안녕하세요 그린씨:2",
                                               "[그린]" + "\n" + "\n" +"아...안녕하세요..:13",
                                               "[피트라]" + "\n" + "\n" + "여기 A동 문서입니다. 조니 상사님이 전해드리라고 하셨습니다.:1",
                                               "[그린]" + "\n" + "\n" +"감사해요:13",
                                               "[그린]" + "\n" + "\n" +"조니상사님께 오후까지 마무리 하겠다고 전해주세요.:12",
                                               "[피트라]" + "\n" + "\n" + "네. 그럼 수고하세요.:1",
                                               "[피트라]" + "\n" + "\n" + "(기분이 안좋아보이시네. 방금 뭐 실수했나..?):3",
                                               "[피트라]" + "\n" + "\n" + "(흠, 아마 일이 많아져서 그런 거 같군. 난 상사님께 보고하러 가야겠다.):2",
                                             });
        talkData.Add(21 + 4000, new string[] { "[그린]" + "\n" + "\n" + "조니상사님께 오후까지 마무리 하겠다고 전달 부탁드릴게요.:11" });
        talkData.Add(21 + 2000, new string[] { "[조니]" + "\n" + "\n" +"문서는 잘 전달했어요?:6",
                                               "[조니]" + "\n" + "\n" +"아, 고마워요. 역시 빠르다니까.:5",
                                               "[조니]" + "\n" + "\n" +"이건 피곤해보이는 피트라씨를 위한 감사선물.:6",
                                               "\n" + "\n" +"(조니에게 에너지 부스트를 받았다):200",
                                               "[조니]" + "\n" + "\n" +"마시면 힘이 불끈. 없던 에너지도 충전된다면서요??"+ "\n" +"광고에서...:6",
                                               "[조니]" + "\n" + "\n" +"그럼 오늘은 이만 들어가 봐요.:5",
                                               "[피트라]" + "\n" + "\n" + "네. 감사합니다:1",
                                               "[피트라]" + "\n" + "\n" + "(좋아. 퇴근이다!):1",
                                               "[피트라]" + "\n" + "\n" + "(얼른 짐싸서 집에 가자.):1",
                                               "[피트라]" + "\n" + "\n" + "(내 자리... 왼쪽 첫번째 책상이지.):2",
                                               });
        /*20 - 기타 대사들*/
        talkData.Add(20 + 2000, new string[] { "[조니]" + "\n" + "\n" + "전달하고나면 나한테 상황 좀 알려줘요."+"\n"+"고마워요:5" });
        talkData.Add(20 + 3000, new string[] { "[신디]" + "\n" + "\n" + "B동 그린씨에게 전달하면 돼요!:9",
                                               "[신디]"+"\n"+"\n"+"서두르는게 좋을거예요~:8" });
        talkData.Add(21 + 3000, new string[] { "[신디]" + "\n" + "\n" + "수고했어요~ 조니상사님이 기다리시고 계신 것 같던데요~?:9" });


        /*quest 30 - 피트라의 자리로 가자.*/
        talkData.Add(30 + 300, new string[] { "\n" + "\n" + "[ 책상 위에 문서가 놓여있다 ]:0",
                                           "[피트라]" + "\n" + "\n" + "보안 A대상 문서... 이게 뭐지?:3",
                                           "\n" + "\n" + "A동 조정 관련. 담당 부서 E 동.:0",
                                           "\n" + "\n" + "대상 - 피트....:0",
                                           "[마린]" + "\n" + "\n" + "뭐야 이게 왜 여기있어? .:17",
                                           "[마린]" + "\n" + "\n" + "(마린이 신경질적으로 문서를 빼았았다.) .:16",
                                           "[피트라]" + "\n" + "\n" + "아.. 마린씨 문서입니까?:3",
                                           "[마린]" + "\n" + "\n" + "네. 설마 맘대로 열어본 건 아니죠?.:15",
                                           "[피트라]" + "\n" + "\n" + "제 책상에 있길래 확인을..:3",
                                           "[피트라]" + "\n" + "\n" + "그런데 저희 회사는 D동이 끝 아닙니까?:3",
                                           "[마린]" + "\n" + "\n" + "...:15",
                                           "[마린]" + "\n" + "\n" + "쓸데없는 소리 마요. 나 지금도 충분히 바쁘니까.:16",
                                           "[피트라]" + "\n" + "\n" + "...그러죠:3",
                                           "[피트라]" + "\n" + "\n" + "(문서에 그거 내 이름이었던 것 같아. E 동은 또 어디지?):3",
                                           "[피트라]" + "\n" + "\n" + "(혹시, 요즘 같은 힘든 세상에 벌써 해고..?):4",
                                           "[피트라]" + "\n" + "\n" + "무슨 일인지 알아봐야겠어." + "\n" +"일단 오늘은 집으로 돌아가자.:3"
                                         });

        /* quest 40 - 1층으로 나가 퇴근하자 */
        talkData.Add(40 + 800, new string[] { "[문]" +"\n" + "\n" + "사용 권한이 없습니다.:0" ,
                                              "[피트라]" +"\n" + "\n" + "음, 이게 왜 안되지? :4" ,
                                              "[문]" +"\n" + "\n" + "권한이 없습니다.:0" ,
                                            });
        talkData.Add(41 + 4000, new string[] { "[그린]" + "\n" + "\n" + "저기..나가시는 문은 왼쪽이에요...:12",
                                               "[피트라]" + "\n" + "\n" + "아! 그린씨, 감사합니다. 제가 오늘 정신이 없어서..:2",
                                               "[그린]" + "\n" + "\n" + "이해해요. 안녕히가세요:12",
                                               "[피트라]" + "\n" + "\n" + "그린씨도 조심히가세요.:2",
                                             });
        talkData.Add(42 + 900, new string[] { "[문]" + "\n" + "\n" + "문이 열립니다.:0"});

        /*40 - 기타 대사들*/
        talkData.Add(42 + 800, new string[] { "[문]" + "\n" + "\n" + "사용 권한이 없습니다.:0" });
        talkData.Add(42 + 4000, new string[] { "[그린]" + "\n" + "\n" + "아, 왼쪽에 저 문으로 나가시면 돼요!:12" });


        /* quest 50 - 출근 - 피트라의 자리로 가자. */
        talkData.Add(50 + 600, new string[] { "[피트라]" +"\n" + "\n" + "...출근이다.  :3" ,
                                              "[피트라]" +"\n" + "\n" + "늦지 않게 얼른 내 자리로 가자.  :3" 
                                            });
        talkData.Add(50 + 300, new string[] { "[피트라]" +"\n" + "\n" + "업무를 시작해볼까.:3" ,
                                            });

        /* quest 60 - 정전. */
        talkData.Add(60 + 1000, new string[] {"[피트라]" +"\n" + "\n" + "앗, 출근 하자마자 정전..? "+"\n"+"이래선 아무것도 할 수가 없는데 :4",
                                            });

    }

    public string GetTalk (int id, int talkIndex) // .GetTalk(id + questTalkIndex, talkIndex) 이렇게 Game Manager에서쓰임
    {
        //Debug.Log("id:" + id + "talkINdex: " + talkIndex);
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

        if (talkIndex >= talkData[id].Length)
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
