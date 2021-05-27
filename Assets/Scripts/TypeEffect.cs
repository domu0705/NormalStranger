﻿using System.Collections;
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
        Debug.Log("값 넣어");
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
            cutName(msg);
            EffectStart();
        }
    }

    void cutName(string msg)
    {
        string name = msg.Split(']')[0];
        msgText.text = name + "]";
        index = name.Length+1;
        Debug.Log("index는 : " + index);
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
        Debug.Log("effecting 부르는 중");
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
