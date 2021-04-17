using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000,new string[] { "피트라씨?", "그러니까 이문서를 빨리 B동으로 넘겨줘요. B동이 아딘지는 알죠?"});
        talkData.Add(2000, new string[] { "아..", "안녕하세요. " });
    }

    public string GetTalk (int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length) // 얘기가 모두 끝났을 때
            return null;
        else 
            return talkData[id][talkIndex];
    }
}
