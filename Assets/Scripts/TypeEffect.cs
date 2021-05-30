using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public GameObject EndCursor;
    public AudioSource audioSource;
    public string TargetMsg;
    public int charPerSeconds;
    public Text msgText;
    public bool isAnim;

    int index;
    bool isNameDone;//캐릭터의 이름이 나올 떄는 소리를 나지 않게 하기.
    float interval;//글자 나오는 속도

    void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }


    public void  SetMsg(string msg)
    {

        if (isAnim)//anim실행중에 space바 누르면
        {
            msgText.text = TargetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            TargetMsg = msg;
            msgText.text = "";
            cutName(msg);
            EffectStart();
        }
    }

    void cutName(string msg)
    {
        if(msg[0] == '[') // 이름이 있는 경우에만 cutName을 하기
        {
            string name = msg.Split(']')[0];
            msgText.text = name + "]";
            index = name.Length+1;
        }
        else
        {
            index = 0;
        }
    }

    // Update is called once per frame
    void EffectStart()
    {
        
        EndCursor.SetActive(false);
        interval = (1.0f / charPerSeconds);
        isAnim = true;
        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        if (msgText.text == TargetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += TargetMsg[index];
        

        //소리
        if((TargetMsg[index] != ' ' ) && (TargetMsg[index] !=  '.'))
            audioSource.Play();
        
        index++;
        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        isAnim = false;
        EndCursor.SetActive(true);
    }
}
