// -------------------------------------------------------------------------------------------------
// 문 정보 설정. 두개의 문(DoorIn, DoorOut)을 연결시키기 위해 사용.
// -------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorData : MonoBehaviour
{
    public enum DoorType { DoorAIn, DoorAOut, DoorBIn,DoorBOut, DoorCIn, DoorCOut, DoorDIn, DoorDOut, DoorRepairIn, DoorRepairOut };
    public DoorType type;
}
