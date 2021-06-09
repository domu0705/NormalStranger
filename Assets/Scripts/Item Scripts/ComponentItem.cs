using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentItem : MonoBehaviour
{
    public GameObject componentImg;
    public Text componentText;
    public int componentNum;



    public void getComponentItem()
    {
        componentNum++;
        if (componentNum > 0)
        {
            componentImg.SetActive(true);
            componentText.gameObject.SetActive(true);
        }

        componentText.text = componentNum.ToString();
    }

    public void useComponent()
    {
        componentImg.SetActive(false);
        componentText.gameObject.SetActive(false);
    }
}
