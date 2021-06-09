using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ElevatorManager : MonoBehaviour
{
    public GameManager manager;
    public GameObject elevatorPanel;
    public GameObject[] floors;
    public GameObject elevatorObj;
    public QuestManager questManager;

   

    public int currentFloor;

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
        Animator elevatorAnim = elevatorObj.GetComponent<Animator>();
        elevatorAnim.SetTrigger("elevatorOn");

        Invoke("firstFloorSetting", 1.5f);
    }

    void firstFloorSetting()
    {
        elevatorOff();

        currentFloor = 1;
        floors[0].SetActive(true);
        floors[1].SetActive(false);
        floors[2].SetActive(false);

        manager.player.transform.position = new Vector3(42, 15.22f, manager.player.transform.position.z);

        /*quest에 따라 바뀌는 object 위치, 세팅을 설정하기*/
        questManager.green3thfloor.SetActive(false);
    }

    public void secondFloorButton()
    {
        Animator elevatorAnim = elevatorObj.GetComponent<Animator>();
        elevatorAnim.SetTrigger("elevatorOn");

        Invoke("secondFloorSetting", 1.5f);

        
    }


    void secondFloorSetting()
    {
        elevatorOff();

        currentFloor = 2;
        floors[0].SetActive(false);
        floors[1].SetActive(true);
        floors[2].SetActive(false);

        manager.player.transform.position = new Vector3(42, 15.22f, manager.player.transform.position.z);

        /*quest에 따라 바뀌는 object 위치, 세팅을 설정하기*/
        questManager.green3thfloor.SetActive(false);
    }


    public void thirdFloorButton()
    {
        Animator elevatorAnim = elevatorObj.GetComponent<Animator>();
        elevatorAnim.SetTrigger("elevatorOn");

        Invoke("thirdFloorSetting", 1.5f);

        
    }


    void thirdFloorSetting()
    {
        elevatorOff();

        currentFloor = 3;
        floors[0].SetActive(false);
        floors[1].SetActive(false);
        floors[2].SetActive(true);

        manager.player.transform.position = new Vector3(42, 15.22f, manager.player.transform.position.z);


        /*quest에 따라 바뀌는 object 위치, 세팅을 설정하기*/
        if (questManager.questId == 90)
        {
            questManager.green3thfloor.SetActive(true);
        }
        else
        {
            questManager.green3thfloor.SetActive(false);
        }
    }
}

