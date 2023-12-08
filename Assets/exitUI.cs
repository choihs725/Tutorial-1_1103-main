using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class exitUI : MonoBehaviour
{
    // Start is called before the first frame update
    private exit exitScript;
    public GameObject worldSpaceCanvas;
    private bool buttonPreviouslyPressed = false;
    private bool uiActive = false;

    void Start()
    {
        exitScript = FindObjectOfType<exit>();
    }

    // Update is called once per frame
    void Update()
    {
        bool buttonPressed = exitScript.val;
        if (buttonPressed)
        {
            if(buttonPressed != buttonPreviouslyPressed)
            {
                uiActive = !uiActive;
                worldSpaceCanvas.SetActive(uiActive);
                buttonPreviouslyPressed = buttonPressed;
            }
            Debug.Log("activating ui");
        }
    }
}
