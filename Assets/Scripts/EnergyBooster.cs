using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnergyBooster : MonoBehaviour
{
    public GameObject boosterImg;
    public Text boosterText;
    public int boosterNum;



    public void getBooster()
    {
        boosterNum++;
        if (boosterNum > 0)
        {
            boosterImg.SetActive(true);
            boosterText.gameObject.SetActive(true);
        }
        
        boosterText.text = boosterNum.ToString();
    }

    public void useBooster()
    {
        boosterNum--;
        if(boosterNum <= 0)
        {
            boosterImg.SetActive(false);
            boosterText.gameObject.SetActive(false);
        }
        boosterText.text = boosterNum.ToString();
    }
}
