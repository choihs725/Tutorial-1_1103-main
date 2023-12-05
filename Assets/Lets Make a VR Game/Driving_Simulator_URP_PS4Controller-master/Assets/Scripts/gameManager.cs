using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    int camIndex;

    public GameObject[] cameras;

    public void closeApp()
    {
        Application.Quit();
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
    }
    private void Update()
    {
        switchCamera();
    }
    void switchCamera()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (camIndex == 5)
            {
                cameras[camIndex].SetActive(false);
                camIndex = 0;
                cameras[camIndex].SetActive(true);
            }
            else
            {
                cameras[camIndex].SetActive(false);
                camIndex++;
                cameras[camIndex].SetActive(true);
            }
        }
    }
}
