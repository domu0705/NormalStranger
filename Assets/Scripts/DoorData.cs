using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorData : MonoBehaviour
{
    public enum DoorType { DoorAIn, DoorAOut, DoorBIn,DoorBOut, DoorCIn, DoorCOut, DoorDIn, DoorDOut, DoorRepairIn, DoorRepairOut };
    public DoorType type;

    // Update is called once per frame
    void Update()
    {
        
    }
}
