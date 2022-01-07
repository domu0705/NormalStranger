// -------------------------------------------------------------------------------------------------
// npc, item 의 기본 정보 설정
// -------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -------------------------------------------------------------------------------------------------
public class ObjectData : MonoBehaviour
{
    public int id;
    public bool isNpc;
    public bool isChecked; // 서랍속 물건들은 한번 가져갔으면 다음엔 없어야 됨. 
    public bool isArrived;
}
