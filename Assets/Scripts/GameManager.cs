using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public TalkManager talkManager;
    public GameObject talkPanel;
    public QuestManager questManager;
    public Text talkText;
    public int talkIndex;
    public GameObject scanObject;
    public bool isAction;
    public Image portraitImg;


    private void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }

    /*space bar 누를때 실행되는 함수*/
    public void Action(GameObject scanObj) 
    {
        scanObject = scanObj;
        ObjectData objData = scanObj.GetComponent<ObjectData>();
        talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

 
    void talk(int id,bool isNpc)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);

        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex); //조니 id : 2000, +10 

        if(talkData == null)//얘기가 더이상 없을 때 (대화가 끝났을 때)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));

            return;
        }

        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];
            portraitImg.color = new Color(1, 1, 1, 1);
            portraitImg.sprite = talkManager.GetPortrait(id , int.Parse(talkData.Split(':')[1])); // int.Parse 는 문자열을 해당 타입으로 변환해주는 함수임


            /* 말하는 대상이 Fitra 일 시 캐릭터 portrait 를 오른쪽에 배치
             * 혼잣말을 할 때는 아무 캐릭터도 보이지 않게 하기 위해서 투명도를 올림.
             */
            int portraitNum = int.Parse(talkData.Split(':')[1]);
            RectTransform rectTransform = portraitImg.GetComponent<RectTransform>();
            if (portraitNum == 1 || portraitNum == 2 || portraitNum == 3)
            {
                rectTransform.anchoredPosition = new Vector2(553, 174);
                portraitImg.color = new Color(1, 1, 1, 1);
            }

            else if (portraitNum == 0)
                portraitImg.color = new Color(1, 1, 1, 0);
            else
            {
                rectTransform.anchoredPosition = new Vector2(-33.6f, 174);
                portraitImg.color = new Color(1, 1, 1, 1);
            }
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;
    }
}
