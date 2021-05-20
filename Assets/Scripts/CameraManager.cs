using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera FirstCamera; //player중심의 카메라
    public Camera SecondCamera; // story용 카메라


    public void UseFirstCamera()
    {
        FirstCamera.enabled = true;
        SecondCamera.enabled = false;
    }

    public void UseSecondCamera()
    {
        FirstCamera.enabled =false;
        SecondCamera.enabled = true;
    }
}
