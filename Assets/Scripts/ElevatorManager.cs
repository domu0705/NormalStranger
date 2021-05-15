using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ElevatorManager : MonoBehaviour
{
    public GameManager manager;
    public GameObject elevatorPanel;
    public GameObject[] floors;

    void Update()
    {
        
    }

    public void elevatorOn()
    {
        elevatorPanel.SetActive(true);
    }

    public void elevatorOff()
    {
        elevatorPanel.SetActive(false);
    }




    /*game manager에서 상위 object를 꺼놓으면 여기서 아무리 하위 object를 켜도 안켜지는 점 주의하기*/
    public void firstFloorButton()
    {
        elevatorOff();

        floors[0].SetActive(true);
        floors[1].SetActive(false);
        floors[2].SetActive(false);
    }

    public void secondFloorButton()
    {
        elevatorOff();

        floors[0].SetActive(false);
        floors[1].SetActive(true);
        floors[2].SetActive(false);
    }

    public void thirdFloorButton()
    {
        elevatorOff();
        Debug.Log("3층 버튼 누름");
        floors[0].SetActive(false);
        floors[1].SetActive(false);
        floors[2].SetActive(true);
    }
}

