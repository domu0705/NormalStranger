using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//scene관련 함수를 사용하기 위해 필요함

public class GameManager : MonoBehaviour
{

    public TalkManager talkManager;
    public GameObject talkPanel;
    public QuestManager questManager;
    public GameObject gameOverPanel;

    public Text talkText;
    public Text heartText;
    public int talkIndex;
    
    public bool isAction;
    public Image portraitLeftImg;
    public Image portraitRightImg;
    public GameObject scanObject;
    public GameObject[] places;
    public PlayerMove player;


    private void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }


    /*space bar 누를때 실행되는 함수*/
    public void Action(GameObject scanObj) 
    {
        scanObject = scanObj;
        ObjectData objData = scanObj.GetComponent<ObjectData>();

        if(scanObj.tag == "Door")
        {
            /*공간 이동하기. (문을 통해 장소 이동)*/
            door(scanObj);
        }
        else
        {
            /*말풍선이 있다면 띄워주기*/
            talk(objData.id, objData.isNpc);
            talkPanel.SetActive(isAction);
        }
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

            /* 말하는 대상이 Fitra 일때만 오른쪽 portrait 활성화
             * 혼잣말을 할 때는 아무 캐릭터도 보이지 않게 하기 위해서 투명도를 올림.
             */
            
            int portraitNum = int.Parse(talkData.Split(':')[1]);
            if (portraitNum >= 1 && portraitNum <= 4 )
            {
                portraitRightImg.sprite = talkManager.GetPortrait(id, portraitNum);
                portraitLeftImg.color = new Color(1, 1, 1, 0);
                portraitRightImg.color = new Color(1, 1, 1, 1);
            }

            else if (portraitNum == 0)
            {
                portraitLeftImg.color = new Color(1, 1, 1, 0);
                portraitRightImg.color = new Color(1, 1, 1, 0);
            }
            else
            {
                portraitLeftImg.sprite = talkManager.GetPortrait(id, portraitNum);
                portraitLeftImg.color = new Color(1, 1, 1, 1);
                portraitRightImg.color = new Color(1, 1, 1, 0);
            }
        }
        else
        {
            talkText.text = talkData;
            portraitLeftImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;
    }


    /*공간 이동하기*/
    void door(GameObject scanObj)
    {
        DoorData door = scanObj.GetComponent<DoorData>();
        switch (door.type)
        {
            case DoorData.DoorType.DoorAIn:
                places[0].SetActive(true);
                places[1].SetActive(false);
                player.transform.position = new Vector3(1.5f, 8.4f, 0);
                break;

            case DoorData.DoorType.DoorAOut:
                places[0].SetActive(false);
                places[1].SetActive(true);
                player.transform.position = new Vector3(1.5f, 11, 0);
                break;

            case DoorData.DoorType.DoorBIn:
                break;

            case DoorData.DoorType.DoorBOut:
                break;

        }

    }

    /*플레이어 하트 감소*/
    public void heartDecrease()
    {
        heartText.text = "x" + (player.heart).ToString();
        //string.Format("{0:n0}", player.score);
    }

    public void gameOver()
    {
        Debug.Log("게임 오버");
        gameOverPanel.SetActive(true);

    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
