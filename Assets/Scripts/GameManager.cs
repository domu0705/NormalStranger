using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public int talkIndex;
    public GameObject scanObject;
    public bool isAction;

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
        string talkData = talkManager.GetTalk(id, talkIndex);
        if(talkData == null)//얘기가 더이상 없을 때
        {
            isAction = false;
            talkIndex = 0;
            return;
        }

        if (isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }
        isAction = true;
        talkIndex++;
    }
}
