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
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjectData objData = scanObj.GetComponent<ObjectData>();
        talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    // Update is called once per frame
    void talk(int id,bool isNpc)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);

        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

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
