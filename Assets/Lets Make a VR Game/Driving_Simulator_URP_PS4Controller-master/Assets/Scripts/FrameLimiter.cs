using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLimiter : MonoBehaviour
{
    [Range(30,60)]
    public int targetFrameRate = 30;
    private void Start()
    {

        print(QualitySettings.vSyncCount);

        Application.targetFrameRate = 70;

        //Cursor.visible = false;
    }

    
}
