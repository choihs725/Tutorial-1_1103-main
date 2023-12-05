using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Brake_R : MonoBehaviour
{
    public InputActionReference brakeAction;
    public bool val = false;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        brakeAction.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // brakeAction의 현재 값을 읽어옴
        float brakeValue = brakeAction.action.ReadValue<float>();

        // brakeValue가 1이면 (버튼이 눌렸으면) val을 true로 설정
        val = brakeValue == 1;

        // 버튼이 눌렸을 때 로그를 출력
        if (val)
        {
            Debug.Log("Left Brake activated");
        }
    }
}
