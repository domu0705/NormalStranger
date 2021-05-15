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
    public ElevatorManager elevatorManager;
    public Text talkText;
    public Text heartText;
    public int talkIndex;
    
    public bool isAction;
    public Image portraitLeftImg;
    public Image portraitRightImg;
    public GameObject scanObject;
    public GameObject[] places;
    public PlayerMove player;
    public EnergyBooster energyBooster;
    public ObjectData objData;//현재 조사중인 물건 or 사람


    private void Start()
    {
        Debug.Log(questManager.CheckQuest());
        heartChanged();
    }


    /*space bar 누를때 실행되는 함수
      함수의 실행을 위해서는 object의 layer을 ispectObject로 바꿔주는 것 잊지 말기. 
    */
    public void Action(GameObject scanObj) 
    {
        scanObject = scanObj;
        objData = scanObj.GetComponent<ObjectData>();

        if(scanObj.tag == "Door")
        {
            /*공간 이동하기. (문을 통해 장소 이동)*/
            door(scanObj);
        }
        else if (scanObj.tag == "Elevator")
        {
            elevator();
        }
        else
        {
            /*말풍선이 있다면 띄워주기*/
            talk(objData.id, objData.isNpc);
            talkPanel.SetActive(isAction);
        }
    }

 
    void talk(int id,bool isNpc)//playerMove에서 isAction이 false면 안움직임. 그래서 계속 이야기 할 수 있는 것임.
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);

        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex); //조니 id : 2000, +10 

        if(talkData == null)//얘기가 더이상 없을 때 (대화가 끝났을 때, 물건 조사가 끝났을 때)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        int portraitNum = int.Parse(talkData.Split(':')[1]);

        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            /* 말하는 대상이 Fitra 일때만 오른쪽 portrait 활성화
             * 혼잣말을 할 때는 아무 캐릭터도 보이지 않게 하기 위해서 투명도를 올림.
             */
            
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
            else if (portraitNum == 200)//대화도중 npc에게 booster을 받았다면 (jhonny만 booster을 주기때문에 portrait는 조니로 고정해둠)
            {
                portraitLeftImg.sprite = talkManager.GetPortrait(id, 5); // jhonny의 웃는얼굴임.
                portraitLeftImg.color = new Color(1, 1, 1, 1);
                portraitRightImg.color = new Color(1, 1, 1, 0);
                energyBooster.getBooster();
            }
            else
            {
                portraitLeftImg.sprite = talkManager.GetPortrait(id, portraitNum);
                portraitLeftImg.color = new Color(1, 1, 1, 1);
                portraitRightImg.color = new Color(1, 1, 1, 0);
            }

            
        }
        else // 물건을 조사할 때
        {
            Debug.Log("물건을 조사한다");
            talkText.text = talkData.Split(':')[0];
            if (portraitNum == 200) // energybooster 을 발견했을 때. (energy booster의 대화 끝에는 200번 써줌)
            {
                if (objData.isChecked)//이미 확인했던 Locker 라면 아이템이 계속 나오면 안됨
                {
                    talkText.text = "..이미 열었던 곳이야";
                }
                else//처음 확인하는 Locker 이라면 energy booster을 얻는다.
                {
                    energyBooster.getBooster();
                    objData.isChecked = true;
                }
            }
            portraitLeftImg.color = new Color(1, 1, 1, 0);
        }
        questManager.ControlObject();
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
                places[2].SetActive(true);
                places[1].SetActive(false);
                player.transform.position = new Vector3(1.5f, 8.4f, 0);
                break;

            case DoorData.DoorType.DoorAOut:
                places[2].SetActive(false);
                places[1].SetActive(true);
                player.transform.position = new Vector3(1.5f, 11, 0);
                break;

            case DoorData.DoorType.DoorBIn:
                places[1].SetActive(false);
                places[3].SetActive(true);
                player.transform.position = new Vector3(19.5f, 11.1f, 0);
                break;

            case DoorData.DoorType.DoorBOut:
                places[3].SetActive(false);
                places[1].SetActive(true);
                player.transform.position = new Vector3(18.5f, 6.2f, 0);
                break;

        }

    }


    /*game manager에서 상위 object를 꺼놓으면 여기서 아무리 하위 object를 켜도 안켜지는 점 주의하기
     *  예를들어 여기서 2nd floor을 꺼버리면 그안의 Room A를 켜도 안보임. 
     */
    void elevator()
    {
        Debug.Log("엘리베ㅣㅇ터 함수로 들어옴");

        /*엘리베이터가 아닌 다른 공간은 모두 끄기*/
        places[0].SetActive(false);
        places[1].SetActive(false);
        places[2].SetActive(false);
        places[3].SetActive(false);
        places[4].SetActive(false);

        /*엘리베이터 내부 이미지 및 UI켜기*/
        elevatorManager.elevatorOn();
    }


    /*플레이어 하트 감소*/
    public void heartChanged()
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
